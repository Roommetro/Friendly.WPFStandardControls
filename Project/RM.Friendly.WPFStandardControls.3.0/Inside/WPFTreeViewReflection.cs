using System.Reflection;
using System.Windows.Controls;
using System.Windows.Data;

namespace RM.Friendly.WPFStandardControls.Inside
{
    internal static class WPFTreeViewReflection
    {
        private static PropertyInfo _itemsHostProperty = null;
        private static PropertyInfo _disconnectedSourceProperty = null;

        public static Panel GetItemsHostFor(TreeViewItem item)
        {
            var propertyInfo = _itemsHostProperty ?? (_itemsHostProperty = typeof(TreeViewItem).GetProperty(
                "ItemsHost", BindingFlags.NonPublic | BindingFlags.Instance));
            return (Panel)propertyInfo.GetValue(item, null);
        }

        public static object GetDisconnectedSource()
        {
            var propertyInfo = _disconnectedSourceProperty ?? (_disconnectedSourceProperty = typeof(BindingOperations).GetProperty(
                "DisconnectedSource", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic));
            return propertyInfo.GetValue(null, null);
        }
    }
}
