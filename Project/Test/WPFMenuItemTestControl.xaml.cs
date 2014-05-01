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
    /// WPFMenuItemTest.xaml の相互作用ロジック
    /// </summary>
    public partial class WPFMenuItemTestControl : UserControl
    {
        public WPFMenuItemTestControl()
        {
            InitializeComponent();
        }

        bool menuItemClicked;

        private void _menuItem_Click(object sender, RoutedEventArgs e)
        {
            menuItemClicked = true;
            menuItemClicked.ToString();
        }

        private void _menuItemMessage_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("");
        }

        bool menuButtonClicked;

        private void _button_Click(object sender, RoutedEventArgs e)
        {
            menuButtonClicked = true;
            menuButtonClicked.ToString();
        }
    }
}
