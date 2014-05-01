using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace RM.Friendly.WPFStandardControls.Inside
{
    static class HeaderedItemsControlUtility
    {
        internal delegate void ShowNextItem<TItem>(TItem item) where TItem : Visual;
        delegate TItem GetItemDelegate<TItem, TCondition>(Visual v, TCondition condition) where TItem : Visual;
        delegate bool IsMatch<TItem>(TItem v) where TItem : Visual;
        delegate TItem Next<TItem>(Visual v) where TItem : Visual;

        internal static TItem GetItem<TItem>(Visual parent, string[] headerTexts, ShowNextItem<TItem> showNextItem) where TItem : Visual
        {
            GetItemDelegate<TItem, string> getItem = (visual, headerText) =>
            {
                IsMatch<TItem> isMatch = (item) => (GetItemText(item) == headerText);
                Next<TItem> next = null;
                next = (v) => GetItemCore(v, isMatch, next);
                return GetItemCore(visual, isMatch, next);
            };
            return GetItem<TItem, string>(parent, headerTexts, getItem, showNextItem);
        }

        internal static TItem GetItem<TItem>(Visual parent, int[] indices, ShowNextItem<TItem> showNextItem) where TItem : Visual
        {
            GetItemDelegate<TItem, int> getItem = (visual, index) =>
            {
                int currentIndex = 0;
                IsMatch<TItem> isMatch = (v) => (currentIndex++ == index);
                Next<TItem> next = null;
                next = (v) => GetItemCore(v, isMatch, next);
                return GetItemCore(visual, isMatch, next);
            };
            return GetItem<TItem, int>(parent, indices, getItem, showNextItem);
        }

        internal static string GetItemText(Visual item)
        {
            TextBlock text = GetCoreElement(item, typeof(TextBlock).FullName) as TextBlock;
            return (text == null) ? string.Empty : text.Text;
        }

        internal static Visual GetCoreElement(Visual visual, string typeFullName)
        {
            foreach (var v in VisualTreeUtility.GetChildren(visual))
            {
                if (v.GetType().FullName == typeFullName)
                {
                    return v;
                }
                Visual o = GetCoreElement(v, typeFullName);
                if (o != null)
                {
                    return o;
                }
            }
            return null;
        }

        static TItem GetItem<TItem, T>(Visual parent, T[] indices, GetItemDelegate<TItem, T> getItem, ShowNextItem<TItem> showNextItem)
            where TItem : Visual
        {
            Visual v = parent;
            for (int i = 0; i < indices.Length; i++)
            {
                var item = getItem(v, indices[i]);
                if (item == null)
                {
                    throw new NotSupportedException(ResourcesLocal3.Instance.ErrorNotFoundItem);
                }
                if (i == indices.Length - 1)
                {
                    return item;
                }
                showNextItem(item);
                v = item;
            }
            return null;
        }

        static TItem GetItemCore<TItem>(Visual visual, IsMatch<TItem> isMatch, Next<TItem> next)
            where TItem : Visual
        {
            foreach (var element in VisualTreeUtility.GetChildren(visual))
            {
                Visual o = element;
                var item = o as TItem;
                if (item != null)
                {
                    if (isMatch(item))
                    {
                        return item;
                    }
                }
                else
                {
                    var popup = o as Popup;
                    if (popup != null)
                    {
                        o = popup.Child as Visual;
                    }
                    var nextItem = next(o);
                    if (nextItem != null)
                    {
                        return nextItem;
                    }
                }
            }
            return null;
        }
    }
}
