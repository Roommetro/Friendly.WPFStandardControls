Friendly.WPFStandardControls
============================

This library is a layer on top of
Friendly, so you must learn that first.
But it is very easy to learn.

https://github.com/Codeer-Software/Friendly.Windows  

***
Friendly.WPFStandardControls defines the following classes.   
They can operate WPF control easily from a separate process.  

* WPFButtonBase  
* WPFComboBox  
* WPFListBox  
* WPFListView  
* WPFMenuBase  
* WPFMenuItem  
* WPFProgressBar  
* WPFRichTextBox  
* WPFSelector  
* WPFSlider  
* WPFTabControl  
* WPFTextBox  
* WPFTextBlock
* WPFToggleButton  
* WPFTreeView  
* WPFTreeViewItem  
* WPFCalendar  
* WPFDatePicker  
* WPFDataGrid  

***
```cs  
//sample  
var process = Process.GetProcessesByName("WPFTarget")[0];  
using (var app = new WindowsAppFriend(process))  
{  
    dynamic main = app.Type(typeof(Application)).Current.MainWindow;  
    var grid = new WPFDataGrid(main._grid);  
    grid.EmulateChangeCellText(0, 0, "abc");  
    grid.EmulateChangeCellComboSelect(0, 1, 2);  
    grid.EmulateCellCheck(0, 2, true);  
}  
```
```cs  
//sample : identify the elements.
var process = Process.GetProcessesByName("WPFTarget")[0];  
using (var app = new WindowsAppFriend(process))  
{  
    AppVar main = _app.Type<Application>().Current.MainWindow;
    var logicalTree = main.LogicalTree();

    var textBox = new WPFTextBox(logicalTree.ByBinding("Memo").ByType<TextBox>().Single());
    var textBlock = new WPFTextBlock(logicalTree.ByBinding("Memo").ByType<TextBlock>().Single());
    var button = new WPFButtonBase(logicalTree.ByBinding("CommandOK").Single());
    var listBox = new WPFListBox(logicalTree.ByBinding("Persons").Single());
}  
```
### More samples.
https://github.com/Roommetro/Friendly.WPFStandardControls/tree/master/Project/Test

***
Download from NuGet.  
https://www.nuget.org/packages/RM.Friendly.WPFStandardControls/  

***
For other GUI types, use the following libraries:

* For Win32.  
https://www.nuget.org/packages/Codeer.Friendly.Windows.NativeStandardControls/  

* For WinForms.  
https://www.nuget.org/packages/Ong.Friendly.FormsStandardControls/  

* For getting the target window.  
https://www.nuget.org/packages/Codeer.Friendly.Windows.Grasp/  

***
If you use PinInterface, you map control simple.  
https://www.nuget.org/packages/VSHTC.Friendly.PinInterface/



