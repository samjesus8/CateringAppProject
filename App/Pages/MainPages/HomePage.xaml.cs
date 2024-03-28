namespace App.Pages.MainPages;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        // Navigate back to the login page
        await Navigation.PopToRootAsync();
    }
}