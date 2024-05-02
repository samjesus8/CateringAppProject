namespace App
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Navigating += AppShell_Navigating;
        }

        private void AppShell_Navigating(object? sender, ShellNavigatingEventArgs e)
        {
            // Check if the user is pressing account without being logged in
            if (e.Target.Location.OriginalString == "//AccountPage" && MauiProgram.currentlyLoggedInUser.UserType == null)
            {
                e.Cancel();

                DisplayAlert("Invalid Operation", "Please login to view your account!!!", "OK");
            }

            // Check if the user is pressing admin without being logged in
            if (e.Target.Location.OriginalString == "//AdminPage" && MauiProgram.currentlyLoggedInUser.UserType == null)
            {
                e.Cancel();

                DisplayAlert("Invalid Operation", " ", "OK");
            }

            // Check if a normal user is trying to press the admin button
            else if (e.Target.Location.OriginalString == "//AdminPage" && MauiProgram.currentlyLoggedInUser.UserType != "admin")
            {
                // Cancel the navigation
                e.Cancel();

                DisplayAlert("Access Denied", "You do not have permission to access this page!!!", "OK");
            }
        }
    }
}
