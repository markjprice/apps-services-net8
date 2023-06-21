namespace Northwind.Maui.Client;

public partial class CategoriesPage : ContentPage
{
	public CategoriesPage()
	{
		InitializeComponent();
	}

  private void ClickMeButton_Clicked(object sender, EventArgs e)
  {
    ClickMeButton.Text = DateTime.Now.ToString("hh:mm:ss");
  }
}