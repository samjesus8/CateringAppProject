using App.Database;
using App.Pages;

namespace App
{
    public partial class MainPage : ContentPage
    {
        private AppDatabaseEngine databaseEngine;
        public MainPage()
        {
            InitializeComponent();
            databaseEngine = new AppDatabaseEngine();
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

            var userDetails = databaseEngine.GetUser(username);

            if (userDetails.Item1 == true)
            {
                // Valid user has been retrieved
                User user = userDetails.Item2;

                if (user != null)
                {
                    // User object is not null, check the password
                    if (password == user.Password)
                    {
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
            await Navigation.PushAsync(new AccountCreation());
        }
    }
}
