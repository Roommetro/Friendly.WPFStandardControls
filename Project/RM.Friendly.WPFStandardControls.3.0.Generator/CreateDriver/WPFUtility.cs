using Codeer.TestAssistant.GeneratorToolKit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace RM.Friendly.WPFStandardControls.Generator.CreateDriver
{
    //TODO 多分いらないコードが多い
    internal static class WPFUtility
    {
        public static List<DependencyObject> GetVisualTreeDescendants(DependencyObject obj, bool stopWindowOrUserControl, int index)
        {
            var list = new List<DependencyObject> { obj };

            if (stopWindowOrUserControl && (0 < index) && ((obj is UserControl) || (obj is Page) || (obj is Window))) return list;
            var info = DriverCreatorUtils.GetDriverInfo(obj, DriverCreatorAdapter.TypeFullNameAndControlDriver);
            if ((info != null) && !info.SearchDescendantUserControls) return list;

            index++;
            int count = VisualTreeHelper.GetChildrenCount(obj);
            for (int i = 0; i < count; i++)
            {
                list.AddRange(GetVisualTreeDescendants(VisualTreeHelper.GetChild(obj, i), stopWindowOrUserControl, index));
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

        public static List<DependencyObject> GetLogicalTreeDescendants(DependencyObject obj, bool stopWindowOrUserControl, int index)
        {
            var list = new List<DependencyObject> { obj };

            if (stopWindowOrUserControl && (0 < index) && ((obj is UserControl) || (obj is Page) || (obj is Window))) return list;
            var info = DriverCreatorUtils.GetDriverInfo(obj, DriverCreatorAdapter.TypeFullNameAndControlDriver);
            if (info != null && !info.SearchDescendantUserControls) return list;

            index++;
            foreach (var e in LogicalTreeHelper.GetChildren(obj))
            {
                if (e is DependencyObject d)
                {
                    list.AddRange(GetLogicalTreeDescendants(d, stopWindowOrUserControl, index));
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
            var children = GetLogicalTreeDescendants(root, false, 0);
            foreach (var e in GetVisualTreeDescendants(root, false, 0))
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

        private static IEnumerable<DependencyProperty> GetDependencyProperties(object obj)
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
    }
}
