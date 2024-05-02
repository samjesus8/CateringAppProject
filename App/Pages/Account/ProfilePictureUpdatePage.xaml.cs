using App.Database;

namespace App.Pages.Account;

public partial class ProfilePictureUpdatePage : ContentPage
{
    private readonly AppDatabaseEngine databaseEngine;
    public ProfilePictureUpdatePage()
	{
		InitializeComponent();
		databaseEngine = new AppDatabaseEngine();
	}

	private async void OnSubmitClicked(object sender, EventArgs e)
	{
		if (!string.IsNullOrWhiteSpace(URLEntry.Text))
		{
			MauiProgram.currentlyLoggedInUser.ProfilePictureURL = URLEntry.Text;
			var isModified = await databaseEngine.ModifyUserAsync(MauiProgram.currentlyLoggedInUser);

			if (isModified.Item1 == true)
			{
				await DisplayAlert("Successfully changed Picture", "", "OK");
				await Navigation.PopModalAsync();
			}
			else
			{
				await DisplayAlert("Something went wrong", $"{isModified.Item2}", "OK");
			}
		}
		else
		{
			await DisplayAlert("Please enter a URL", "", "OK");
		}
	}

	private async void OnBackButtonClicked(object sender, EventArgs e)
	{
		await Navigation.PopModalAsync();
	}
}