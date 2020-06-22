Friendly.WPFStandardControls
============================

This library is a layer on top of
Friendly, so you must learn that first.
But it is very easy to learn.

https://github.com/Codeer-Software/Friendly 

## Getting Started
Install Friendly.WPFStandardControls from NuGet

    Install-Package RM.Friendly.WPFStandardControls
https://www.nuget.org/packages/RM.Friendly.WPFStandardControls/

***
## ControlDrivers
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
* WPFListBox&lt;TItemUserControlDriver>
* WPFListView&lt;TItemUserControlDriver> 
* WPFTreeView&lt;TItemUserControlDriver>

***
```cs  
//sample  
var process = Process.GetProcessesByName("WPFTarget")[0];  
using (var app = new WindowsAppFriend(process))  
{  
    dynamic main = app.Type(typeof(Application)).Current.MainWindow;  
    
    var textBox = new WPFTextBox(main._textBox);
    textBox.EmulateChangeText("abc");

    var grid = new WPFDataGrid(main._grid);  
    grid.EmulateChangeCellText(0, 0, "abc");  
    grid.EmulateChangeCellComboSelect(0, 1, 2);  
    grid.EmulateCellCheck(0, 2, true);  
}  
```
### More samples.
https://github.com/Roommetro/Friendly.WPFStandardControls/tree/master/Project/Test

## Data Template ItemsControl
For ListBox, ListView, TreeView, there is a mechanism that supports customization using DataTemplate.
* WPFListBox&lt;TItemUserControlDriver>
* WPFListView&lt;TItemUserControlDriver> 
* WPFTreeView&lt;TItemUserControlDriver>
```xml  
<UserControl x:Class="Test.ActiveItemTestControl"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006" 
             xmlns:d="http://schemas.microsoft.com/expression/blend/2008" 
             xmlns:local="clr-namespace:Test"
             mc:Ignorable="d" 
             d:DesignHeight="450" d:DesignWidth="800">
    <Canvas>
        <ListBox x:Name="_listBox" SelectionMode="Multiple" Height="148" Width="312">
            <ListBox.ItemTemplate>
                <DataTemplate>
                    <TextBox Text="{Binding Name}" />
                    <TextBox Text="{Binding Age}" />
                </DataTemplate>
            </ListBox.ItemTemplate>
        </ListBox>
    </Canvas>
</UserControl> 
```
```cs
public class ItemDriver : IAppVarOwner
{
    public AppVar AppVar { get; }
    public WPFTextBox Name => AppVar.VisualTree().ByBinding("Name").Dynamic();
    public WPFTextBox Age => AppVar.VisualTree().ByBinding("Age").Dynamic();
    public ItemDriver(AppVar appVar) => AppVar = appVar;
}

public void Test()
{
    var list = new WPFListBox<ItemDriver>(_ctrl._listBox);
    var item = list.GetItemDriver(1);
    item.Name.EmulateChangeTExt("Ishikawa");
    item.Name.EmulateChangeTExt("40");
}
```
The control driver is implemented using processing that uses [the basic functions of Friendly](https://github.com/Codeer-Software/Friendly#friendly-infrastructure).<br>
If you are using non-standard controls such as 3rd party controls you will need to create a new one.<br>
Knowledge of Friendly and its controls should not be so difficult.<br>
When you make ControlDriver, it is better not to refer to the implementation of WPF Standard Controls. <br>
It is difficult to read because there are many special writing methods that include support for .Net 4.0 and earlier.<br>
Normally, it is not necessary to support .Net 4.0 or earlier, so it is better to write it differently.<br>
Please refer to [this](https://github.com/Codeer-Software/Friendly.XamControls) as it is relatively easy to read.<br>
It is 3rd party control driver.<br>
https://github.com/Codeer-Software/Friendly.XamControls<br>

***

## Search Element
If you have an x:name, it is better to get it from the field using Friendly's basic functions.<br>
It is recommended to add x:name during development to simplify the test.<br>
If x:name does not exist, we have prepared the following means.<br>

### LogicalTree() and VisualTree()
```cs  
var app = new WindowsAppFriend(process);
var mainWindow = app.WaitForIdentifyFromTypeFullName("TargetApp.MainWindow");

//wrote the type for clarity, but usually write it as var.
IWPFDependencyObjectCollection<DependencyObject> logicalTree = mainWindow.LogicalTree();
IWPFDependencyObjectCollection<DependencyObject> visualTree = mainWindow.VisualTree();
```
Extension methods for AppVar and IAppVarOwner. <br>
Enabled with using RM.Friendly.WPFStandardCotnrols.<br>
Returns a LogicalTree and a VisualTree as collections, respectively.<br>
However, this is not a pure collection because the process is separated.<br>
You cannot use the regular Linq method.<br>
Normally, they are collected in the descendant direction, but they can also be collected in the ancestor direction by using an argument.<br>
```cs  
var logicalTree = mainWindow.LogicalTree(TreeRunDirection.Ancestors);
```
The following extension methods are provided for the returned IWPFDependencyObjectCollection<DependencyObject>.<br>
Please Identify after narrowing down.<br>
```cs  
var logicalTree = mainWindow.LogicalTree();

IWPFDependencyObjectCollection<DependencyObject> selectedByBinding = logicalTree.ByBinding("Memo");
IWPFDependencyObjectCollection<DependencyObject> selectedByType = logicalTree.ByType("TargetApp.CustomTextBox");
IWPFDependencyObjectCollection<Button> selectedByType2 = logicalTree.ByType<Button>();

//Searches can be repeated.
IWPFDependencyObjectCollection<Button> selectedByTypeAndBinding = logicalTree.ByType<TextBox>().ByBinding("Name");

//Identify.
WPFTextBox memo = selectedByBinding.Single().Dynamic();
WPFTextBox name = selectedByTypeAndBinding.SingleOrDefault().Dynamic();
WPFButtonBase button1 = selectedByType2[1].Dynamic();
```
When ByType is narrowed down to the following types, search is prepared at each extension method.<br>
IWPFDependencyObjectCollection&lt;ContentControl> <br>
IWPFDependencyObjectCollection&lt;ButtonBase> <br>

```cs  
var logicalTree = mainWindow.LogicalTree();
IWPFDependencyObjectCollection<Button> selectedByType = logicalTree.ByType<Button>();

//Button can search both ContentControl and ButtonBase.
var buttonClose = main.LogicalTree().ByType<Button>().ByCommand(ApplicationCommands.Close).Single();
//If can't use  the type, you can also search by string.
var buttonClose = main.LogicalTree().ByType<Button>().ByCommand("System.Windows.Input.ApplicationCommands", "Close").Single();

//Command parameter.
var buttonA = main.LogicalTree().ByType<Button>().ByCommandParameter("A").Single();

//Content text.
var buttonA = main.LogicalTree().ByType<Button>().ByContentText("A").Single();
```
### In target process (dll injection)
When Dll injection is performed, the above search can be executed inside the target process.<br>
In addition, since the return value is IEnumerable&lt;DependencyObject>, you can use Linq and search more freely.<br>
```cs  
public void Test()
{
    WPFStandardControls_3_5.Injection(_app);
    WindowsAppExpander.LoadAssembly(_app, GetType().Assembly);

    //get layout.
    var layout = _app.Type(GetType()).GetLayout(_app.Type<Application>().Current.MainWindow);
    var textBox = new WPFTextBox(layout.TextBox);
    var textBlock = new WPFTextBlock(layout.TextBlock);
    var button = new WPFButtonBase(layout.Button);
    var listBox = new WPFListBox(layout.ListBox);
}

class Layout
{
    public TextBox TextBox { get; set; }
    public TextBlock TextBlock { get; set; }
    public Button Button { get; set; }
    public ListBox ListBox { get; set; }
}

static Layout GetLayout(Window main)
{
    var logicalTree = main.LogicalTree();
    return new Layout()
    {
        TextBox = logicalTree.ByBinding("Memo").ByType<TextBox>().Single(),
        TextBlock = logicalTree.ByBinding("Memo").ByType<TextBlock>().Single(),
        Button = logicalTree.ByType<Button>().Where(x => x.IsCancel).Single(),
        ListBox = logicalTree.ByBinding("Persons").Single()
    };
}
```