using App.Database;

namespace App.Pages.Admin;

public partial class EditAdminUserForm : ContentPage
{
	public User SelectedAdminUser { get; set; }
    private readonly AppDatabaseEngine databaseEngine;
    public EditAdminUserForm(User selectedAdminUser)
	{
		InitializeComponent();
		databaseEngine = new AppDatabaseEngine();

		this.SelectedAdminUser = selectedAdminUser;
		this.UserIDEntry.Text = selectedAdminUser.UserID.ToString();
		this.UsernameEntry.Text = selectedAdminUser.Username;
	}

	private async void OnBackButtonClick(object sender, EventArgs e)
	{
		await Navigation.PopModalAsync();
	}

	private async void OnSubmitChangesClicked(object sender, EventArgs e)
	{
		var modifiedUser = new User
		{
			UserID = this.SelectedAdminUser.UserID,
			Username = this.UsernameEntry.Text,
			Password = this.PasswordEntry.Text,
			UserType = this.SelectedAdminUser.UserType,
			Address = this.SelectedAdminUser.Address,
			ProfilePictureURL = this.SelectedAdminUser.ProfilePictureURL
		};

		var isModified = await databaseEngine.ModifyUserAsync(modifiedUser);
		if (isModified.Item1 == true)
		{
            await DisplayAlert("Success", "Sucessfully modified user!!!", "OK");
            await Navigation.PopModalAsync();
        }
		else
		{
			await DisplayAlert("Error", $"Something went wrong when trying to modify this user \n\nError: {isModified.Item2}", "OK");
		}
	}
}