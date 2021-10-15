using System;
using System.Windows.Forms;

namespace RM.Friendly.WPFStandardControls.Generator.CreateDriver
{
    public partial class DriverCodeSettingForm : Form
    {
        public delegate string GetDriverCodeDelegate(Type targetType, string driverName, string propertyCode, string methodCode);
        public GetDriverCodeDelegate DelegateGetDriverCode = null;
        public delegate string GetGeneratorCodeDelegate(string driverName, string generatorName, string[] targetEventName);
        public GetGeneratorCodeDelegate DelegateGetGeneratorCode = null;

        object _objTarget = null;
        string _driverName = string.Empty;
        string _generatorName = string.Empty;

        public DriverCodeSettingForm(object objTarget, string driverName, string generatorName)
        {
            InitializeComponent();

            _objTarget = objTarget;
            _driverName = driverName;
            _generatorName = generatorName;

            _driverCodeDriverControl.DelegateUpdateCodeRequest = OnCodeUpdateRequest;
            _driverCodeGeneratorControl.DelegateUpdateCodeRequest = OnCodeUpdateRequest;
        }

        public void AddEventName(string name)
        {
            _driverCodeGeneratorControl.AddEventName(name);
        }

        public string[] GetSelectedEventName()
        {
            return _driverCodeGeneratorControl.GetSelectedEventName();
        }

        /// <summary>
        /// プロパティ/フィールドのドライバ用コード文字列を取得
        /// </summary>
        /// <returns>コード文字列</returns>
        public string GetOutputTextProperty()
        {
            return _driverCodeDriverControl.GetOutputTextProperty();
        }

        /// <summary>
        /// メソッドのドライバ用コード文字列を取得
        /// </summary>
        /// <returns>コード文字列</returns>
        public string GetOutputTextMethod()
        {
            return _driverCodeDriverControl.GetOutputTextMethod();
        }

        private void DriverCodeSettingForm_Load(object sender, EventArgs e)
        {
            _driverCodeDriverControl.ObjTarget = _objTarget;
            OnCodeUpdateRequest();
        }

        private void _tabControlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnCodeUpdateRequest();
        }

        void OnCodeUpdateRequest()
        {
            string code = string.Empty;
            switch (_tabControlType.SelectedIndex)
            {
                case 0:
                    {
                        var propertyCode = GetOutputTextProperty();
                        var methodCode = GetOutputTextMethod();
                        code = DelegateGetDriverCode.Invoke(_objTarget.GetType(), _driverName, propertyCode, methodCode);
                    }
                    break;
                case 1:
                    {
                        var targetEventName = GetSelectedEventName();
                        code = DelegateGetGeneratorCode.Invoke(_driverName, _generatorName, targetEventName);
                    }
                    break;
            }
            _textBoxPreview.Text = code;
        }
    }
}
