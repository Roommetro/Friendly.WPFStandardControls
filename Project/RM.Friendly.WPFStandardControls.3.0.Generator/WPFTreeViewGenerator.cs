using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using Codeer.TestAssistant.GeneratorToolKit;
using RM.Friendly.WPFStandardControls.Inside;
using System.Windows;

namespace RM.Friendly.WPFStandardControls.Generator
{
    [CaptureCodeGenerator("RM.Friendly.WPFStandardControls.WPFTreeView")]
    public class WPFTreeViewGenerator : CaptureCodeGeneratorBase
    {
        TreeView _control;
        delegate void DetachEvent();
        List<DetachEvent> _detach = new List<DetachEvent>();
        List<TreeViewItem> _attachedItems = new List<TreeViewItem>();

        protected override void Detach()
        {
            foreach (var e in _detach)
            {
                e();
            }
        }

        protected override void Attach()
        {
            _control = ControlObject as TreeView;
            AttachChildren(new string[0], _control);
        }

        void AttachChildren(string[] texts, ItemsControl itemsControl)
        {
            //仮想化対応 50ミリ周期でイベントハンドリングしていないTreeViewItemを監視する
            //初回はすぐに実行されるようにする
            var timer = new System.Windows.Forms.Timer { Interval = 1 };
            int[] next = null;

            System.Windows.Forms.MethodInvoker eventConnection = () =>
            {
                timer.Interval = 50;

                //子要素取得
                var exists = GetTreeChildren(itemsControl, next, out var notExists);
                foreach (var item in exists)
                {
                    string text = HeaderedItemsControlUtility.GetItemText(item);

                    List<string> nextTexts = new List<string>(texts);
                    nextTexts.Add(text);
                    EventConnection(item, false, nextTexts.ToArray());
                }

                //仮想化でまだ存在していない要素があればそれを監視させる
                next = notExists;

                //全てアタッチしたら終了
                if (next.Length == 0)
                {
                    timer?.Stop();
                }
            };

            //タイマ処理
            timer.Tick += (_, __) =>
            {
                try
                {
                    eventConnection();
                }
                catch
                {
                    timer.Stop();
                }
            };
            timer.Start();
        }

        void EventConnection(TreeViewItem item, bool isInEvent, string[] texts)
        {
            //既に処理した要素は無視
            if (_attachedItems.Contains(item)) return;
            _attachedItems.Add(item);

            if (isInEvent)
            {
            }

            //Expandedイベント
            RoutedEventHandler opened = (s, e) =>
            {
                if (!ReferenceEquals(e.Source, item)) return;
                if (HasFocus())
                {
                    AddSentence(new TokenName(), ".GetItem(" + MakeGetArgs(texts) + ").EmulateChangeExpanded(true",
                        new TokenAsync(CommaType.Before), ");");
                }
                AttachChildren(texts, item);
            };
            item.Expanded += opened;

            //選択イベント
            RoutedEventHandler selected = (s, e) =>
            {
                SelectedChanged(item, texts);
            };
            item.Selected += selected;

            RoutedEventHandler closedForGenerate = (s, ee) =>
            {
                Collapsed(item, texts);
            };
            item.Collapsed += closedForGenerate;

            //イベント削除用に記憶
            _detach.Add(() =>
            {
                item.Expanded -= opened;
                item.Selected -= selected;
                item.Collapsed -= closedForGenerate;
            });

            //開いている場合は子要素にも同様の処理を行う
            if (item.IsExpanded)
            {
                AttachChildren(texts, item);
            }
        }

        void Collapsed(TreeViewItem item, string[] texts)
        {
            if (item.IsExpanded) return;
            if (!HasFocus()) return;

            AddSentence(new TokenName(), ".GetItem(" + MakeGetArgs(texts) + ").EmulateChangeExpanded(false",
              new TokenAsync(CommaType.Before), ");");
        }

        void SelectedChanged(TreeViewItem item, string[] texts)
        {
            if (!item.IsFocused) return;
            AddSentence(new TokenName(), ".GetItem(" + MakeGetArgs(texts) + ").EmulateChangeSelected(",
                (item.IsSelected ? "true" : "false"),
                new TokenAsync(CommaType.Before), ");");
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

        static TreeViewItem[] GetTreeChildren(ItemsControl control, int[] idnexes, out int[] notExists)
        {
            var listNotExists = new List<int>();
            var list = new List<TreeViewItem>();
            if (idnexes == null)
            {
                var allIndexes = new List<int>();
                for (int i = 0; i < control.Items.Count; i++)
                {
                    allIndexes.Add(i);
                }
                idnexes = allIndexes.ToArray();
            }
            foreach (var i in idnexes)
            {
                var item = control.ItemContainerGenerator.ContainerFromIndex(i) as TreeViewItem;
                if (item == null)
                {
                    listNotExists.Add(i);
                }
                else
                {
                    list.Add(item);
                }
            }
            notExists = listNotExists.ToArray();
            return list.ToArray();
        }

        bool HasFocus()
        {
            foreach (var e in TreeUtilityInTarget.VisualTree(_control))
            {
                var uielement = e as UIElement;
                if (uielement == null) continue;
                if (uielement.IsFocused) return true;
            }
            return false;
        }
    }
}

