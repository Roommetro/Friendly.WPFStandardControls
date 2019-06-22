using System.CodeDom.Compiler;
using System.Windows;
using RM.Friendly.WPFStandardControls.Generator.CreateDriver;

namespace Target.CreateDriverTarget
{
    /// <summary>
    /// SingleWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class SingleWindow : Window
    {
        public bool CheckDom { get; set; } = true;

        public SingleWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!CheckDom) return;
            using (var dom = CodeDomProvider.CreateProvider("CSharp"))
            {
                new WPFDriverCreator(dom).CreateDriver(this);
            }

            Close();
        }
    }
}
