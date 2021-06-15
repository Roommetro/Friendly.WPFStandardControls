using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Codeer.TestAssistant.GeneratorToolKit;

namespace RM.Friendly.WPFStandardControls.Generator
{
    [CaptureCodeGenerator("RM.Friendly.WPFStandardControls.WPFListView")]
    public class WPFListViewGenerator : CaptureCodeGeneratorBase
    {
        Selector _control;

        protected override void Attach()
        {
            _control = (Selector)ControlObject;
            _control.SelectionChanged += SelectionChanged;
        }

        protected override void Detach()
        {
            _control.SelectionChanged -= SelectionChanged;
        }

        public override bool ConvertChildClientPoint(ref System.Drawing.Point clientPointWinForms, out string childUIObject)
        {
            childUIObject = string.Empty;
            var clientPoint = new Point(clientPointWinForms.X, clientPointWinForms.Y);

            //指定座標の要素取得
            var hitElement = PointUtility.GetPosElement(clientPoint, _control);
            if (hitElement == null) return false;

            //Item取得
            ListViewItem item = null;
            foreach (var x in TreeUtilityInTarget.VisualTree(hitElement, TreeRunDirection.Ancestors))
            {
                if (Equals(x, _control)) break;
                item = x as ListViewItem;
                if (item != null) break;
            }

            if (item == null) return false;

            var index = _control.ItemContainerGenerator.IndexFromContainer(item);

            //座標変換
            var screenPos = _control.PointToScreen(clientPoint);
            var childPoint = item.PointFromScreen(screenPos);
            clientPointWinForms.X = (int)childPoint.X;
            clientPointWinForms.Y = (int)childPoint.Y;

            childUIObject = $".GetItem({index})";

            return true;
        }

        void SelectionChanged(object sender, EventArgs e)
        {
            if (GenerateUtility.HasFocus(_control) && _control.SelectedIndex != -1)
            {
                AddSentence(new TokenName(), ".EmulateChangeSelectedIndex(" + _control.SelectedIndex, new TokenAsync(CommaType.Before), ");");
            }
        }
    }
}
