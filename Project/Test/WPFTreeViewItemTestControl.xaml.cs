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
    /// WPFTreeViewItemTestControl.xaml の相互作用ロジック
    /// </summary>
    public partial class WPFTreeViewItemTestControl : UserControl
    {
        public WPFTreeViewItemTestControl()
        {
            InitializeComponent();
            _item1.Selected += delegate { MessageBox.Show(""); };
            _item2.Expanded += delegate { MessageBox.Show(""); };
        }

        bool treeButtonClicked;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            treeButtonClicked = true;
            treeButtonClicked.ToString();
        }
    }
}
