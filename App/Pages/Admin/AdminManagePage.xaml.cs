using App.Database;
using App.Pages.Login;
using System.Collections.ObjectModel;

namespace App.Pages.Admin;

public partial class AdminManagePage : ContentPage
{
	public ObservableCollection<User> AdminUsers { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public long UserID { get; set; }
    public string UserType { get; set; }
    private readonly AppDatabaseEngine databaseEngine;
    public AdminManagePage()
	{
		InitializeComponent();
		AdminUsers = new ObservableCollection<User>();
		databaseEngine = new AppDatabaseEngine();
		BindingContext = this;
		LoadAdminUsers();
	}

	private async void OnAddAdminClick(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new AccountCreation(true));
	}

	private async void OnBackButtonClick(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}

	private async void LoadAdminUsers()
	{
		var adminUsers = await databaseEngine.GetAllAdminUsersAsync();

		AdminUsers.Clear();

		foreach (User user in adminUsers.Item2)
		{
			this.Username = user.Username;
			this.Password = user.Password;
			this.UserID = user.UserID;
			this.UserType = user.UserType;
			AdminUsers.Add(user);
		}
	}
}