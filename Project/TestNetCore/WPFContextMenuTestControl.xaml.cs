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
    /// WPFContextMenuTestControl.xaml の相互作用ロジック
    /// </summary>
    public partial class WPFContextMenuTestControl : UserControl
    {
        public WPFContextMenuTestControl()
        {
            InitializeComponent();

            _list1.ItemsSource = new[] { "a", "b", "c" };
            _list2.ItemsSource = new[] { "a", "b", "c" };
            _list3.ItemsSource = new[] { "a", "b", "c" };
            _isModal = false;
            _isModal.ToString();
        }

        bool _isModal;
        List<string> _commands = new List<string>();
        private void Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var c = e.Command as RoutedUICommand;
            _commands.Add(c.Name);
            if (_isModal) 
            {
                MessageBox.Show("");
            }
        }

        private void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void CanExecuteFalse(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = false;
        }
    }
}
