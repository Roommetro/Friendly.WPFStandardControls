using System.Collections.Generic;
using System.Windows.Forms;

namespace RM.Friendly.WPFStandardControls.Generator.CreateDriver
{
    public partial class EventSelectForm : Form
    {
        public EventSelectForm()
        {
            InitializeComponent();
        }

        public void AddEventName(string name)
        {
            _checkedListBoxEventName.Items.Add(name, false);
        }

        public string[] GetSelectedEventName()
        {
            var dst = new List<string>();

            foreach (var item in _checkedListBoxEventName.CheckedItems)
            {
                dst.Add(item.ToString());
            }

            return (dst.Count <= 0) ? null : dst.ToArray();
        }
    }
}
