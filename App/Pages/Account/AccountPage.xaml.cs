using App.Database;

namespace App.Pages.Account;

public partial class AccountPage : ContentPage
{
    private readonly AppDatabaseEngine databaseEngine;
    public User currentUser { get; set; }
	public AccountPage()
	{
		InitializeComponent();
		this.databaseEngine = new AppDatabaseEngine();

		//Set current user to the currently logged in user
		this.currentUser = MauiProgram.currentlyLoggedInUser;

		//Setting fields
		this.UsernameEntry.Text = this.currentUser.Username;
		this.AddressEntry.Text = this.currentUser.Address;
		BindingContext = this;
	}

	private async void OnUploadProfilePictureClicked(object sender, EventArgs e)
	{
		await Navigation.PushModalAsync(new ProfilePictureUpdatePage());
	}

	private async void OnChangeClicked(object sender, EventArgs e)
	{
		if (!string.IsNullOrWhiteSpace(this.UsernameEntry.Text) || !string.IsNullOrWhiteSpace(this.PasswordEntry.Text) || !string.IsNullOrWhiteSpace(this.AddressEntry.Text))
		{
			MauiProgram.currentlyLoggedInUser.Username = this.UsernameEntry.Text;
			MauiProgram.currentlyLoggedInUser.Password = this.PasswordEntry.Text;
			MauiProgram.currentlyLoggedInUser.Address = this.AddressEntry.Text;

			var isModified = await databaseEngine.ModifyUserAsync(MauiProgram.currentlyLoggedInUser);

			if (isModified.Item1 == true)
			{
				await DisplayAlert("Successfully changed account details", "", "OK");
				this.PasswordEntry.Text = string.Empty;
			}
            else
            {
                await DisplayAlert("Something went wrong", $"{isModified.Item2}", "OK");
            }
        }

		else
		{
			await DisplayAlert("Error", "Please ensure all fields are not empty!!!", "OK");
		}
	}
}