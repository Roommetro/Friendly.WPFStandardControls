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
    /// SearchTestControl.xaml の相互作用ロジック
    /// </summary>
    public partial class SearchTestControl : UserControl
    {
        public SearchTestControl()
        {
            InitializeComponent();

            this.DataContext = new VM1();
            _grid.DataContext = new VM2();

            List<Item> list = new List<Item>();
            for (int i = 0; i < 3; i++)
            {
                list.Add(new Item() { Name = i.ToString(), Age = i });
            }
            _listView.ItemsSource = list;
        }

        class Item
        {
            public string Name { get; set; }
            public int Age { get; set; }
        };
    }

    public class VM1 : ViewModelBase
    {
        string _text;
        public string Text
        {
            get
            {
                return _text;
            }

            set
            {
                _text = value;
                RaisePropertyChanged("Text");
            }
        }

        DelegateCommand _button1;
        public DelegateCommand Button1Command
        {
            get
            {
                if (_button1 == null)
                {
                    _button1 = new DelegateCommand(() => { }, () => true);
                }

                return _button1;
            }
        }

        DelegateCommand _button2;
        public DelegateCommand Button2Command
        {
            get
            {
                if (_button2 == null)
                {
                    _button2 = new DelegateCommand(() => { }, () => true);
                }

                return _button2;
            }
        }
    }

    public class VM2 : ViewModelBase
    {
        string _text;
        public string Text
        {
            get
            {
                return _text;
            }

            set
            {
                _text = value;
                RaisePropertyChanged("Text");
            }
        }
    }
}
