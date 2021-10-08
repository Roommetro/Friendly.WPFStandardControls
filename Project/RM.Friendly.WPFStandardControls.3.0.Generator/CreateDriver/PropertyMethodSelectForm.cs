using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace RM.Friendly.WPFStandardControls.Generator.CreateDriver
{
    public partial class PropertyMethodSelectForm : Form
    {
        /// <summary>
        /// チェックボックス付きテキストセルクラス
        /// </summary>
        class CheckBoxAndTextCell : DataGridViewCheckBoxCell
        {
            private const int PadLeft = 2;
            private const int PadRight = 3;
            private const int PadTop = 4;
            private const int PadBottom = 3;

            private readonly TextFormatFlags FormatFlags = TextFormatFlags.Left | TextFormatFlags.EndEllipsis;

            private string _text = string.Empty;

            // チェックボックスの横に表示するテキストを取得、設定する。
            public string Text
            {
                get { return _text; }
                set { _text = value; }
            }

            public override object DefaultNewRowValue
            {
                get { return false; }
            }

            protected override void Paint(Graphics graphics,
                                          Rectangle clipBounds, Rectangle cellBounds,
                                          int rowIndex, DataGridViewElementStates cellState,
                                          object value, object formattedValue, string errorText,
                                          DataGridViewCellStyle cellStyle,
                                          DataGridViewAdvancedBorderStyle advancedBorderStyle,
                                          DataGridViewPaintParts paintParts)
            {
                // DataGridViewCheckBoxCell.Paint で、チェックボックスを描画する。
                base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState,
                           value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);

                // チェックボックスの横のセル内の残りスペースに、テキストを描画する。
                Rectangle checkBoxBounds = base.GetContentBounds(graphics, cellStyle, rowIndex);
                Point textLocation = GetTextLocation(cellBounds, checkBoxBounds);
                var availableTextSize = GetAvailableTextSize(cellBounds, checkBoxBounds);
                var availableTextRect = new Rectangle(textLocation, availableTextSize);
                var foreColor = Selected ? cellStyle.SelectionForeColor : cellStyle.ForeColor;
                TextRenderer.DrawText(graphics, Text,
                                      cellStyle.Font, availableTextRect, foreColor, FormatFlags);
            }

            private Point GetTextLocation(Rectangle cellBounds, Rectangle contentBounds)
            {
                int textX = cellBounds.X + contentBounds.Right + PadLeft;
                int textY = cellBounds.Y + PadTop;
                var textLocation = new Point(textX, textY);
                return textLocation;
            }

            private Size GetAvailableTextSize(Rectangle cellBounds, Rectangle contentBounds)
            {
                int textWidth = Math.Max(0, cellBounds.Width - contentBounds.Width - PadLeft - PadRight);
                int textHeight = Math.Max(0, cellBounds.Height - PadBottom);
                var textSize = new Size(textWidth, textHeight);
                return textSize;
            }

            // カラムの自動サイズ設定に使用する。
            protected override Size GetPreferredSize(Graphics graphics,
                                                     DataGridViewCellStyle cellStyle, int rowIndex,
                                                     Size constraintSize)
            {
                Rectangle checkBoxBounds = base.GetContentBounds(graphics, cellStyle, rowIndex);
                Size preferredTextSize = TextRenderer.MeasureText(graphics, Text, cellStyle.Font);
                int contentWidth = checkBoxBounds.Width + preferredTextSize.Width + PadLeft + PadRight;
                int contentHeight = Math.Max(checkBoxBounds.Height, preferredTextSize.Height + PadTop + PadBottom);
                var contentSize = new Size(contentWidth, contentHeight);
                return contentSize;
            }

            // 追加した Text プロパティもクローンに含める。
            public override object Clone()
            {
                var cloneCell = (CheckBoxAndTextCell)base.Clone();
                cloneCell.Text = this.Text;
                return cloneCell;
            }
        }


        object _objTarget = null;
        List<string> _outputTextProperty = new List<string>();
        List<string> _outputTextMethod = new List<string>();

        public PropertyMethodSelectForm(object objTarget)
        {
            InitializeComponent();

            _objTarget = objTarget;
        }

        /// <summary>
        /// プロパティ/フィールドのドライバ用コード文字列を取得
        /// </summary>
        /// <returns>コード文字列</returns>
        public string GetOutputTextProperty()
        {
            return string.Join(null, _outputTextProperty.ToArray());
        }

        /// <summary>
        /// メソッドのドライバ用コード文字列を取得
        /// </summary>
        /// <returns>コード文字列</returns>
        public string GetOutputTextMethod()
        {
            return string.Join(null, _outputTextMethod.ToArray());
        }

        private void PropertyMethodSelectForm_Load(object sender, EventArgs e)
        {
            SetPropertyGridView();
            SetMethodGridView();
        }

        private void _textBoxFilterProperty_TextChanged(object sender, EventArgs e)
        {
            SetVisibleRow(_dataGridViewProperty, _textBoxFilterProperty.Text);
        }

        private void _textBoxFilterMethod_TextChanged(object sender, EventArgs e)
        {
            SetVisibleRow(_dataGridViewMethod, _textBoxFilterMethod.Text);
        }

        private void _buttonOK_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in _dataGridViewProperty.Rows)
            {
                var cell = row.Cells[0] as CheckBoxAndTextCell;
                if ((bool)cell.Value != true)
                {
                    continue;
                }
                AddPropertyCode(cell.Text, (bool)row.Cells[2].Value, (bool)row.Cells[3].Value, GetValueType(cell.Tag));
            }

            foreach (DataGridViewRow row in _dataGridViewMethod.Rows)
            {
                var cell = row.Cells[0] as CheckBoxAndTextCell;
                if ((bool)cell.Value != true)
                {
                    continue;
                }
                AddMethodCode(cell.Tag as MethodInfo, (bool)row.Cells[1].Value);
            }
        }

        /// <summary>
        /// プロパティ/フィールド一覧グリッドを設定
        /// </summary>
        void SetPropertyGridView()
        {
            AddColumnCheckText("Name", _dataGridViewProperty, 200);
            AddColumnText("Type", _dataGridViewProperty, 200);
            AddColumnCheck("Public", _dataGridViewProperty);
            AddColumnCheck("Static", _dataGridViewProperty);

            var t = DateTime.Now;
            var propertyList = new SortedDictionary<string, object>();
            GetPropertyList(_objTarget.GetType(), propertyList);
            var rows = new List<DataGridViewRow>();
            foreach (var property in propertyList)
            {
                var row = new DataGridViewRow();
                {
                    var cell = new CheckBoxAndTextCell();
                    cell.Text = property.Key;
                    cell.Value = false;
                    cell.Tag = property.Value;
                    row.Cells.Add(cell);
                }
                {
                    var cell = new DataGridViewTextBoxCell();
                    var valueType = GetValueType(property.Value);
                    cell.Value = (valueType != null) ? valueType.Name : string.Empty;
                    row.Cells.Add(cell);
                    cell.ReadOnly = true;
                }
                {
                    var cell = new DataGridViewCheckBoxCell();
                    cell.Value = true;
                    row.Cells.Add(cell);
                }
                {
                    var cell = new DataGridViewCheckBoxCell();
                    cell.Value = false;
                    row.Cells.Add(cell);
                }
                rows.Add(row);
            }
            _dataGridViewProperty.Rows.AddRange(rows.ToArray());
        }

        /// <summary>
        /// メソッド一覧グリッドを設定
        /// </summary>
        void SetMethodGridView()
        {
            AddColumnCheckText("Name", _dataGridViewMethod, 200);
            AddColumnCheck("Public", _dataGridViewMethod);

            var methodList = new SortedDictionary<string, object>();
            GetMethodList(_objTarget.GetType(), methodList);
            var rows = new List<DataGridViewRow>();
            foreach (var method in methodList)
            {
                var row = new DataGridViewRow();
                {
                    var cell = new CheckBoxAndTextCell();
                    cell.Text = method.Key;
                    cell.Value = false;
                    cell.Tag = method.Value;
                    row.Cells.Add(cell);
                }
                {
                    var cell = new DataGridViewCheckBoxCell();
                    cell.Value = true;
                    row.Cells.Add(cell);
                }
                rows.Add(row);
            }
            _dataGridViewMethod.Rows.AddRange(rows.ToArray());
        }

        /// <summary>
        /// 対象オブジェクトの値を取得
        /// </summary>
        /// <param name="src">対象オブジェクト</param>
        /// <returns>値</returns>
        object GetValue(object src)
        {
            object ret = null;
            try
            {
                if (src as FieldInfo != null)
                {
                    var fieldInfo = src as FieldInfo;
                    ret = fieldInfo.GetValue(_objTarget);
                }
                else if (src as PropertyInfo != null)
                {
                    var propertyInfo = src as PropertyInfo;
                    ret = propertyInfo.GetValue(_objTarget, null);
                }
            }
            catch { }

            return ret;
        }

        /// <summary>
        /// 対象オブジェクトの値の型を取得
        /// </summary>
        /// <param name="src">対象オブジェクト</param>
        /// <returns>型</returns>
        Type GetValueType(object src)
        {
            Type ret = null;
            try
            {
                if (src as FieldInfo != null)
                {
                    var fieldInfo = src as FieldInfo;
                    ret = fieldInfo.FieldType;
                }
                else if (src as PropertyInfo != null)
                {
                    var propertyInfo = src as PropertyInfo;
                    ret = propertyInfo.PropertyType;
                }
            }
            catch { }

            return ret;
        }

        /// <summary>
        /// フィルタの内容に応じて行を表示するか決める
        /// </summary>
        /// <param name="grid">対象グリッド</param>
        /// <param name="filterText">フィルタテキスト</param>
        void SetVisibleRow(DataGridView grid, string filterText)
        {
            foreach (DataGridViewRow row in grid.Rows)
            {
                bool visible = true;
                if (!string.IsNullOrEmpty(filterText))
                {
                    var cell = row.Cells[0] as CheckBoxAndTextCell;
                    visible = 0 <= cell.Text.IndexOf(filterText, StringComparison.CurrentCultureIgnoreCase);
                    if (!visible)
                    {
                        // 見つからない場合は型名でチェック
                        var value = GetValue(cell.Tag);
                        if (value != null)
                        {
                            visible = (0 <= value.GetType().ToString().IndexOf(filterText, StringComparison.CurrentCultureIgnoreCase));
                        }
                    }
                }
                row.Visible = visible;
            }
        }

        /// <summary>
        /// チェックボックス付きテキストカラムを追加
        /// </summary>
        /// <param name="text">カラムのラベル</param>
        /// <param name="grid">追加先のグリッド</param>
        /// <param name="width">幅</param>
        void AddColumnCheckText(string text, DataGridView grid, int width = 50)
        {
            var viewColumn = new DataGridViewColumn();
            viewColumn.HeaderText = text;
            viewColumn.CellTemplate = new CheckBoxAndTextCell();
            viewColumn.Width = width;
            grid.Columns.Add(viewColumn);
        }

        /// <summary>
        /// テキストカラムを追加
        /// </summary>
        /// <param name="text">カラムのラベル</param>
        /// <param name="grid">追加先のグリッド</param>
        /// <param name="width">幅</param>
        void AddColumnText(string text, DataGridView grid, int width = 100)
        {
            var viewColumn = new DataGridViewTextBoxColumn();
            viewColumn.HeaderText = text;
            viewColumn.Width = width;
            grid.Columns.Add(viewColumn);
        }

        /// <summary>
        /// チェックボックスカラムを追加
        /// </summary>
        /// <param name="text">カラムのラベル</param>
        /// <param name="grid">追加先のグリッド</param>
        void AddColumnCheck(string text, DataGridView grid)
        {
            var viewColumn = new DataGridViewCheckBoxColumn();
            viewColumn.HeaderText = text;
            viewColumn.Width = 50;
            grid.Columns.Add(viewColumn);
        }

        /// <summary>
        /// グリッドに表示する情報を作成
        /// </summary>
        /// <param name="type">値の型</param>
        /// <param name="dst">出力先</param>
        void GetPropertyList(Type type, SortedDictionary<string, object> dst)
        {
            var bindingAttr = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly |
                BindingFlags.Instance | BindingFlags.Static;

            var fields = type.GetFields(bindingAttr | BindingFlags.GetField);
            foreach (FieldInfo f in fields)
            {
                dst[f.Name] = f;
            }
            var properties = type.GetProperties(bindingAttr | BindingFlags.GetProperty);
            foreach (PropertyInfo p in properties)
            {
                dst[p.Name] = p;
            }

            // 継承元があればそちらも対象とする
            if (type.BaseType != null)
            {
                GetPropertyList(type.BaseType, dst);
            }
        }

        /// <summary>
        /// メソッド一覧を取得
        /// </summary>
        /// <param name="type">取得対象</param>
        /// <param name="dst">出力先</param>
        void GetMethodList(Type type, SortedDictionary<string, object> dst)
        {
            var bindingAttr = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.NonPublic;
            MethodInfo[] methodsTmp = type.GetMethods(bindingAttr);
            List<MethodInfo> methods = new List<MethodInfo>(methodsTmp);
            var typeInyerfaces = type.GetInterfaces();
            // 拡張メソッドをチェック
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    var types = assembly.GetTypes();
                    foreach (var t in types)
                    {
                        foreach (var method in t.GetMethods(bindingAttr))
                        {
                            var args = method.GetParameters();
                            if (args.Length <= 0)
                            {
                                continue;
                            }
                            if (args[0].ParameterType == typeof(object))
                            {
                                continue;
                            }
                            var isSubclassOf = type.IsSubclassOf(args[0].ParameterType);
                            var index = Array.IndexOf(typeInyerfaces, args[0].ParameterType);
                            if (!(isSubclassOf || args[0].ParameterType == type || 0 <= index))
                            {
                                continue;
                            }
                            methods.Add(method);
                        }
                    }
                }
                catch { }
            }

            foreach (var info in methods)
            {
                if (info.IsSpecialName)
                {
                    continue;
                }
                dst[info.Name] = info;
            }

            // 継承元があればそちらも対象とする
            if (type.BaseType != null)
            {
                GetMethodList(type.BaseType, dst);
            }
        }

        /// <summary>
        /// プロパティ/フィールドのドライバ用コード文字列を作成
        /// </summary>
        /// <param name="name">プロパティ名</param>
        /// <param name="isPublic">public属性を付けるか判定</param>
        /// <param name="isStatic">static属性を付けるか判定</param>
        /// <param name="type">対象オブジェクトの型</param>
        void AddPropertyCode(string name, bool isPublic, bool isStatic, Type type)
        {
            var attribute = isPublic ? "public " : "";
            attribute += isStatic ? "static " : "";
            var outputText = string.Format("        {0}{1} {2} => this.Dynamic().{3};\n"
                , attribute, GetAliasName(type), name, name);
            _outputTextProperty.Add(outputText);
        }

        /// <summary>
        /// メソッドのドライバ用コード文字列を作成
        /// </summary>
        /// <param name="info">メソッド情報</param>
        /// <param name="isPublic">public属性を付けるか判定</param>
        void AddMethodCode(MethodInfo info, bool isPublic)
        {
            var attribute = isPublic ? "public " : "";
            var returnValueType = GetAliasName(info.ReturnType);
            var parameterText = string.Empty;
            var parameterValueText = string.Empty;
            var parameterList = info.GetParameters();
            for (var i = 0; i < parameterList.Length; i++)
            {
                var parameterInfo = parameterList[i];
                parameterText += (0 < parameterText.Length) ? ", " : "";
                parameterText += GetAliasName(parameterInfo.ParameterType);
                parameterText += " ";
                parameterText += parameterInfo.Name;

                parameterValueText += (0 < parameterValueText.Length) ? ", " : "";
                parameterValueText += parameterInfo.Name;
            }
            var outputText = (0 < _outputTextMethod.Count) ? "\n" : "";
            outputText += string.Format("\n        {0}{1} {2}({3}) =>\n                this.Dynamic().{4}({5});"
                , attribute, returnValueType, info.Name, parameterText, info.Name, parameterValueText);
            _outputTextMethod.Add(outputText);
        }

        /// <summary>
        /// 型名のエイリアスを取得
        /// </summary>
        /// <param name="type">取得対象の型</param>
        /// <returns>型名(取得できない場合はAppVar)</returns>
        string GetAliasName(Type type)
        {
            switch (type.FullName)
            {
                case "System.Boolean": return "bool";
                case "System.Byte": return "byte";
                case "System.SByte": return "sbyte";
                case "System.Char": return "char";
                case "System.Decimal": return "decimal";
                case "System.Double": return "double";
                case "System.Single": return "float";
                case "System.Int32": return "int";
                case "System.UInt32": return "uint";
                case "System.Int64": return "long";
                case "System.UInt64": return "ulong";
                case "System.Object": return "object";
                case "System.Int16": return "short";
                case "System.UInt16": return "ushort";
                case "System.String": return "string";
                case "System.Void": return "void";
            }

            return "AppVar";
        }
    }
}
