using System.CodeDom.Compiler;
using System.Windows;
using RM.Friendly.WPFStandardControls.Generator.CreateDriver;

namespace Target.CreateDriverTarget
{
    /// <summary>
    /// TabUserControlWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class TabUserControlWindow : Window
    {
        public TabUserControlWindow()
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
