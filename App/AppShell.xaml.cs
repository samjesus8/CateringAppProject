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
            var target = e.Target.Location.OriginalString;

            if (e.Target.Location.OriginalString == "//D_FAULT_ShellContent4" && MauiProgram.currentlyLoggedInUser.UserType == null)
            {
                e.Cancel();

                DisplayAlert("Invalid Operation", " ", "OK");
            }

            else if (e.Target.Location.OriginalString == "//D_FAULT_ShellContent4" && MauiProgram.currentlyLoggedInUser.UserType != "admin")
            {
                // Cancel the navigation
                e.Cancel();

                DisplayAlert("Access Denied", "You do not have permission to access this page!!!", "OK");
            }
        }
    }
}
