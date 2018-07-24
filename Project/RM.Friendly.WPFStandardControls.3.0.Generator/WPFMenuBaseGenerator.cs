using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using Codeer.TestAssistant.GeneratorToolKit;
using RM.Friendly.WPFStandardControls.Inside;
using System.Windows;

namespace RM.Friendly.WPFStandardControls.Generator
{
    [CaptureCodeGenerator("RM.Friendly.WPFStandardControls.WPFMenuBase")]
    public class WPFMenuBaseGenerator : CaptureCodeGeneratorBase
    {
        Menu _control;
        delegate void DetachEvent();
        List<DetachEvent> _detach = new List<DetachEvent>();

        protected override void Attach()
        {
            _control = ControlObject as Menu;
            List<MenuItem> items = new List<MenuItem>();
            HeaderedItemsControlUtility.GetChildren(_control, items);
            foreach (var element in items)
            {
                var item = element;
                string text = HeaderedItemsControlUtility.GetItemText(item);
                if (string.IsNullOrEmpty(text))
                {
                    continue;
                }

                RoutedEventHandler opened = (s, e) =>
                {
                    SubmenuOpened(item, new string[] { text });
                };
                item.SubmenuOpened += opened;

                RoutedEventHandler click = (s, e) =>
                {
                    Click(item, new string[] { text });
                };
                item.Click += click;
                _detach.Add(() =>
                {
                    item.SubmenuOpened -= opened;
                    item.Click -= click;
                });
            }
        }

        protected override void Detach()
        {
            foreach (var element in _detach)
            {
                element();
            }
        }

        delegate void MyAction();

        void SubmenuOpened(MenuItem parentItem, string[] texts)
        {
            parentItem.Dispatcher.BeginInvoke((MyAction)delegate
            {
                List<MenuItem> items = new List<MenuItem>();
                HeaderedItemsControlUtility.GetChildren(parentItem, items);
                foreach (var element in items)
                {
                    var item = element;
                    string text = HeaderedItemsControlUtility.GetItemText(item);
                    if (string.IsNullOrEmpty(text))
                    {
                        continue;
                    }

                    List<string> nextTexts = new List<string>(texts);
                    nextTexts.Add(text);
                    RoutedEventHandler opened = (s, e) =>
                    {
                        SubmenuOpened(item, nextTexts.ToArray());
                    };
                    item.SubmenuOpened += opened;

                    RoutedEventHandler click = (s, e) =>
                    {
                        Click(item, nextTexts.ToArray());
                    };
                    item.Click += click;
                    RoutedEventHandler closed = null;
                    closed = (s, ee) =>
                    {
                        item.SubmenuOpened -= opened;
                        item.Click -= click;
                        parentItem.SubmenuClosed -= closed;
                    };
                    parentItem.SubmenuClosed += closed;
                }
            });
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

