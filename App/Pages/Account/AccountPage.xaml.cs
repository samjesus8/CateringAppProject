using App.Database;
using System.ComponentModel;

namespace App.Pages.Account;

public partial class AccountPage : ContentPage, INotifyPropertyChanged
{
    private readonly AppDatabaseEngine databaseEngine;
    private User _currentUser;
    public User currentUser
    {
        get => _currentUser;
        set
        {
            if (_currentUser != value)
            {
                _currentUser = value;
                OnPropertyChanged(nameof(currentUser));
            }
        }
    }
    public event PropertyChangedEventHandler PropertyChanged;
    public AccountPage()
	{
		InitializeComponent();
		this.databaseEngine = new AppDatabaseEngine();
        LoadUser();
        BindingContext = this;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		LoadUser();
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private async void LoadUser()
	{
		var user = await databaseEngine.GetUserAsync(MauiProgram.currentlyLoggedInUser.Username);
		this.currentUser = user.Item2;
		this.UsernameEntry.Text = user.Item2.Username;
		this.AddressEntry.Text = user.Item2.Address;
	}

    private async void OnUploadProfilePictureClicked(object sender, EventArgs e)
	{
		await Navigation.PushModalAsync(new ProfilePictureUpdatePage());
	}

	private async void OnChangeClicked(object sender, EventArgs e)
	{
		//If password field isn't empty, replace the user's password. Else don't change it
		if (!string.IsNullOrWhiteSpace(this.PasswordEntry.Text))
		{
			MauiProgram.currentlyLoggedInUser.Password = this.PasswordEntry.Text;
		}

		if (!string.IsNullOrWhiteSpace(this.UsernameEntry.Text) || !string.IsNullOrWhiteSpace(this.AddressEntry.Text))
		{
			MauiProgram.currentlyLoggedInUser.Username = this.UsernameEntry.Text;
			MauiProgram.currentlyLoggedInUser.Address = this.AddressEntry.Text;

			var isModified = await databaseEngine.ModifyUserAsync(MauiProgram.currentlyLoggedInUser);

			if (isModified.Item1 == true)
			{
				await DisplayAlert("Successfully changed account details", "", "OK");
				this.PasswordEntry.Text = string.Empty;
			}
            else
            {
                await DisplayAlert("Something went wrong", $"{isModified.Item2}", "OK");
            }
        }

		else
		{
			await DisplayAlert("Error", "Please ensure all fields are not empty!!!", "OK");
		}
	}
}