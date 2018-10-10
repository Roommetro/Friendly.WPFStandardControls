using Codeer.Friendly;
using RM.Friendly.WPFStandardControls;
using System.Windows.Controls;

namespace NotInstallProject
{
    public class ItemControlDriver
    {
        public AppVar AppVar { get; set; }
        public WPFTextBlock Text => new WPFTextBlock(AppVar.VisualTree().ByType<TextBlock>()[0]);
        public ItemControlDriver(AppVar a)
        {
            AppVar = a;
        }
    }
}
