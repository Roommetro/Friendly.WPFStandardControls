using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace RM.Friendly.WPFStandardControls.Generator
{
    public class PointUtility
    {
        public static FrameworkElement GetPosElement(Point pos, object obj)
        {
            var visual = obj as Visual;
            if (visual == null) return null;

            var list = new List<FrameworkElement>();
            VisualTreeHelper.HitTest(visual, OnHitTestFilterCallback,
                result =>
                {
                    var element = result.VisualHit as FrameworkElement;
                    list.Add(element);
                    return HitTestResultBehavior.Continue;
                },
                new PointHitTestParameters(pos));

            //面積が最小のものを返す
            FrameworkElement min = null;
            double minArea = double.MaxValue;
            foreach (var e in list)
            {
                var area = e.ActualHeight * e.ActualWidth;
                if (area < minArea)
                {
                    min = e;
                    minArea = area;
                }
            }

            return min;
        }

        static HitTestFilterBehavior OnHitTestFilterCallback(DependencyObject target)
        {
            var element = target as FrameworkElement;
            if (element != null)
            {
                if (element.Visibility != Visibility.Visible
                    || element.Opacity <= 0)
                {
                    return HitTestFilterBehavior.ContinueSkipSelfAndChildren;
                }
            }
            else
            {
                return HitTestFilterBehavior.ContinueSkipSelf;
            }
            return HitTestFilterBehavior.Continue;
        }

    }
}
