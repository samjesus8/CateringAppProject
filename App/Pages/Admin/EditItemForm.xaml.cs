using App.Database;

namespace App.Pages.Admin;

public partial class EditItemForm : ContentPage
{
	public FoodItem SelectedFoodItem { get; set; }
    private readonly AppDatabaseEngine databaseEngine;
    public EditItemForm(FoodItem selectedFoodItem)
	{
        InitializeComponent();

		//Setting input fields to the selected item
		this.SelectedFoodItem = selectedFoodItem;
		this.NameEntry.Text = selectedFoodItem.Name;
		this.DescriptionEntry.Text = selectedFoodItem.Description;
		this.PriceEntry.Text = selectedFoodItem.Price.ToString();
		this.ImageURLEntry.Text = selectedFoodItem.FoodItemImageURL;

		//Load in Database
		databaseEngine = new AppDatabaseEngine();
	}

    private async void OnSubmitChangesClicked(object sender, EventArgs e)
	{
        var priceParse = double.TryParse(this.PriceEntry.Text, out _);
		if (priceParse == false)
		{
			await DisplayAlert("Error", "Price must be a valid integer", "OK");
			return;
		}

		var modifiedItem = new FoodItem
		{
			ItemID = SelectedFoodItem.ItemID,
			Name = this.NameEntry.Text,
			Description = this.DescriptionEntry.Text,
			Price = double.Parse(this.PriceEntry.Text),
			FoodItemImageURL = this.ImageURLEntry.Text
		};

		var isModified = databaseEngine.ModifyItem(modifiedItem);
		if (isModified.Item1 == true)
		{
			await DisplayAlert("Sucess", "Sucessfully modified item!!!", "OK");
			await Navigation.PopModalAsync();
		}
		else
		{
			await DisplayAlert("Fail", $"Something went wrong when trying to modify this item \n\nError: {isModified.Item2}", "OK");
		}
	}

	private async void OnBackButtonClick(object sender, EventArgs e)
	{
		await Navigation.PopModalAsync();
	}
}