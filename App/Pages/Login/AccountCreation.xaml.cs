using App.Database;

namespace App.Pages.Login;

public partial class AccountCreation : ContentPage
{
    private readonly AppDatabaseEngine databaseEngine;
    bool isAdminUser { get; set; }

    public AccountCreation(bool isAdmin)
	{
		InitializeComponent();
        databaseEngine = new AppDatabaseEngine();

        if (isAdmin == true )
        {
            this.isAdminUser = true;
        }
        else
        {
            this.isAdminUser = false;
        }
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        // Navivate back to previous page
        await Navigation.PopAsync();
    }

    private async void OnCreateButtonClicked(object sender, EventArgs e)
    {
        string username = UsernameEntry.Text;
        string password = PasswordEntry.Text;

        if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
        {
            var progressDialog = new ProgressDialogPage();
            await Navigation.PushModalAsync(progressDialog);

            User newUser = new()
            {
                Username = username,
                Password = password,
                UserID = databaseEngine.GenerateID(),
                Address = "Address",
                ProfilePictureURL = "https://imgur.com/1VJsWOS.jpeg"
            };

            var doesExist = await databaseEngine.CheckUserExistsAsync(newUser.Username);
            if (doesExist.Item1 == false)
            {
                var success = await databaseEngine.CreateUserAsync(newUser, this.isAdminUser);

                await Navigation.PopModalAsync();

                if (success.Item1)
                {
                    if (this.isAdminUser == false)
                    {
                        await DisplayAlert("Success", "Account successfully created!", "OK");
                        await Navigation.PopToRootAsync();
                    }
                    else
                    {
                        await DisplayAlert("Success", "Admin User successfully created!", "OK");
                        await Navigation.PopToRootAsync();
                    }
                }
                else
                {
                    await DisplayAlert("Error", $"Failed to create account!!! \n\n{success.Item2}", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", $"That user alerady exists!!!", "OK");
                await Navigation.PopModalAsync();
            }

        }
        else
        {
            await DisplayAlert("Error", "Please enter username and password.", "OK");
        }
    }
}