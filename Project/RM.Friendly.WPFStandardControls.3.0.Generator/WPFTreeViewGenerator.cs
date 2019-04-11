using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using Codeer.TestAssistant.GeneratorToolKit;
using RM.Friendly.WPFStandardControls.Inside;
using System.Windows;
using System.Windows.Media;

namespace RM.Friendly.WPFStandardControls.Generator
{
    [CaptureCodeGenerator("RM.Friendly.WPFStandardControls.WPFTreeView")]
    public class WPFTreeViewGenerator : CaptureCodeGeneratorBase
    {
        TreeView _control;
        delegate void DetachEvent();
        List<DetachEvent> _detach = new List<DetachEvent>();

        protected override void Attach()
        {
            _control = ControlObject as TreeView;
            List<TreeViewItem> items = new List<TreeViewItem>();
            HeaderedItemsControlUtility.GetChildren(_control, items);
            
            foreach (var element in GetTreeChildren(_control, 0))
            {
                var item = element;
                string text = HeaderedItemsControlUtility.GetItemText(item);
                if (string.IsNullOrEmpty(text))
                {
                    continue;
                }

                RoutedEventHandler opened = (s, e) =>
                {
                    if (!ReferenceEquals(e.Source, item)) return;
                    Expanded(item, true, new string[] { text });
                };
                item.Expanded += opened;

                RoutedEventHandler click = (s, e) =>
                {
                    SelectedChanged(item, new string[] { text });
                };
                item.Selected += click;

                _detach.Add(() =>
                {
                    item.Expanded -= opened;
                    item.Selected -= click;
                });

                if (item.IsExpanded)
                {
                    Expanded(item, false, new string[] { text });
                }
            }
        }

        protected override void Detach()
        {
            foreach (var element in _detach)
            {
                element();
            }
        }

        static IEnumerable<TreeViewItem> GetTreeChildren(DependencyObject control, int index)
        {
            var list = new List<TreeViewItem>();
            if (index != 0)
            {
                var item = control as TreeViewItem;
                if (item != null)
                {
                    list.Add(item);
                    return list;
                }
            }
            int count = VisualTreeHelper.GetChildrenCount(control);
            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(control, i);
                if (child == null) continue;
                list.AddRange(GetTreeChildren(child, index + 1));
            }
            return list;
        }

        void Collapsed(TreeViewItem item, string[] texts)
        {
            AddSentence(new TokenName(), ".GetItem(" + MakeGetArgs(texts) + ").EmulateChangeExpanded(false",
              new TokenAsync(CommaType.Non), ");");
        }

        void Expanded(TreeViewItem parentItem, bool isInEvent, string[] texts)
        {
            if (isInEvent)
            {
                AddSentence(new TokenName(), ".GetItem(" + MakeGetArgs(texts) + ").EmulateChangeExpanded(true",
                  new TokenAsync(CommaType.Non), ");");
            }

            RoutedEventHandler closedForGenerate = null;
            closedForGenerate = (s, ee) =>
            {
                Collapsed(parentItem, texts);
                parentItem.Collapsed -= closedForGenerate;
            };
            parentItem.Collapsed += closedForGenerate;

            //念のためにディタッチ時に全部削除
            _detach.Add(() =>
            {
                parentItem.Collapsed -= closedForGenerate;
            });

            System.Windows.Forms.MethodInvoker eventConnection = () =>
            {
                foreach (var element in GetTreeChildren(parentItem, 0))
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
                        if (!ReferenceEquals(e.Source, item)) return;
                        Expanded(item, true, nextTexts.ToArray());
                    };
                    item.Expanded += opened;

                    RoutedEventHandler click = (s, e) =>
                    {
                        SelectedChanged(item, nextTexts.ToArray());
                    };
                    item.Selected += click;

                    RoutedEventHandler closed = null;
                    closed = (s, ee) =>
                    {
                        item.Expanded -= opened;
                        item.Selected -= click;
                        parentItem.Collapsed -= closed;
                    };
                    parentItem.Collapsed += closed;
                    
                    //念のためにディタッチ時に全部削除
                    _detach.Add(() =>
                    {
                        item.Expanded -= opened;
                        item.Selected -= click;
                        parentItem.Collapsed -= closed;
                    });

                }
            };

            if (isInEvent)
            {
                var timer = new System.Windows.Forms.Timer { Interval = 1 };
                timer.Tick += (_, __) =>
                {
                    eventConnection();
                    timer.Stop();
                };
                timer.Start();
            }
            else
            {
                eventConnection();
            }
        }

        void SelectedChanged(TreeViewItem item, string[] texts)
        {
            if (!item.IsFocused) return;
            AddSentence(new TokenName(), ".GetItem(" + MakeGetArgs(texts) + ").EmulateChangeSelected(",
                (item.IsSelected ? "true" : "false"),
                new TokenAsync(CommaType.Non), ");");
        }

        static string MakeGetArgs(string[] texts)
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

