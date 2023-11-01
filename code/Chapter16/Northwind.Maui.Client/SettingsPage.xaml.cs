using Northwind.Maui.Client.Controls; // To use enum Theme.
using Northwind.Maui.Client.Resources.Styles; // To use DarkModeTheme.

namespace Northwind.Maui.Client;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
	}

  private void ThemePicker_SelectionChanged(object sender, EventArgs e)
  {
    Picker picker = sender as Picker;
    Theme theme = (Theme)picker.SelectedItem;

    ICollection<ResourceDictionary> resources =
      Application.Current.Resources.MergedDictionaries;

    if (resources is not null)
    {
      resources.Clear();

      resources.Add(new Resources.Styles.Northwind());
      resources.Add(new LightDarkModeColors());

      ResourceDictionary themeResource = theme switch
      {
        Theme.Dark => new DarkModeTheme(),
        Theme.Light => new LightModeTheme(),
        _ => new SystemModeTheme()
      };

      resources.Add(themeResource);
    }
  }

  private void ThemePicker_Loaded(object sender, EventArgs e)
  {
    ThemePicker.SelectedItem = Theme.System;
  }
}