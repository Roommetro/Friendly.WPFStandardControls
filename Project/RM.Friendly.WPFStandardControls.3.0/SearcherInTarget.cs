using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

namespace RM.Friendly.WPFStandardControls
{
#if ENG
    /// <summary>
    /// Search by binding.
    /// </summary>
#else
    /// <summary>
    /// Binding情報から要素を取得するためのユーティリティー
    /// </summary>
#endif
    public static class SearcherInTarget
    {
#if ENG
        /// <summary>
        /// Search by binding from DependencyObject collection.
        /// </summary>
        /// <param name="collection">DependencyObject collection.</param>
        /// <param name="path">Binding path.</param>
        /// <param name="dataItem">DataItem.</param>
        /// <returns>Hit elements.</returns>
#else
        /// <summary>
        /// Binding情報から要素を検索。
        /// </summary>
        /// <param name="collection">DependencyObjectのコレクション。</param>
        /// <param name="path">バインディングパス。</param>
        /// <param name="dataItem">DataItem。</param>
        /// <returns>ヒットした要素。</returns>
#endif
        public static IEnumerable<DependencyObject> ByBinding(IEnumerable<DependencyObject> collection, string path, object dataItem = null)
        {
            List<DependencyObject> list = new List<DependencyObject>();
            foreach (var e in collection)
            {
                if (IsMatch(e, path, dataItem))
                {
                    list.Add(e);
                }
            }
            return list;
        }

#if ENG
        /// <summary>
        /// Search by Type from DependencyObject collection.
        /// </summary>
        /// <typeparam name="T">Target type.</typeparam>
        /// <param name="collection">DependencyObject collection.</param>
        /// <returns>Hit elements.</returns>
#else
        /// <summary>
        /// タイプから要素を検索。
        /// </summary>
        /// <typeparam name="T">検索対象のタイプ。</typeparam>
        /// <param name="collection">DependencyObjectのコレクション。</param>
        /// <returns>ヒットした要素。</returns>
#endif
        public static IEnumerable<DependencyObject> ByType<T>(IEnumerable<DependencyObject> collection)
        {
            return ByType(collection, typeof(T).FullName);
        }

#if ENG
        /// <summary>
        /// Search by Type from DependencyObject collection.
        /// </summary>
        /// <param name="collection">DependencyObject collection.</param>
        /// <param name="typeFullName">Target type.</param>
        /// <returns>Hit elements.</returns>
#else
        /// <summary>
        /// タイプから要素を検索。
        /// </summary>
        /// <param name="collection">DependencyObjectのコレクション。</param>
        /// <param name="typeFullName">検索対象のタイプ。</param>
        /// <returns>ヒットした要素。</returns>
#endif
        public static IEnumerable<DependencyObject> ByType(IEnumerable<DependencyObject> collection, string typeFullName)
        {
            List<DependencyObject> list = new List<DependencyObject>();
            foreach (var e in collection)
            {
                if (e.GetType().FullName == typeFullName)
                {
                    list.Add(e);
                }
            }
            return list;
        }

        static bool IsMatch(DependencyObject obj, string path, object dataItem = null)
        {
            foreach (var property in GetDependencyProperties(obj))
            {
                var binding = BindingOperations.GetBindingExpression(obj, property);
                if (binding == null)
                {
                    continue;
                }
                if (binding.ParentBinding.Path.Path != path)
                {
                    continue;
                }
                if (dataItem == null)
                {
                    return true;
                }
                if (ReferenceEquals(binding.DataItem, dataItem))
                {
                    return true;
                }
            }
            return false;
        }

        static IEnumerable<DependencyProperty> GetDependencyProperties(object obj)
        {
            List<DependencyProperty> list = new List<DependencyProperty>();
            PropertyDescriptorCollection propertyDescriptors =
                TypeDescriptor.GetProperties(obj, new Attribute[] { new PropertyFilterAttribute(PropertyFilterOptions.All) });
            foreach (PropertyDescriptor property in propertyDescriptors)
            {
                DependencyPropertyDescriptor dpd = DependencyPropertyDescriptor.FromProperty(property);
                if (dpd != null)
                {
                    list.Add(dpd.DependencyProperty);
                }
            }
            return list;
        }
    }
}
