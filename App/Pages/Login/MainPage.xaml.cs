using App.Database;
using App.Pages.Home;

namespace App.Pages.Login
{
    public partial class MainPage : ContentPage
    {
        private readonly AppDatabaseEngine databaseEngine;

        public MainPage()
        {
            InitializeComponent();
            databaseEngine = new AppDatabaseEngine();

            // Clear username & password fields when loading up the form for the first time
            UsernameEntry.Text = string.Empty;
            PasswordEntry.Text = string.Empty;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Clear username and password fields when the page appears
            UsernameEntry.Text = string.Empty;
            PasswordEntry.Text = string.Empty;
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Error", "Please enter both username and password.", "OK");
                return;
            }

            var userDetails = await databaseEngine.GetUserAsync(username);

            if (userDetails.Item1 == true)
            {
                // Valid user has been retrieved
                User user = userDetails.Item2;

                if (user != null)
                {
                    // User object is not null, check the password
                    if (password == user.Password)
                    {
                        // Set global user instance
                        MauiProgram.currentlyLoggedInUser = user;

                        // Dismiss Keyboard
                        this.Unfocus();

                        // Correct password
                        await Navigation.PushAsync(new HomePage());
                    }
                    else
                    {
                        // Incorrect password
                        await DisplayAlert("Error", "Incorrect password!!!", "OK");
                    }
                }
                else
                {
                    // User object is null, this should not happen if GetUser is properly implemented
                    await DisplayAlert("Error", "Something went wrong when trying to retrieve user details!!!", "OK");
                }
            }
            else
            {
                //No such user has been found
                await DisplayAlert("Error", "User does not exist!!!", "OK");
            }
        }

        private async void OnCreateAccountClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AccountCreation(false));
        }
    }
}
