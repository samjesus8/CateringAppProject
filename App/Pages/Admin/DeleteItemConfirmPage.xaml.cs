using App.Database;

namespace App.Pages.Admin;

public partial class DeleteItemConfirmPage : ContentPage
{
	private FoodItem FoodItem { get; set; }
    private readonly AppDatabaseEngine databaseEngine;
    public DeleteItemConfirmPage(FoodItem itemToDelete)
	{
		InitializeComponent();
		databaseEngine = new AppDatabaseEngine();
		this.FoodItem = itemToDelete;
	}

	private async void OnYesClick(object sender, EventArgs e)
	{
		var isDeleted = await databaseEngine.DeleteItemAsync(this.FoodItem.ItemID);
		if (isDeleted.Item1 == true)
		{
			await DisplayAlert("Successfully deleted item", "", "OK");
			await Navigation.PopModalAsync();
		}
		else
		{
			await DisplayAlert("Error", $"{isDeleted.Item2}", "OK");
			await Navigation.PopModalAsync();
		}
	}

	private async void OnNoClick(object sender, EventArgs e)
	{
		await Navigation.PopModalAsync();
	}
}