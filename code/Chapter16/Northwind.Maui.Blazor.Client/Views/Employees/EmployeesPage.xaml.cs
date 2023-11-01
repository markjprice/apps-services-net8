namespace Northwind.Maui.Blazor.Client.Views;

public partial class EmployeesPage : ContentPage
{
	public EmployeesPage()
	{
		InitializeComponent();
	}

  private async void CopyToClipboardButton_Clicked(object sender, EventArgs e)
  {
    await Clipboard.Default.SetTextAsync(NotesTextBox.Text);
  }

  private async void PasteFromClipboardButton_Clicked(
    object sender, EventArgs e)
  {
    if (Clipboard.HasText)
    {
      NotesTextBox.Text = await Clipboard.Default.GetTextAsync();
    }
  }

  private async void PickTextFileButton_Clicked(object sender, EventArgs e)
  {
    try
    {
      FilePickerFileType textFileTypes = new(
        new Dictionary<DevicePlatform, IEnumerable<string>>
        {
          { DevicePlatform.iOS, new[] { "public.plain-text" } },
          { DevicePlatform.Android, new[] { "text/plain" } },
          { DevicePlatform.WinUI, new[] { ".txt" } },
          { DevicePlatform.Tizen, new[] { "*/*" } },
          { DevicePlatform.macOS, new[] { "txt" } }
        });

      PickOptions options = new()
      {
        PickerTitle = "Pick a text file",
        FileTypes = textFileTypes
      };

      FileResult result = await FilePicker.Default.PickAsync(options);

      if (result != null)
      {
        await using var stream = await result.OpenReadAsync();
        FileContentsLabel.Text = new StreamReader(stream).ReadToEnd();
      }

      FilePathLabel.Text = result.FullPath;
    }
    catch (Exception ex)
    {
      await DisplayAlert(title: "Exception",
        message: ex.Message, cancel: "OK");
    }
  }

  private async void PickImageButton_Clicked(object sender, EventArgs e)
  {
    FileResult photo = await MediaPicker.Default.PickPhotoAsync();

    if (photo != null)
    {
      FileImage.Source = ImageSource.FromFile(photo.FullPath);

      FilePathLabel.Text = photo.FullPath;
    }
    else
    {
      await DisplayAlert(title: "Exception",
        message: "Photo was null.", cancel: "OK");
    }
  }

  private async void TakePhotoButton_Clicked(object sender, EventArgs e)
  {
    if (MediaPicker.Default.IsCaptureSupported)
    {
      FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

      if (photo != null)
      {
        FileImage.Source = ImageSource.FromFile(photo.FullPath);

        FilePathLabel.Text = photo.FullPath;
      }
      else
      {
        await DisplayAlert(title: "Exception",
          message: "Photo was null.", cancel: "OK");
      }
    }
    else
    {
      await DisplayAlert(title: "Sorry",
        message: "Image capture is not supported on this device.",
        cancel: "OK");
    }
  }
}