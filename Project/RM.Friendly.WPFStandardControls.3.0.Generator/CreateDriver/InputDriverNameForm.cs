using System.Windows.Forms;

namespace RM.Friendly.WPFStandardControls.Generator.CreateDriver
{
    public partial class InputDriverNameForm : Form
    {
        public string DriverName => _textBoxName.Text;
        public InputDriverNameForm() => InitializeComponent();
    }
}