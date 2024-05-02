namespace App.Pages.Admin;

public partial class AdminPage : ContentPage
{
	public AdminPage()
	{
		InitializeComponent();
	}

    private async void OnAddItemClick(object sender, EventArgs e)
    {
        // Navigate to the page to add a new food item
        await Navigation.PushAsync(new AddFoodItemPage());
    }

    private async void OnViewItemsClick(object sender, EventArgs e)
    {
        // Navigate to the page to view current items
        await Navigation.PushAsync(new ViewItemsPage());
    }

    private async void OnEditItemsClick(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EditItemPage());
    }

    private async void OnManageAdminClick(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AdminManagePage());
    }

    private async void OnDeleteItemClick(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new DeleteItemPage());
    }
}