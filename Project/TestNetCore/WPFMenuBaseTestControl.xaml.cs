using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// WPFMenuBaseTest.xaml の相互作用ロジック
    /// </summary>
    public partial class WPFMenuBaseTestControl : UserControl, ICommand
    {
        public WPFMenuBaseTestControl()
        {
            InitializeComponent();
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged.ToString();
            }
            _menu.ItemsSource = new MenuItemsDefine().ItemData;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        string executeCommand;
        public void Execute(object parameter)
        {
            executeCommand = parameter.ToString();
        }
    }

    public class ItemData : ObservableCollection<ItemData>
    {
        public ItemData()
        {
        }
        public ItemData(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
        public string Command { get { return Name; } }

        public ItemData Children { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public class MenuItemsDefine
    {
        public ItemData ItemData { get; set; }
        public MenuItemsDefine()
        {
            ItemData = new ItemData() {
            new ItemData("0") {
                Children = new ItemData {
                    new ItemData("0-0") {
                        Children = new ItemData {
                            new ItemData("0-0-0"),
                            new ItemData("0-0-1"),
                            new ItemData("0-0-2"),
                            new ItemData("0-0-3"),
                        }
                    },
                    new ItemData("0-1"),
                    new ItemData("0-2")
                }
            },
            new ItemData("1") {
                Children = new ItemData {
                    new ItemData("1-0") {
                        Children = new ItemData {
                            new ItemData("1-0-0"),
                            new ItemData("1-0-1"),
                            new ItemData("1-0-2"),
                            new ItemData("1-0-3"),
                        }
                    },
                    new ItemData("1-1"),
                    new ItemData("1-2"),
                    new ItemData("1-3"),
                    new ItemData("1-4"),
                }
            }
        };
        }
    }
}
