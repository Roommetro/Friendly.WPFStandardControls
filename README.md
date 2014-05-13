Friendly.WPFStandardControls
============================

You must learn Friendly first.  
Because this library is built on Friendly Layer.  
But, it is very easy.  

http://www.english.codeer.co.jp/test-automation/friendly-fundamental  

============================
Friendly.WPFStandardControls defines the following classes.   
They can operate WPF control easily from another process.   

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
* WPFToggleButton  
* WPFTreeView  
* WPFTreeViewItem  
* WPFCalendar  
* WPFDatePicker  
* WPFDataGrid  

============================
```cs  
//sample  
var process = Process.GetProcessesByName("WPFTarget")[0];  
using (var app = new WindowsAppFriend(process))  
{  
    var main = app.Type(typeof(Application)).Current.MainWindow;  
    var grid = new WPFDataGrid(main._grid);  
    grid.EmulateChangeCellText(0, 0, "abc");  
    grid.EmulateChangeCellComboSelect(0, 1, 2);  
    grid.EmulateCellCheck(0, 2, true);  
}  
```

============================
Download form nuget.  
https://www.nuget.org/packages/RM.Friendly.WPFStandardControls/  

============================
If you want to operate other gui, you get following libraries.  

* or Win32.  
https://www.nuget.org/packages/Codeer.Friendly.Windows.NativeStandardControls/  

* For WinForms.  
https://www.nuget.org/packages/Ong.Friendly.FormsStandardControls/  

* For getting the desired window.  
https://www.nuget.org/packages/Codeer.Friendly.Windows.Grasp/  





