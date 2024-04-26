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
                UserID = GenerateID(),
            };

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
            await DisplayAlert("Error", "Please enter username and password.", "OK");
        }
    }

    private long GenerateID()
    {
        var random = new Random();

        long minValue = 1000000000000;
        long maxValue = 9999999999999;

        long randomNumber = (long)random.Next((int)(minValue >> 32), int.MaxValue) << 32 | (long)random.Next();
        long result = randomNumber % (maxValue - minValue + 1) + minValue;

        return result;
    }
}