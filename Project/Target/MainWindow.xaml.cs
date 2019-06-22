using System.Windows;
using System.Windows.Input;
using Target.CreateDriverTarget;

namespace Target
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.Key == Key.Q && (Keyboard.GetKeyStates(Key.LeftCtrl) & KeyStates.Down) == KeyStates.Down)
            {
                new SingleWindow() { CheckDom = false }.ShowDialog();
            }
        }
    }
}
