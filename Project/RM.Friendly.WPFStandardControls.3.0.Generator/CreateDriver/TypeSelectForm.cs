using System;
using System.Windows.Forms;

namespace RM.Friendly.WPFStandardControls.Generator.CreateDriver
{
    public partial class TypeSelectForm : Form
    {
        public Type SelectedType => (Type)_listBox.SelectedItem;

        public TypeSelectForm(Type[] types)
        {
            InitializeComponent();
            _listBox.Items.AddRange(types);
            _listBox.SelectedIndex = 0;
        }

        void ListBoxMouseDoubleClick(object sender, MouseEventArgs e)
            => DialogResult = DialogResult.OK;
    }
}
