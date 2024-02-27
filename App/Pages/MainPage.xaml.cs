namespace App
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnLoginClicked(object sender, EventArgs e)
        {
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;

            DisplayAlert("Login Information", $"Username: {username}\nPassword: {password}", "OK");
        }
    }
}
