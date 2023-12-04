**Implementing Model-View-ViewModel for .NET MAUI**

This online-only section is about implementing the MVVM design pattern for .NET MAUI apps.

- [Understanding MVVM](#understanding-mvvm)
  - [The INotificationPropertyChanged interface](#the-inotificationpropertychanged-interface)
  - [The ObservableCollection class](#the-observablecollection-class)
  - [Creating a view model with two-way data binding](#creating-a-view-model-with-two-way-data-binding)


# Understanding MVVM

**Model-View-ViewModel (MVVM)** is a design pattern like MVC. The letters in the acronym stand for:

- **Model**: An entity class that represents a data object in a store like a relational database.
- **View**: A markup file that represents a user interface that can be bound to a view model. You
could have different views for different scenarios, like desktop or mobile. In the desktop view,
the data might be bound to a horizontally oriented carousel view and show a picture of each
category that the user swipes left and right through. In the mobile view, the data might be
bound to a simple vertical list view with just text that the user scrolls up and down through.
- **View Model**: A class that represents the business logic, like validation rules, and presentation
logic, like properties for all data values that might need to appear in a view. Examples of data
values include a category name or a unit price of a product. Examples of business logic include
commands for actions that need to be taken, like creating a new product or saving a change to
a category, and events like “the data has changed,” without any specific user interface.

In MVC, models passed to a view are read-only because they are only passed one way into the view.
That is why immutable records are good for MVC models. View models also only have properties for
storing data values. They do not have any functionality.

But ViewModels are different. They need to support two-way interactions and if the original data
changes during the lifetime of the object, the view needs to be dynamically updated. ViewModels also
have methods (actions) that they can perform, and these can be bound to interactive user interface
elements like buttons to execute the action.

> You can learn about the MVVM design pattern and how to implement it for .NET MAUI apps at the following link: https://learn.microsoft.com/en-us/dotnet/architecture/maui/mvvm.

## The INotificationPropertyChanged interface

The `INotifyPropertyChanged` interface enables a model class to support two-way data binding. It works by forcing the class to have an event named `PropertyChanged`, with a parameter of type
`PropertyChangedEventArgs`, as shown in the following code:
```cs
namespace System.ComponentModel
{
  public class PropertyChangedEventArgs : EventArgs
  {
    public PropertyChangedEventArgs(string? propertyName);
    public virtual string? PropertyName { get; }
  }

  public delegate void PropertyChangedEventHandler(
    object? sender, PropertyChangedEventArgs e);

  public interface INotifyPropertyChanged
  {
    event PropertyChangedEventHandler PropertyChanged;
  }
}
```

Inside each property in the class, when setting a new value, you must raise the event (if it is not null) with an instance of `PropertyChangedEventArgs` containing the name of the property as a string value, as shown in the following code:
```cs
private string companyName;

public string CompanyName
{
  get => companyName;
  set
  {
    companyName = value; // Store the new value being set.

    // Invoke any delegates that are listening for changes to
    // this property, for example, controls.
    PropertyChanged?.Invoke(this,
      new PropertyChangedEventArgs(nameof(CompanyName)));
  }
}
```

When a user interface control is data-bound to the property, it will automatically update to show the new value when it changes.

To simplify the implementation, we can use a compiler feature to get the name of the property by decorating a string parameter with the `[CallerMemberName]` attribute, as shown in the following code:
```cs
private void NotifyPropertyChanged(
  [CallerMemberName] string propertyName = "")
{
  // If an event handler has been set then invoke
  // the delegate and pass the name of the property.
  PropertyChanged?.Invoke(this,
    new PropertyChangedEventArgs(propertyName));
}

public string CompanyName
{
  get => companyName;
  set
  {
    companyName = value; // Store the new value being set.
    NotifyPropertyChanged(); // Caller member name is "CompanyName".
  }
}
```

## The ObservableCollection class

Related to `INotifyPropertyChanged` is the `INotifyCollectionChanged` interface that is implemented by the `ObservableCollection<T>` class. This gives notifications when items get added or removed, or when the collection is refreshed. When bound to controls like ListView or TreeView, the user interface will update dynamically to reflect changes.

## Creating a view model with two-way data binding

We need to create a view model that will allow us to show and modify a customer entity, so the class
should implement two-way data binding:
1. In the Chapter16 solution, in the Northwind.Maui.Client project folder, create two classes,
one named CustomerDetailViewModel.cs to show the details of a single customer and one
named CustomersListViewModel.cs to show a list of customers.
1. In CustomerDetailViewModel.cs, modify the statements to define a class that implements the
INotifyPropertyChanged interface and has six read-write properties that will support two-way
data binding and one read-only property, as shown in the following code:
using System.ComponentModel; // To use INotifyPropertyChanged.
using System.Runtime.CompilerServices; // To use [CallerMemberName].
namespace Northwind.Maui.Client;
public class CustomerDetailViewModel : INotifyPropertyChanged
{
public event PropertyChangedEventHandler PropertyChanged;
private string customerId;
private string companyName;
private string contactName;
private string city;
private string country;
private string phone;
// This attribute sets the propertyName parameter
// using the context in which this method is called.
private void NotifyPropertyChanged(
[CallerMemberName] string propertyName = "")
{
// If an event handler has been set then invoke
// the delegate and pass the name of the property.
PropertyChanged?.Invoke(this,
new PropertyChangedEventArgs(propertyName));
}
public string CustomerId
{
get => customerId;
setChapter 17 5
{
customerId = value;
NotifyPropertyChanged();
}
}
public string CompanyName
{
get => companyName;
set
{
companyName = value;
NotifyPropertyChanged();
}
}
public string ContactName
{
get => contactName;
set
{
contactName = value;
NotifyPropertyChanged();
}
}
public string City
{
get => city;
set
{
city = value;
NotifyPropertyChanged();
NotifyPropertyChanged(nameof(Location));
}
}
public string Country
{
get => country;
set6 Implementing Model-View-ViewModel for .NET MAUI
{
country = value;
NotifyPropertyChanged();
NotifyPropertyChanged(nameof(Location));
}
}
public string Phone
{
get => phone;
set
{
phone = value;
NotifyPropertyChanged();
}
}
public string Location => $"{City}, {Country}";
}
Note the following:
• The class implements INotifyPropertyChanged, so a two-way bound control like Editor
will update the property and vice versa. There is a PropertyChanged event that is raised
whenever one of the properties is modified, using a NotifyPropertyChanged private
method to simplify the implementation.
• In addition to properties for storing values retrieved from the HTTP service, the class
defines a read-only Location property. This will be bound to a summary list of customers to show the location of each one. Whenever the City or Country property changes,
we also need to notify anything that is bound to this property that the Location has
changed, or any views bound to Location will not update correctly.
1. In CustomersListViewModel.cs, modify the statements to define a class that inherits from
ObservableCollection<T> and has a method to populate sample data, as shown in the following code:
using System.Collections.ObjectModel; // To use ObservableCollection<T>.
namespace Northwind.Maui.Client;
public class CustomersListViewModel :Chapter 17 7
ObservableCollection<CustomerDetailViewModel>
{
// For testing before calling a real web service.
public void AddSampleData(bool clearList = true)
{
if (clearList) Clear();
Add(new CustomerDetailViewModel
{
CustomerId = "ALFKI",
CompanyName = "Alfreds Futterkiste",
ContactName = "Maria Anders",
City = "Berlin",
Country = "Germany",
Phone = "030-0074321"
});
Add(new CustomerDetailViewModel
{
CustomerId = "FRANK",
CompanyName = "Frankenversand",
ContactName = "Peter Franken",
City = "München",
Country = "Germany",
Phone = "089-0877310"
});
Add(new CustomerDetailViewModel
{
CustomerId = "SEVES",
CompanyName = "Seven Seas Imports",
ContactName = "Hari Kumar",
City = "London",
Country = "UK",
Phone = "(171) 555-1717"
});
}
}8 Implementing Model-View-ViewModel for .NET MAUI
Note the following:
• After loading from the service, which will be implemented later in this chapter, the customers
are cached locally using ObservableCollection<T>. This supports notifications to any bound
user interface components, such as ListView, so that the user interface can redraw itself when
the underlying data adds or removes items from the collection.
• For testing purposes, when the HTTP service is not available, there is a static method to populate three sample customers.
Creating views for the customers list and customer details
You will now add a view to show a list of customers and a view to show the details for a customer:
1. In CustomersPage.xaml, change Title to Customers, add padding and spacing to the vertical
stack layout, and then modify its contents to define a list view of customers, each one showing
their company name and location, as shown highlighted in the following markup:
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
x:Class="Northwind.Maui.Client.CustomersPage"
Title="Customers">
<VerticalStackLayout Spacing="15" Padding="20">
<HorizontalStackLayout Spacing="10">
<Label Text="Customers" FontSize="Title" />
<Button Text="Add" Clicked="Add_Clicked"
HorizontalOptions="End" />
</HorizontalStackLayout>
<ListView ItemsSource="{Binding .}"
VerticalOptions="Start"
HorizontalOptions="Start"
IsPullToRefreshEnabled="True"
ItemTapped="Customer_Tapped"
Refreshing="Customers_Refreshing">
<ListView.ItemTemplate>
<DataTemplate>
<TextCell Text="{Binding CompanyName}"
Detail="{Binding Location}"
TextColor="{DynamicResource PrimaryTextColor}"
DetailColor="{DynamicResource PrimaryTextColor}" >
<TextCell.ContextActions>Chapter 17 9
<MenuItem Clicked="Customer_Phoned" Text="Phone" />
<MenuItem Clicked="Customer_Deleted" Text="Delete"
IsDestructive="True" />
</TextCell.ContextActions>
</TextCell>
</DataTemplate>
</ListView.ItemTemplate>
</ListView>
</VerticalStackLayout>
</ContentPage>
Note the following:
• ListView has its IsPullToRefreshEnabled set to true.
• An Add button is in the list view header so that users can navigate to a detail view to
add a new customer.
• A data template defines how to display each customer: larger text for the company
name and smaller text for the location underneath.
• Event handlers have been written for the following events:
• Customer_Tapped: A customer being tapped or clicked to show their details.
• Customers_Refreshing: The list being pulled down to refresh its items.
• Customer_Phoned: A cell being swiped left on iPhone, long-pressed on Android,
or right-clicked on Windows, and then tapping or clicking Phone.
• Customer_Deleted: A cell being swiped left on iPhone, long-pressed on Android,
or right-clicked on Windows, and then tapping or clicking Delete.
• Add_Clicked: The Add button being clicked or tapped.
1. In CustomersPage.xaml.cs, modify the contents to create the view model, populate it with
sample data, and set it as the binding context, as well as implementing event handlers for all
the control events, as shown highlighted in the following code:
namespace Northwind.Maui.Client;
public partial class CustomersPage : ContentPage
{
public CustomersPage()
{
InitializeComponent();
CustomersListViewModel viewModel = new();10 Implementing Model-View-ViewModel for .NET MAUI
viewModel.AddSampleData();
BindingContext = viewModel;
}
async void Customer_Tapped(object sender, ItemTappedEventArgs e)
{
if (e.Item is not CustomerDetailViewModel c) return;
// navigate to the detail view and show the tapped customer
await Navigation.PushAsync(new CustomerDetailPage(
BindingContext as CustomersListViewModel, c));
}
async void Customers_Refreshing(object sender, EventArgs e)
{
if (sender is not ListView listView) return;
listView.IsRefreshing = true;
// simulate a refresh
await Task.Delay(1500);
listView.IsRefreshing = false;
}
void Customer_Deleted(object sender, EventArgs e)
{
MenuItem menuItem = sender as MenuItem;
if (menuItem.BindingContext is not CustomerDetailViewModel c)
return;
(BindingContext as CustomersListViewModel).Remove(c);
}
async void Customer_Phoned(object sender, EventArgs e)
{
MenuItem menuItem = sender as MenuItem;
if (menuItem.BindingContext is not CustomerDetailViewModel c)
return;
if (await DisplayAlert("Dial a Number",
"Would you like to call " + c.Phone + "?",Chapter 17 11
"Yes", "No"))
{
try
{
if (PhoneDialer.IsSupported)
{
PhoneDialer.Open(c.Phone);
}
}
catch (Exception ex)
{
await DisplayAlert(title: "Failed",
message: string.Format(
"Failed to dial {0} due to: {1}", c.Phone, ex.Message),
cancel: "OK");
}
}
}
async void Add_Clicked(object sender, EventArgs e)
{
await Navigation.PushAsync(new CustomerDetailPage(
BindingContext as CustomersListViewModel));
}
}
Note the following:
• BindingContext is set to an instance of CustomersViewModel that is populated with
sample data in the constructor of the page.
• When a customer in the list view is tapped, the user is taken to a details view (which
you will implement in the next step).
• When the list view is pulled down, it triggers a simulated refresh that takes 1.5 seconds.
• When a customer is deleted in the list view, they are removed from the bound customers view model.
• When a customer in the list view is swiped, and the Phone button is tapped, a dialog
prompts the user as to whether they want to dial the number, and if so, the platform-native implementation will be retrieved using the dependency resolver and then used
to dial the number.
• When the Add button is tapped, the user is taken to the customer detail page to enter
details for a new customer.12 Implementing Model-View-ViewModel for .NET MAUI
1. In CustomerDetailPage.xaml, modify its contents to define a user interface to review and edit
the details of a customer, as shown highlighted in the following markup, and note the following:
• The Title of the content page has been set to Customer Detail.
• A Grid with two columns and six rows is used for the layout.
• Entry views are two-way data bound to properties of the CustomerViewModel class.
• InsertButton has an event handler to execute code to add a new customer:
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage
xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
x:Class="Northwind.Maui.Client.CustomerDetailPage"
Title="Customer Detail">
<VerticalStackLayout>
<Grid ColumnDefinitions="Auto,Auto"
RowDefinitions="Auto,Auto,Auto,Auto,Auto,Auto">
<Label Text="Customer Id" VerticalOptions="Center" Margin="6"
/>
<Entry Text="{Binding CustomerId, Mode=TwoWay}"
Grid.Column="1"
MaxLength="5" TextTransform="Uppercase" />
<Label Text="Company Name" Grid.Row="1"
VerticalOptions="Center" Margin="6" />
<Entry Text="{Binding CompanyName, Mode=TwoWay}"
Grid.Column="1" Grid.Row="1" />
<Label Text="Contact Name" Grid.Row="2"
VerticalOptions="Center" Margin="6" />
<Entry Text="{Binding ContactName, Mode=TwoWay}"
Grid.Column="1" Grid.Row="2" />
<Label Text="City" Grid.Row="3"
VerticalOptions="Center" Margin="6" />
<Entry Text="{Binding City, Mode=TwoWay}"
Grid.Column="1" Grid.Row="3" />
<Label Text="Country" Grid.Row="4"
VerticalOptions="Center" Margin="6" />
<Entry Text="{Binding Country, Mode=TwoWay}"
Grid.Column="1" Grid.Row="4" />
<Label Text="Phone" Grid.Row="5"Chapter 17 13
VerticalOptions="Center" Margin="6" />
<Entry Text="{Binding Phone, Mode=TwoWay}"
Grid.Column="1" Grid.Row="5" />
</Grid>
<Button x:Name="InsertButton" Text="Insert Customer"
Clicked="InsertButton_Clicked" />
</VerticalStackLayout>
</ContentPage>
1. In CustomerDetailPage.xaml.cs, modify its contents to create a view model and set it as the
binding context, and implement the Insert button, as shown highlighted in the following code:
namespace Northwind.Maui.Client;
public partial class CustomerDetailPage : ContentPage
{
private CustomersListViewModel customers;
public CustomerDetailPage(CustomersListViewModel customers)
{
InitializeComponent();
this.customers = customers;
BindingContext = new CustomerDetailViewModel();
Title = "Add Customer";
}
public CustomerDetailPage(CustomersListViewModel customers,
CustomerDetailViewModel customer)
{
InitializeComponent();
this.customers = customers;
BindingContext = customer;
InsertButton.IsVisible = false;
A control that is a child of a Grid automatically has its Grid.Row and Grid.Column
set to 0. Therefore, controls like the Customer Id label do not need those properties to be explicitly set, and controls like the CustomerId entry only need the
column set.14 Implementing Model-View-ViewModel for .NET MAUI
}
async void InsertButton_Clicked(object sender, EventArgs e)
{
customers.Add((CustomerDetailViewModel)BindingContext);
await Navigation.PopAsync(animated: true);
}
}
Note the following:
• The default constructor has been deleted.
• The constructor with a customers parameter sets the binding context to a new customer
instance and the view title is changed to Add Customer.
• The constructor with a customers parameter and a customer parameter sets the binding context to that instance and hides the Insert button because it is not needed when
editing an existing customer due to two-way data binding.
• When the Insert button is tapped, the new customer is added to the customers view
model and the navigation is moved back to the previous view asynchronously.
1. In the Platforms folder, in the Android folder, open the AndroidManifest.xml file using an
XML editor, and then add entries to enable phone dialing, as shown highlighted in the following markup:
<?xml version="1.0" encoding="utf-8"?>
<manifest xmlns:android="http://schemas.android.com/apk/res/android">
<application android:allowBackup="true"
android:icon="@mipmap/appicon"
android:roundIcon="@mipmap/appicon_round"
android:supportsRtl="true"></application>
<uses-permission android:name="android.permission
.ACCESS_NETWORK_STATE" />
<uses-permission android:name="android.permission.INTERNET" />
<queries>
<intent>
<action android:name="android.intent.action.DIAL" />
<data android:scheme="tel"/>
</intent>
</queries>
</manifest>Chapter 17 15
Testing the .NET MAUI app
We will now test the app using the Android device emulator so that we can see the phone caller functionality:
1. In Visual Studio 2022, to the right of the Run button in the toolbar, set the target framework to
net8.0-android and select the Android emulator.
1. Start the project with debugging. The project will build and then, after five minutes or so, the
latest version of your app will deploy, and the Android device emulator will appear with your
running .NET MAUI app. Be patient because the old version may still be running in the saved
state of the emulator.
1. Navigate to Customers, as shown in Figure 17.1:
Figure 17.1: Android emulator showing Customers in the Northwind app
1. Click Seven Seas Imports and modify Company Name to Seven Oceans Imports, as shown in
the following screenshot of the customer detail page in Figure 17.2:
Figure 17.2: Editing a company name on the customer detail page
1. Click the back button to return to the list of customers and note that the company name has
been updated due to the two-way data binding.16 Implementing Model-View-ViewModel for .NET MAUI
1. Click Add, and then fill in the fields for a new customer, as shown in Figure 17.3.
By default, in the Android device emulator, the virtual keyboard is shown when typing on a
physical keyboard. To hide the virtual keyboard, click the keyboard icon to the right of the
square Android soft button, and then toggle Show virtual keyboard.
Figure 17.3: Completing the details to add a new customer
1. On the customer detail page, click the Insert Customer button and, after being returned to
the list of customers, note that the new customer has been added to the bottom of the list.
1. Click and hold on one of the customers to reveal two action buttons, Phone and Delete, as
shown in Figure 17.4:
Figure 17.4: Extra commands for a selected customer
1. Click Phone and note the pop-up prompt to the user to dial the number of that customer with
Yes and No buttons.
1.  Click Yes and note the app switches to the device’s native phone dialer.
2.  In the emulator, click the back button (the back-pointing triangle) three times to return to
the app.Chapter 17 17
1.  Click and hold on one of the customers to reveal two action buttons, Phone and Delete, and
then click on Delete, and note that the customer is removed.
1.  Click, hold, and drag the list down and then release, and note the animation effect for refreshing
the list, but remember that this feature is simulated, so the list does not change.
1.  Close the Android device emulator.
2.  Repeat the above steps with Windows Machine to see the differences.
We will now make the app call a web service to get the list of customers.
Using community toolkits
There is an easier way to implement MVVM with .NET MAUI and that is to use the MVVM Toolkit.
There is also a .NET MAUI Community Toolkit to add other useful features to .NET MAUI apps like
animations and toast notifications:
• The MVVM Toolkit is maintained and published by Microsoft. Its package is CommunityToolkit.
Mvvm. Its minimum target is .NET Standard 2.0 so it can be used with not just .NET MAUI but
also Windows Forms, WPF, UWP, Uno, Avalonia, and other project types. It also targets .NET
6 and later so it can use optimizations available in modern .NET.
• The .NET MAUI Community Toolkit is maintained by volunteers in the community. Its package
is CommunityToolkit.Maui.
The Model-View-ViewModel (MVVM) pattern separates an application’s business and presentation
logic from its user interface markup. This makes the app easier to test and maintain, and it’s easier
to add or modify features over time.
MVVM can be a pain to implement because it requires a lot of boilerplate code. For example, the
properties in the view model must implement the INotifyPropertyChanged interface and raise the
PropertyChanged event so that the view gets notified when it needs to update.
The MVVM Toolkit has source generators to do that work for you. For example, just inherit from the
ObservableObject class, define a private field named using camel casing, and decorate with the
[ObservableProperty] attribute, as shown in the following code, and then the source generators
will do the rest:
// ObservableObject, [ObservableProperty]
using CommunityToolkit.Mvvm.ComponentModel;
partial class Category : ObservableObject
{
You can read an announcement about the .NET Community Toolkit at the following link:
https://devblogs.microsoft.com/dotnet/announcing-the-dotnet-communitytoolkit-800/.18 Implementing Model-View-ViewModel for .NET MAUI
[ObservableProperty]
private string? categoryName;
// Other members.
}
If a class already needs to inherit from another class and so it cannot inherit from ObservableObject,
then you can decorate the class with a special attribute, as shown in the following code:
// [INotifyPropertyChanged], [ObservableProperty]
using CommunityToolkit.Mvvm.ComponentModel;
[INotifyPropertyChanged]
partial class Category : SomeOtherClass
{
[ObservableProperty]
private string? categoryName;
// Other members.
}
Building a products page with the toolkits
Let’s go:
1. In the Northwind.Maui.Client.csproj project file, add package references for the two community toolkits to the existing <ItemGroup> for packages, as shown highlighted in the following
markup:
<ItemGroup>
<PackageReference Include="Microsoft.Maui.Controls"
Version="$(MauiVersion)" />
<PackageReference Include="Microsoft.Maui.Controls.Compatibility"
Version="$(MauiVersion)" />
<PackageReference Include="Microsoft.Extensions.Logging.Debug"
Version="8.0.0" />
<PackageReference Include="CommunityToolkit.Mvvm" Version="8.2.2"
/>
<PackageReference Include="CommunityToolkit.Maui" Version="6.0.0"
Good Practice: To create an observable class, it is best to inherit from ObservableObject.
If you cannot, then decorate with [INotifyPropertyChanged], but this will be less efficient because code must be duplicated.Chapter 17 19
/>
</ItemGroup>
1. Build the project to restore packages and note the warning message:
MCT001 `.UseMauiCommunityToolkit()` must be chained to
`.UseMauiApp<T>()`
1. In MauiProgram.cs, import the namespace for the .NET MAUI Community Toolkit, as shown
in the following code:
using CommunityToolkit.Maui; // To use UseMauiCommunityToolkit method.
1. In MauiProgram.cs, add a call to use the .NET MAUI Community Toolkit, as shown highlighted
in the following code:
builder
.UseMauiApp<App>()
.UseMauiCommunityToolkit()
.ConfigureFonts(fonts =>
{
fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
});
1. Rebuild the project and note the error disappears.
2. In the project folder, add a new class named ProductViewModel.cs, as shown in the following
code:
// To use ObservableObject and [ObservableProperty].
using CommunityToolkit.Mvvm.ComponentModel;
namespace Northwind.Maui.Client;
// ObservableObject implements INotifyPropertyChanged.
internal partial class ProductViewModel : ObservableObject
{
// This attribute uses the source generator to add the public property
// named ProductId to the class
[ObservableProperty]
private int productId;
[ObservableProperty]
private string productName;20 Implementing Model-View-ViewModel for .NET MAUI
[ObservableProperty]
private int supplierId;
[ObservableProperty]
private int categoryId;
[ObservableProperty]
private string quantityPerUnit;
[ObservableProperty]
private decimal unitPrice;
[ObservableProperty]
private int unitsInStock;
[ObservableProperty]
private int unitsOnOrder;
[ObservableProperty]
private int reorderLevel;
[ObservableProperty]
private bool discontinued;
// A readonly property to show information about stock.
public string Stock
{
get => $"Stock: {UnitsInStock} in stock, {UnitsOnOrder} on order,
reorder at {ReorderLevel}.";
}
}
1. In the project folder, add a new class named ProductsViewModel.cs, as shown in the following
code:
using CommunityToolkit.Mvvm.Input; // To use [RelayCommand].
using System.Collections.ObjectModel; // To use ObservableCollection<T>.
namespace Northwind.Maui.Client;
internal partial class ProductsViewModel :
ObservableCollection<ProductViewModel>Chapter 17 21
{
[RelayCommand(CanExecute = nameof(CanDeleteProduct))]
private void DeleteProduct(int productId)
{
ProductViewModel productToRemove =
this.SingleOrDefault(p => p.ProductId == productId);
if (productToRemove is not null)
{
Remove(productToRemove);
}
}
private bool CanDeleteProduct(int productId)
{
ProductViewModel productToRemove =
this.SingleOrDefault(p => p.ProductId == productId);
return (productToRemove is not null);
}
public void AddSampleData()
{
Add(new ProductViewModel()
{
ProductId = 1,
ProductName = "Chai",
SupplierId = 1,
CategoryId = 1,
QuantityPerUnit = "10 boxes x 20 bags",
UnitPrice = 18M,
UnitsInStock = 39,
UnitsOnOrder = 0,
ReorderLevel = 10,
Discontinued = false
});
Add(new ProductViewModel()
{
ProductId = 2,
ProductName = "Chang",22 Implementing Model-View-ViewModel for .NET MAUI
SupplierId = 1,
CategoryId = 1,
QuantityPerUnit = "24 - 12 oz bottles",
UnitPrice = 19M,
UnitsInStock = 17,
UnitsOnOrder = 40,
ReorderLevel = 25,
Discontinued = false
});
Add(new ProductViewModel()
{
ProductId = 3,
ProductName = "Aniseed Syrup",
SupplierId = 1,
CategoryId = 2,
QuantityPerUnit = "12 - 550 ml bottles",
UnitPrice = 10M,
UnitsInStock = 13,
UnitsOnOrder = 70,
ReorderLevel = 25,
Discontinued = false
});
}
}
1. In Solution Explorer, expand Dependencies, expand net8.0-android, expand Analyzers, expand
CommunityToolkit.Mvvm.SourceGenerators, expand CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator, open Northwind.Maui.Client.ProductViewModel.g.cs,
and note the public property named ProductId that was generated based on your private field
productId, as shown in the following code and in Figure 17.5:
// <auto-generated/>
#pragma warning disable
#nullable enable
namespace Northwind.Maui.Client
{
/// <inheritdoc/>
partial class ProductViewModel
{
/// <inheritdoc cref="productId"/>
[global::System.CodeDom.Compiler.GeneratedCode(Chapter 17 23
"CommunityToolkit.Mvvm.SourceGenerators.
ObservablePropertyGenerator", "8.2.0.0")]
[global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public int ProductId
{
get => productId;
set
{
if (!global::System.Collections.Generic
.EqualityComparer<int>.Default.Equals(productId, value))
{
OnProductIdChanging(value);
OnProductIdChanging(default, value);
OnPropertyChanging(global::CommunityToolkit.Mvvm
.ComponentModel.__Internals
.__KnownINotifyPropertyChangingArgs.ProductId);
productId = value;
OnProductIdChanged(value);
OnProductIdChanged(default, value);
OnPropertyChanged(global::CommunityToolkit.Mvvm
.ComponentModel.__Internals
.__KnownINotifyPropertyChangedArgs.ProductId);
}
Figure 17.5: Source generated partial class for bindable properties24 Implementing Model-View-ViewModel for .NET MAUI
1. Expand CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator, open
Northwind.Maui.Client.ProductsViewModel.DeleteProduct.g.cs, and note the public
property named DeleteProductCommand that was generated based on your private method
DeleteProduct, as shown in the following code:
// <auto-generated/>
#pragma warning disable
#nullable enable
namespace Northwind.Maui.Client
{
/// <inheritdoc/>
partial class ProductsViewModel
{
/// <summary>The backing field for <see
cref="DeleteProductCommand"/>.</summary>
[global::System.CodeDom.Compiler.GeneratedCode(
"CommunityToolkit.Mvvm.SourceGenerators
.RelayCommandGenerator", "8.2.0.0")]
private global::CommunityToolkit.Mvvm.Input
.RelayCommand<int>? deleteProductCommand;
/// <summary>Gets an <see cref=
"global::CommunityToolkit.Mvvm.Input
.IRelayCommand{T}"/> instance wrapping <see
cref="DeleteProduct"/>.</summary>
[global::System.CodeDom.Compiler.GeneratedCode(
"CommunityToolkit.Mvvm.SourceGenerators
.RelayCommandGenerator", "8.2.0.0")]
[global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public global::CommunityToolkit.Mvvm.Input
.IRelayCommand<int> DeleteProductCommand =>
deleteProductCommand ??= new global::CommunityToolkit
.Mvvm.Input.RelayCommand<int>(
new global::System.Action<int>(DeleteProduct), CanDeleteProduct);
}
}
1.  In ProductsPage.xaml.cs, add statements to the constructor to initialize a data context and
add sample data, as shown highlighted in the following code:
namespace Northwind.Maui.Client;
public partial class ProductsPage : ContentPage
{Chapter 17 25
public ProductsPage()
{
InitializeComponent();
ProductsViewModel products = new();
products.AddSampleData();
BindingContext = products;
}
}
1.  In ProductsPage.xaml, add statements to import local types, change Title to Products, and
use a ListView to output the products, as shown highlighted in the following markup:
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
xmlns:local="clr-namespace:Northwind.Maui.Client"
x:Class="Northwind.Maui.Client.ProductsPage"
Title="Products">
<ListView ItemsSource="{Binding .}">
<ListView.ItemTemplate>
<DataTemplate>
<ViewCell>
<HorizontalStackLayout>
<VerticalStackLayout Padding="5,5,0,5" Spacing="15">
<Label Text="{Binding ProductName}"
FontSize="14"
FontAttributes="Bold" />
<Label Text="{Binding Stock}" />
</VerticalStackLayout>
<Button Text="Delete"
Command="{Binding Source={RelativeSource
AncestorType={x:Type local:ProductsViewModel}},
Path=DeleteProductCommand}"
CommandParameter="{Binding ProductId}"/>
</HorizontalStackLayout>
</ViewCell>
</DataTemplate>
</ListView.ItemTemplate>
</ListView>
</ContentPage>26 Implementing Model-View-ViewModel for .NET MAUI
Testing the app
Now we can test the app:
1. Start the project with debugging.
2. Navigate to Products and note the list of products, as shown in Figure 17.6:
Figure 17.6: Products with delete buttons bound to an MVVM command
1. In the Chang row, click the Delete button, and note the animation when the row is deleted.
2. Close the app.
Consuming a web service from a mobile app
Apple’s App Transport Security (ATS) forces developers to use good practice, including secure connections between an app and a web service. ATS is enabled by default and your mobile apps will throw an
exception if they do not connect securely. Since Android 9, Google has had a similar policy.
If you need to call a web service that is secured with a self-signed certificate like our Northwind.Maui.
WebApi.Service project is, it is possible but complicated. For simplicity, we will allow unsecure connections to the web service and disable the security checks in the mobile app.
More Information: You can learn more about the .NET MAUI MVVM Community Toolkit
documentation at the following link: https://learn.microsoft.com/en-us/dotnet/
communitytoolkit/mvvm/.Chapter 17 27
Creating a minimal API web service for customers
We will create a web service for working with customers in the Northwind database:
1. In your preferred code editor, add a web service project, as defined in the following list:
• Project template: ASP.NET Core Web API / webapi --use-minimal-apis
• Workspace/solution file and folder: Chapter16
• Project file and folder: Northwind.Maui.WebApi.Service
• Authentication type: None.
• Configure for HTTPS: Selected.
• Enable Docker: Cleared.
• Enable OpenAPI support: Selected.
• Do not use top-level statements: Cleared.
• Use controllers: Cleared.
1. In the project file, set the invariant global to false, treat errors as warnings, and add a project
reference to the Northwind database context project for SQL Server that you created in Chapter
3, Building Entity Models for SQL Server Using EF Core, as shown in the following markup:
<ItemGroup>
<ProjectReference Include="..\..\Chapter03\Northwind.Common
.DataContext.SqlServer\Northwind.Common
.DataContext.SqlServer.csproj" />
</ItemGroup>
1. At the command prompt or terminal, build the Northwind.Maui.WebApi.Service project to
make sure the entity model class library projects outside the current solution are properly
compiled, as shown in the following command: dotnet build.
1. In the Properties folder, in launchSettings.json, for the https profile, modify applicationUrl
to use port 5161 for https and port 5162 for http, as shown highlighted in the following configuration:
"applicationUrl": "https://localhost:5161;http://localhost:5162",
1. For the http profile, modify applicationUrl to use port 5162 for http, as shown highlighted
in the following configuration:
"applicationUrl": "http://localhost:5162",
We cannot use the native AOT project template because we need to reference an EF
Core 8 project, which does not support AOT. Also note that the --use-minimalapis switch is not needed in .NET 8 because it is now the default, but for .NET 7
you would need to specify this switch.28 Implementing Model-View-ViewModel for .NET MAUI
1. In Program.cs, delete the statements about the weather service and replace them with statements to configure endpoints for CRUD operations on customers, as shown highlighted in
the following code:
using Microsoft.AspNetCore.Mvc; // To use [FromServices] .
using Northwind.EntityModels; // To use AddNorthwindContext method.
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/
aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddNorthwindContext();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
app.UseSwagger();
app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.MapGet("api/customers", (
[FromServices] NorthwindContext db) => db.Customers)
.WithName("GetCustomers")
.Produces<Customer[]>(StatusCodes.Status200OK);
app.MapGet("api/customers/{id}", (
[FromRoute] string id,
[FromServices] NorthwindContext db) => db.Customers
.FirstOrDefault(c => c.CustomerId == id))
.WithName("GetCustomer")
.Produces<Customer>(StatusCodes.Status200OK);
app.MapPost("api/customers", async (
[FromBody] Customer customer,Chapter 17 29
[FromServices] NorthwindContext db) =>
{
db.Customers.Add(customer);
await db.SaveChangesAsync();
return Results.Created($"api/customers/{customer.CustomerId}",
customer);
}).WithOpenApi()
.Produces<Customer>(StatusCodes.Status201Created);
app.MapPut("api/customers/{id}", async (
[FromRoute] string id,
[FromBody] Customer customer,
[FromServices] NorthwindContext db) =>
{
Customer? foundCustomer = await db.Customers.FindAsync(id);
rif (foundCustomer is null) return Results.NotFound();
foundCustomer.CompanyName = customer.CompanyName;
foundCustomer.ContactName = customer.ContactName;
foundCustomer.ContactTitle = customer.ContactTitle;
foundCustomer.Address = customer.Address;
foundCustomer.City = customer.City;
foundCustomer.Region = customer.Region;
foundCustomer.PostalCode = customer.PostalCode;
foundCustomer.Country = customer.Country;
foundCustomer.Phone = customer.Phone;
foundCustomer.Fax = customer.Fax;
await db.SaveChangesAsync();
return Results.NoContent();
}).WithOpenApi()
.Produces(StatusCodes.Status404NotFound)
.Produces(StatusCodes.Status204NoContent);
app.MapDelete("api/customers/{id}", async (
[FromRoute] string id,
[FromServices] NorthwindContext db) =>
{
if (await db.Customers.FindAsync(id) is Customer customer)30 Implementing Model-View-ViewModel for .NET MAUI
{
db.Customers.Remove(customer);
await db.SaveChangesAsync();
return Results.NoContent();
}
return Results.NotFound();
}).WithOpenApi()
.Produces(StatusCodes.Status404NotFound)
.Produces(StatusCodes.Status204NoContent);
app.Run();
1. If your database server is not running, for example, because you are hosting it in Docker, a
virtual machine, or in the cloud, then make sure to start it.
1. Start the web service project with the https profile and note the Swagger documentation, as
shown in Figure 17.7:
Figure 17.7: Swagger documentation for the Northwind Web API service
1. Click GET /api/customers to expand that section.
2.  Click the Try it out button, click the Execute button, and note that customer records are returned.
3.  Close the browser and shut down the web server.
Configuring the web service to allow unsecure requests
Next, we will enable the web service to handle unsecure connections:Chapter 17 31
1. In the Northwind.Maui.WebApi.Service project, in Program.cs, in the section that configures
the HTTP pipeline, comment out the HTTPS redirection, as shown in the following code:
// app.UseHttpsRedirection();
1. Start the Northwind.Maui.WebApi.Service project without debugging.
2. Start Chrome and test that the web service is returning customers as JSON by navigating to
the following URL: http://localhost:5162/api/customers/, and note the returned JSON
document, as shown in Figure 17.8:
Figure 17.8: Customers returned as a JSON document
1. Close Chrome and shut down the web server.
Connecting to local web services while testing
When testing a .NET MAUI app on a Windows machine, it has normal access to the local network
including any web services you are hosting on localhost. The iOS emulator also has normal access to
the local network. So, both Windows and iOS targeted .NET MAUI apps can connect directly to a web
service hosted at an endpoint like http://localhost:5162/api/customer.
But when testing a .NET MAUI app on an emulated Android device, it is separated from your local
network by a virtual router. To connect to a web service hosted on localhost, you must use a special
IP address 10.0.2.2 that the virtual router maps to 127.0.0.1, aka localhost. So, Android-targeted
.NET MAUI apps can connect to a web service hosted at an endpoint like http://localhost:5162/
api/customer by using http://10.0.2.2:5162/api/customer.
Configuring the iOS app to allow unsecured connections
To allow unsecured connections to web services in an iOS app, we have a couple of choices:
• Set NSAppTransportSecurity to NSAllowsArbitraryLoads. This allows clear text in all scenarios.
• Set NSAppTransportSecurity to NSAllowsLocalNetworking. This allows clear text only in
local scenarios.
Now you will configure the Northwind.Maui.Customers project to disable ATS to allow unsecured
HTTP requests to the web service:
1. In the Northwind.Maui.Client project, in the Platforms/iOS folder, open the Info.plist file
by right-clicking and opening it with the XML (Text) Editor.32 Implementing Model-View-ViewModel for .NET MAUI
1. At the bottom of the dictionary, add a new key named NSAppTransportSecurity that is a
dictionary, and in it, add a key named NSAllowsArbitraryLoads that has a value of true, as
shown highlighted in the following partial markup:
<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE plist PUBLIC "-//Apple//DTD PLIST 1.0//EN"
"http://www.apple.com/DTDs/PropertyList-1.0.dtd">
<plist version="1.0">
<dict>
<key>LSRequiresIPhoneOS</key>
<true/>
...
<key>XSAppIconAssets</key>
<string>Assets.xcassets/appicon.appiconset</string>
<key>NSAppTransportSecurity</key>
<dict>
<key>NSAllowsArbitraryLoads</key>
<true/>
</dict>
</dict>
</plist>
1. Save and close Info.plist.
Configuring the Android app to allow unsecured connections
In a similar way to Apple and ATS, with Android 9 (API level 28) cleartext (that is, non-HTTPS) support
is disabled by default.
Now you will configure the project to enable cleartext to allow unsecured HTTP requests to the web
service:
1. In the Platforms/Android folder, in the Resources folder, add a new folder named xml.
2. In the xml folder, add a new XML file named network_security_config.xml, and add entries
to enable cleartext when connecting over the virtual router’s special IP address that maps out
to localhost, as shown in the following markup:
<?xml version="1.0" encoding="utf-8" ?>
<network-security-config>
Warning! If you run a .NET MAUI app using the iOS simulator on Windows, the app is
actually running on the connected Mac even though it visually appears on Windows. It
therefore cannot connect to local web services. It would have to connect remotely to the
web service, or you could run the web service on the Mac.Chapter 17 33
<domain-config cleartextTrafficPermitted="true">
<domain includeSubdomains="true">10.0.2.2</domain>
</domain-config>
</network-security-config>
1. In the Android folder, in AndroidManifest.xml, add an attribute to the <application> element
to reference the new XML file, as shown highlighted in the following markup:
<?xml version="1.0" encoding="utf-8"?>
<manifest xmlns:android="http://schemas.android.com/apk/res/android">
<application android:allowBackup="true"
android:icon="@mipmap/appicon"
android:networkSecurityConfig=
"@xml/network_security_config"
android:roundIcon="@mipmap/appicon_round"
android:supportsRtl="true">
</application>
<uses-permission android:name="android.permission.ACCESS_NETWORK_STATE"
/>
<uses-permission android:name="android.permission.INTERNET" />
<queries>
<intent>
<action android:name="android.intent.action.DIAL" />
<data android:scheme="tel"/>
</intent>
</queries>
</manifest>
1. Save all the changes.
Getting customers from the web service
Now, we can modify the customers list page to get its list of customers from the web service instead
of using sample data:
1. In the Northwind.Maui.Client project, in CustomersPage.xaml, add a label to show information about the web service endpoint and a label to show any error messages, as shown in the
following markup:
<VerticalStackLayout Spacing="15" Padding="20">
<HorizontalStackLayout Spacing="10">
<Label Text="Customers" FontSize="Title" />
<Button Text="Add" Clicked="Add_Clicked"
HorizontalOptions="End" />
</HorizontalStackLayout>34 Implementing Model-View-ViewModel for .NET MAUI
<Label x:Name="InfoLabel" />
<Label x:Name="ErrorLabel" IsVisible="false" />
<ListView ItemsSource="{Binding .}"
1. In CustomersPage.xaml.cs, import the following additional namespaces:
using System.Net.Http.Headers; // To use
MediaTypeWithQualityHeaderValue.
using System.Net.Http.Json; // To use ReadFromJsonAsync<T> method.
1. Modify the CustomersPage constructor to load the list of customers using the service proxy and
only call the AddSampleData method if an exception occurs, as shown in the following code:
public CustomersPage()
{
InitializeComponent();
CustomersListViewModel viewModel = new();
try
{
string domain = DeviceInfo.Platform
== DevicePlatform.Android ? "10.0.2.2" : "localhost";
HttpClient client = new()
{ BaseAddress = new Uri($"http://{domain}:5162") };
InfoLabel.Text = $"BaseAddress: {client.BaseAddress}";
client.DefaultRequestHeaders.Accept.Add(
new MediaTypeWithQualityHeaderValue("application/json"));
HttpResponseMessage response = client
.GetAsync("api/customers").Result;
response.EnsureSuccessStatusCode();
IEnumerable<CustomerDetailViewModel> customersFromService =
response.Content.ReadFromJsonAsync
<IEnumerable<CustomerDetailViewModel>>().Result;
foreach (CustomerDetailViewModel c in customersFromService
.OrderBy(customer => customer.CompanyName))Chapter 17 35
{
viewModel.Add(c);
}
InfoLabel.Text += $"\n{viewModel.Count} customers loaded.";
}
catch (Exception ex)
{
ErrorLabel.Text = ex.Message + "\nUsing sample data instead.";
ErrorLabel.IsVisible = true;
viewModel.AddSampleData();
}
BindingContext = viewModel;
}
1. Navigate to Build | Clean Northwind.Maui.Client because changes to Info.plist and
AndroidManifest.xml, like allowing unsecured connections, sometimes require a clean build.
1. Navigate to Build | Build Northwind.Maui.Client.
2. Start the Northwind.Maui.WebApi.Service project.
3. Start the Northwind.Maui.Client project, navigate to the Customers page, and note that 91
customers are loaded from the web service, as shown in Figure 17.9:
Figure 17.9: Loading customers from a web service into the Northwind .NET MAUI app
1. Close the app.36 Implementing Model-View-ViewModel for .NET MAUI
Practicing and exploring
Test your knowledge and understanding by answering some questions, getting some hands-on practice,
and exploring this chapter’s topics with more in-depth research.
Exercise 17.1 – Test your knowledge
Answer the following questions:
1. What interface must a type implement to support two-way binding?
2. What class should a view model inherit from the support automatic notifications when an item
is added and removed from a collection?
1. What class should a view model inherit from and what keyword must be applied to it to allow
automatic definitions of bindable properties?
1. What attribute must you decorate private fields with to allow automatic definitions of bindable
properties?
1. What naming conventions must you use for private fields to allow automatic definitions of
bindable properties?
Exercise 17.2 – Explore topics
Use the links on the following page to learn more details about the topics covered in this chapter:
https://github.com/markjprice/apps-services-net8/blob/main/docs/book-links.md#chapter-
17---implementing-model-view-viewmodel-for-net-maui
Exercise 17.3 – Implement the calculator with MVVM commands
Implement the calculator on the employees page with commands. Hints on how to do so can be found
at the following links:
• https://docs.microsoft.com/en-us/dotnet/maui/fundamentals/data-binding/commanding.
• https://docs.microsoft.com/en-us/dotnet/maui/xaml/fundamentals/mvvm.
Summary
In this chapter, you learned:
• The concepts behind the Model-View-ViewModel pattern.
• How to use the MVVM and MAUI community toolkits.
• How to consume data from a web service.
In the next chapter, you will learn how to integrate a .NET MAUI app with native mobile features.