namespace App.Pages;

public partial class AboutPage : ContentPage
{
    public string DeviceName { get; set; }
    public string DeviceOS { get; set; }
    public string DeviceManufacturer { get; set; }
	public AboutPage()
	{
		InitializeComponent();
        this.DeviceName = DeviceInfo.Name;
        this.DeviceManufacturer = DeviceInfo.Manufacturer;
        this.DeviceOS = $"{DeviceInfo.Platform} {DeviceInfo.VersionString}";
        BindingContext = this;
    }

    private async void LearnMore_Clicked(object sender, EventArgs e)
    {
        // Navigate to the specified URL in the system browser.
        await Launcher.Default.OpenAsync("https://aka.ms/maui");
    }
}