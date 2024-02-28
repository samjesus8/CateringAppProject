using App.Database;

namespace App.Pages.MainPages;

public partial class AccountCreation : ContentPage
{
    private AppDatabaseEngine databaseEngine;

    public AccountCreation()
	{
		InitializeComponent();
        databaseEngine = new AppDatabaseEngine();
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
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

            bool success = await Task.Run(() => databaseEngine.CreateUser(newUser));

            await Navigation.PopModalAsync();

            if (success)
            {
                await DisplayAlert("Success", "Account created successfully!", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Failed to create account. Please try again later.", "OK");
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