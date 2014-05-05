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
    /// WPFListViewTestControl.xaml の相互作用ロジック
    /// </summary>
    public partial class WPFListViewTestControl : UserControl
    {
        public WPFListViewTestControl()
        {
            InitializeComponent();
            List<Item> list = new List<Item>();
            for (int i = 0; i < 100; i++)
            {
                list.Add(new Item() { Name = i.ToString(), Age = i });
            }
            listView.ItemsSource = list;
        }

        class Item
        {
            public string Name { get; set; }
            public int Age { get; set; }
        };
    }
}
