using System.CodeDom.Compiler;
using System.Windows;
using RM.Friendly.WPFStandardControls.Generator.CreateDriver;

namespace Target.CreateDriverTarget
{
    /// <summary>
    /// UserControlWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class UserControlWindow : Window
    {
        public UserControlWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (var dom = CodeDomProvider.CreateProvider("CSharp"))
            {
                new WPFDriverCreator(dom).CreateDriver(this);
            }

            Close();
        }
    }
}
