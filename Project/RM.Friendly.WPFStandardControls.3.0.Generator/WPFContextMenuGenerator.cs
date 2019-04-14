using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using Codeer.TestAssistant.GeneratorToolKit;
using RM.Friendly.WPFStandardControls.Inside;
using System.Windows;
using System.Windows.Media;

namespace RM.Friendly.WPFStandardControls.Generator
{
    [CaptureCodeGenerator("RM.Friendly.WPFStandardControls.WPFContextMenu")]
    public class WPFContextMenuGenerator : CaptureCodeGeneratorBase
    {
        ContextMenu _control;
        delegate void DetachEvent();
        List<DetachEvent> _detach = new List<DetachEvent>();

        protected override void Attach()
        {
            _control = ControlObject as ContextMenu;

            System.Windows.Forms.Timer timer = null;
            _control.IsVisibleChanged += (_, __) =>
            {
                //メニューが表示された時にアタッチ
                if (_control.IsVisible)
                {
                    //タイマーで子アイテムが表示されたときにアタッチする
                    var attachedChildren = new List<MenuItem>();
                    if (timer != null) timer.Dispose();
                    timer = new System.Windows.Forms.Timer { Interval = 50 };
                    timer.Tick += (___,____) =>
                    {
                        if (_control.Visibility != Visibility.Visible)
                        {
                            timer.Dispose();
                            return;
                        }
                        AttachChildren(_control, attachedChildren, new string[0]);
                    };
                    timer.Start();
                }
                else
                {
                    if (timer != null) timer.Dispose();
                    timer = null;
                    Detach();
                }
            };
        }

        protected override void Detach()
        {
            foreach (var element in _detach)
            {
                element();
            }
            _detach.Clear();
        }

        void AttachChildren(Visual parent, List<MenuItem> attachedChildren, string[] texts)
        {
            List<MenuItem> items = new List<MenuItem>();
            HeaderedItemsControlUtility.GetChildren(parent, items);
            foreach (var element in items)
            {
                var item = element;
                string text = HeaderedItemsControlUtility.GetItemText(item);
                if (string.IsNullOrEmpty(text)) continue;

                List<string> nextTexts = new List<string>(texts);
                nextTexts.Add(text);

                //まだイベントにアタッチしてない場合
                if (!attachedChildren.Contains(item))
                {
                    attachedChildren.Add(item);

                    RoutedEventHandler click = (s, e) =>
                    {
                        Click(item, nextTexts.ToArray());
                    };
                    item.Click += click;

                    _detach.Add(() =>
                    {
                        item.Click -= click;
                    });
                }

                //子アイテムにアタッチ
                AttachChildren(item, attachedChildren, nextTexts.ToArray());
            }
        }

        void Click(MenuItem item, string[] texts)
        {
            if (!string.IsNullOrEmpty(item.Name) || 0 < item.Items.Count)
            {
                return;
            }
            StringBuilder getArgs = new StringBuilder();
            foreach (var element in texts)
            {
                if (0 < getArgs.Length)
                {
                    getArgs.Append(", ");
                }
                getArgs.Append(GenerateUtility.ToLiteral(element));
            }

            AddSentence(new TokenName(), ".GetItem(" + getArgs.ToString() + ").EmulateClick(", new TokenAsync(CommaType.Non), ");");
        }
    }
}

