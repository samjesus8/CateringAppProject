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
            // Check if the user is pressing admin without being logged in
            if (e.Target.Location.OriginalString == "//D_FAULT_ShellContent4" && MauiProgram.currentlyLoggedInUser.UserType == null)
            {
                e.Cancel();

                DisplayAlert("Invalid Operation", " ", "OK");
            }

            // Check if a normal user is trying to press the admin button
            else if (e.Target.Location.OriginalString == "//D_FAULT_ShellContent4" && MauiProgram.currentlyLoggedInUser.UserType != "admin")
            {
                // Cancel the navigation
                e.Cancel();

                DisplayAlert("Access Denied", "You do not have permission to access this page!!!", "OK");
            }
        }
    }
}
