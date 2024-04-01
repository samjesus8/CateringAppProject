namespace App.Pages.MainPages;

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
}