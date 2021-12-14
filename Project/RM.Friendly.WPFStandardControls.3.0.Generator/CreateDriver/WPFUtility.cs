using Codeer.TestAssistant.GeneratorToolKit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;

namespace RM.Friendly.WPFStandardControls.Generator.CreateDriver
{
    //TODO 多分いらないコードが多い
    //リファクタリングが必要
    internal static class WPFUtility
    {
        public static List<DependencyObject> GetVisualTreeDescendants(DependencyObject obj, bool stopWindowOrUserControl, bool stopControlDriver, int index)
        {
            if (obj == null) return new List<DependencyObject>();
            var list = new List<DependencyObject> { obj };
            if (index != 0 && IsStopSearch(obj, stopWindowOrUserControl, stopControlDriver, index)) return list;

            index++;
            int count = VisualTreeHelper.GetChildrenCount(obj);
            for (int i = 0; i < count; i++)
            {
                list.AddRange(GetVisualTreeDescendants(VisualTreeHelper.GetChild(obj, i), stopWindowOrUserControl, stopControlDriver, index));
            }
            var popup = obj as Popup;
            if (popup != null && popup.Child != null)
            {
                list.AddRange(GetVisualTreeDescendants(popup.Child, stopWindowOrUserControl, stopControlDriver, 0));
            }
            var type = obj.GetType();
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty);
            foreach (PropertyInfo p in properties)
            {
                if (p.PropertyType == typeof(ContextMenu))
                {
                    var contextMenuTmp = p.GetValue(obj, null) as ContextMenu;
                    if (contextMenuTmp != null)
                    {
                        list.Add(contextMenuTmp);
                        for (int i = 0; i < contextMenuTmp.Items.Count; i++)
                        {
                            var child = contextMenuTmp.Items[i] as MenuItem;
                            if (child == null)
                            {
                                continue;
                            }
                            list.AddRange(GetVisualTreeDescendants(child, stopWindowOrUserControl, stopControlDriver, 0));
                        }
                    }
                }
            }
            return list;
        }

        public static List<DependencyObject> GetVisualTreeAncestor(DependencyObject obj)
        {
            var list = new List<DependencyObject>();
            while (obj != null)
            {
                list.Add(obj);
                obj = VisualTreeHelper.GetParent(obj);
            }
            return list;
        }

        public static List<DependencyObject> GetLogicalTreeDescendants(DependencyObject obj, bool stopWindowOrUserControl, bool stopControlDriver, int index)
        {
            var list = new List<DependencyObject> { obj };
            if (index != 0 && IsStopSearch(obj, stopWindowOrUserControl, stopControlDriver, index)) return list;

            if (obj is Expander expander)
            {
                obj = expander.Content as DependencyObject;
                if (obj == null) return list;
            }

            index++;
            foreach (var e in LogicalTreeHelper.GetChildren(obj))
            {
                if (e is DependencyObject d)
                {
                    list.AddRange(GetLogicalTreeDescendants(d, stopWindowOrUserControl, stopControlDriver, index));
                }
            }
            return list;
        }

        public static ICollection<BindingExpression> GetBindingExpression(DependencyObject obj)
        {
            var list = new List<BindingExpression>();
            foreach (var property in GetDependencyProperties(obj))
            {
                var binding = BindingOperations.GetBindingExpression(obj, property);
                if (binding != null) list.Add(binding);
            }
            return list;
        }

        public static BindingExpression GetMatchBindingExpression(IEnumerable<BindingExpression> exps, string path)
        {
            foreach (var binding in exps)
            {
                if (binding == null) continue;
                if (binding.ParentBinding.Path.Path == path) return binding;
            }
            return null;
        }

        public static bool ExistMany(DependencyObject root, Type type)
        {
            var children = GetLogicalTreeDescendants(root, false, true, 0);
            foreach (var e in GetVisualTreeDescendants(root, false, true, 0))
            {
                if (!CollectionUtility.HasReference(children, e)) children.Add(e);
            }
            int count = 0;
            foreach (var c in children)
            {
                if (type == c.GetType()) count++;
            }
            return 1 < count;
        }

        static IEnumerable<DependencyProperty> GetDependencyProperties(object obj)
        {
            var list = new List<DependencyProperty>();
            var propertyDescriptors = TypeDescriptor.GetProperties(obj, new Attribute[] { new PropertyFilterAttribute(PropertyFilterOptions.All) });
            foreach (PropertyDescriptor property in propertyDescriptors)
            {
                var dpd = DependencyPropertyDescriptor.FromProperty(property);
                if (dpd != null)
                {
                    list.Add(dpd.DependencyProperty);
                }
            }
            return list;
        }

        static bool IsStopSearch(DependencyObject obj, bool stopWindowOrUserControl, bool stopControlDriver, int index)
        {
            if (stopWindowOrUserControl && (0 < index))
            {
                if (((obj is UserControl) || (obj is Page) || (obj is Window))) return true;
                var info = DriverCreatorUtils.GetDriverInfo(obj, DriverCreatorAdapter.TypeFullNameAndUserControlDriver);
                if (info != null) return true;
            }

            if (stopControlDriver)
            {
                var info = DriverCreatorUtils.GetDriverInfo(obj, DriverCreatorAdapter.TypeFullNameAndControlDriver);
                if (info != null && !info.SearchDescendantUserControls) return true;
            }
            return false;
        }
    }
}
