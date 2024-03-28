using App.Database;

namespace App.Pages.MainPages;

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
}