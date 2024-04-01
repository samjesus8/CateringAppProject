using App.Database;

namespace App.Pages.Home;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        // Reset global user property
        MauiProgram.currentlyLoggedInUser = new User();

        // Navigate back to the login page
        await Navigation.PopToRootAsync();
    }

    private async void OnBasketClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Test", "Test", "OK");
    }
}