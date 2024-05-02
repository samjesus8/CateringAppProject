using App.Database;

namespace App.Pages.Account;

public partial class AccountPage : ContentPage
{
	public User currentUser { get; set; }
	public AccountPage()
	{
		InitializeComponent();

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

	}
}