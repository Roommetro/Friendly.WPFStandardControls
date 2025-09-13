using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Test
{
    /// <summary>
    /// VisualTreeWithPopupTestControl.xaml の相互作用ロジック
    /// </summary>
    public partial class VisualTreeWithPopupTestControl : UserControl
    {
        public VisualTreeWithPopupTestControl()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _popup1.IsOpen = !_popup1.IsOpen;
        }
    }
}
