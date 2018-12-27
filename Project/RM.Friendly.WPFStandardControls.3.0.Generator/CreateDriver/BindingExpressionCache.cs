using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;

namespace RM.Friendly.WPFStandardControls.Generator.CreateDriver
{
    internal class BindingExpressionCache
    {
        private readonly List<DependencyObject> _objs = new List<DependencyObject>();
        private readonly List<IEnumerable<BindingExpression>> _exps = new List<IEnumerable<BindingExpression>>();

        public IEnumerable<BindingExpression> GetBindingExpression(DependencyObject obj)
        {
            for (int i = 0; i < _objs.Count; i++)
            {
                if (ReferenceEquals(_objs[i], obj)) return _exps[i];
            }
            _objs.Add(obj);
            var exp = WPFUtility.GetBindingExpression(obj);
            _exps.Add(exp);
            return exp;
        }
    }
}
