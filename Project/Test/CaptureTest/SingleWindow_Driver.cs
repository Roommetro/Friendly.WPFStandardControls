using Codeer.Friendly.Dynamic;
using Codeer.Friendly;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using Codeer.TestAssistant.GeneratorToolKit;
using RM.Friendly.WPFStandardControls;
using System.Windows.Controls;

namespace Test.CaptureTest
{
    [WindowDriver(TypeFullName = "Target.CreateDriverTarget.SingleWindow")]
    public class SingleWindow_Driver
    {
        public WindowControl Core { get; }
        public WPFToggleButton ToggleButton => new WPFToggleButton(Core.VisualTree().ByType("System.Windows.Controls.Primitives.ToggleButton").ByBinding("IsOverflowOpen").Single());
        public WPFMenuBase Menu1 => new WPFMenuBase(Core.Dynamic().Menu1);
        public WPFButtonBase Button2 => new WPFButtonBase(Core.Dynamic().Button2);
        public WPFToggleButton ToggleButton1 => new WPFToggleButton(Core.Dynamic().ToggleButton1);
        public WPFButtonBase Button1 => new WPFButtonBase(Core.Dynamic().Button1);
        public WPFToggleButton CheckBox1 => new WPFToggleButton(Core.Dynamic().CheckBox1);
        public WPFToggleButton RadioButton1 => new WPFToggleButton(Core.Dynamic().RadioButton1);
        public WPFToggleButton RadioButton2 => new WPFToggleButton(Core.Dynamic().RadioButton2);
        public WPFComboBox ComboBox1 => new WPFComboBox(Core.Dynamic().ComboBox1);
        public WPFComboBox ComboBox2 => new WPFComboBox(Core.Dynamic().ComboBox2);
        public WPFListBox ListBox1 => new WPFListBox(Core.Dynamic().ListBox1);
        public WPFListView ListView1 => new WPFListView(Core.Dynamic().ListView1);
        public WPFTreeView TreeView1 => new WPFTreeView(Core.VisualTree().ByType<TreeView>().Single());
        public WPFExpander Expander1 => new WPFExpander(Core.Dynamic().Expander1);
        public WPFComboBox ComboInExpander => new WPFComboBox(Core.Dynamic()._comboInExpander);
        public WPFPasswordBox PasswordBox1 => new WPFPasswordBox(Core.Dynamic().PasswordBox1);
        public WPFProgressBar ProgressBar1 => new WPFProgressBar(Core.Dynamic().ProgressBar1);
        public WPFRichTextBox RichTextBox1 => new WPFRichTextBox(Core.Dynamic().RichTextBox1);
        public WPFSlider Slider1 => new WPFSlider(Core.Dynamic().Slider1);
        public WPFTextBox TextBox1 => new WPFTextBox(Core.Dynamic().TextBox1);
        public WPFDatePicker DatePicker1 => new WPFDatePicker(Core.Dynamic().DatePicker1);
        public WPFDataGrid DataGrid1 => new WPFDataGrid(Core.Dynamic().DataGrid1);
        public WPFTabControl TabControl1 => new WPFTabControl(Core.Dynamic().TabControl1);
        public WPFCalendar Calendar1 => new WPFCalendar(Core.Dynamic().Calendar1);

        public SingleWindow_Driver(WindowControl core)
        {
            Core = core;
        }
    }

    public static class SingleWindow_Driver_Extensions
    {
        [WindowDriverIdentify(TypeFullName = "Target.CreateDriverTarget.SingleWindow")]
        public static SingleWindow_Driver Attach_SingleWindow(this WindowsAppFriend app)
            => new SingleWindow_Driver(app.WaitForIdentifyFromTypeFullName("Target.CreateDriverTarget.SingleWindow"));
    }
}