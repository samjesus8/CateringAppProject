namespace App.Pages;

public partial class AccountCreation : ContentPage
{
	public AccountCreation()
	{
		InitializeComponent();
	}

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private void OnCreateButtonClicked(object sender, EventArgs e)
    {
        string username = UsernameEntry.Text;
        string password = PasswordEntry.Text;

        DisplayAlert("Account Creation", $"Username: {username}\nPassword: {password}\nAccount created successfully!", "OK");
    }
}