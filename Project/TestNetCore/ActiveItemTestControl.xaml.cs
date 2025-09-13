using System.Collections.Generic;
using System.Windows.Controls;

namespace Test
{
    public partial class ActiveItemTestControl : UserControl
    {
        public class Person
        {
            public string Name { get; set; }
            public List<Person> Children { get; set; }
        }

        public ActiveItemTestControl()
        {
            InitializeComponent();

            _listBox.ItemsSource = new[]
            {
                new Person { Name = "Tanaka"},
                new Person { Name = "Satou"},
            };

            _listView.ItemsSource = new[]
            {
                new Person { Name = "Tanaka"},
                new Person { Name = "Satou"},
            };

            _treeView.ItemsSource = new List<Person>
            {
                new Person
                {
                    Name = "Tarou Tanaka",
                    Children = new List<Person>
                    {
                        new Person { Name = "Hanako Tanaka" },
                        new Person { Name = "Ichiro Tanaka" },
                        new Person
                        {
                            Name = "Kantarou Kimura",
                            Children = new List<Person>
                            {
                                new Person { Name = "Hana Kimura" },
                                new Person { Name = "Ume Kimura" },
                            }
                        }
                    }
                },
                new Person
                {
                    Name = "Jirou Tanaka",
                    Children = new List<Person>
                    {
                        new Person { Name = "Saburou Tanaka" }
                    }
                }
            };
        }
    }
}
