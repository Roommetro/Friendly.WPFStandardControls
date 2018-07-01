using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using Codeer.TestAssistant.GeneratorToolKit;
using RM.Friendly.WPFStandardControls.Inside;
using System.Windows;

namespace RM.Friendly.WPFStandardControls.Generator
{
    [Generator("RM.Friendly.WPFStandardControls.WPFTreeView")]
    public class WPFTreeViewGenerator : GeneratorBase
    {
        TreeView _control;
        delegate void DetachEvent();
        List<DetachEvent> _detach = new List<DetachEvent>();

        protected override void Attach()
        {
            _control = ControlObject as TreeView;
            List<TreeViewItem> items = new List<TreeViewItem>();
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
                    Expanded(item, new string[] { text });
                };
                item.Expanded += opened;

                RoutedEventHandler click = (s, e) =>
                {
                    SelectedChanged(item, new string[] { text });
                };
                item.Selected += click;
                item.Unselected += click;

                _detach.Add(() =>
                {
                    item.Expanded -= opened;
                    item.Selected -= click;
                    item.Unselected -= click;
                });
            }
        }

        private void Collapsed(TreeViewItem item, string[] texts)
        {
            AddSentence(new TokenName(), ".GetItem(" + MakeGetArgs(texts) + ").EmulateChangeExpanded(false",
              new TokenAsync(CommaType.Non), ");");

        }

        protected override void Detach()
        {
            foreach (var element in _detach)
            {
                element();
            }
        }

        delegate void MyAction();

        void Expanded(TreeViewItem parentItem, string[] texts)
        {
            AddSentence(new TokenName(), ".GetItem(" + MakeGetArgs(texts) + ").EmulateChangeExpanded(true",
              new TokenAsync(CommaType.Non), ");");

            RoutedEventHandler closedForGenerate = null;
            closedForGenerate = (s, ee) =>
            {
                Collapsed(parentItem, texts);
                parentItem.Collapsed -= closedForGenerate;
            };
            parentItem.Collapsed += closedForGenerate;

            parentItem.Dispatcher.BeginInvoke((MyAction)delegate
            {
                List<TreeViewItem> items = new List<TreeViewItem>();
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
                        Expanded(item, nextTexts.ToArray());
                    };
                    item.Expanded += opened;

                    RoutedEventHandler click = (s, e) =>
                    {
                        SelectedChanged(item, nextTexts.ToArray());
                    };
                    item.Selected += click;
                    item.Unselected += click;


                    RoutedEventHandler closed = null;
                    closed = (s, ee) =>
                    {
                        item.Expanded -= opened;
                        item.Selected -= click;
                        item.Unselected -= click;
                        parentItem.Collapsed -= closed;
                    };
                    parentItem.Collapsed += closed;
                }
            });
        }

        void SelectedChanged(TreeViewItem item, string[] texts)
        {
            AddSentence(new TokenName(), ".GetItem(" + MakeGetArgs(texts) + ").EmulateChangeSelected(",
                (item.IsSelected ? "true" : "false"),
                new TokenAsync(CommaType.Non), ");");
        }

        private static string MakeGetArgs(string[] texts)
        {
            StringBuilder getArgs = new StringBuilder();
            foreach (var element in texts)
            {
                if (0 < getArgs.Length)
                {
                    getArgs.Append(", ");
                }
                getArgs.Append(GenerateUtility.ToLiteral(element));
            }
            return getArgs.ToString();
        }
    }
}

