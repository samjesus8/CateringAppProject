using App.Database;

namespace App.Pages.Admin;

public partial class AddFoodItemPage : ContentPage
{
    private readonly AppDatabaseEngine databaseEngine;
    public AddFoodItemPage()
	{
		InitializeComponent();
        databaseEngine = new AppDatabaseEngine();
        ItemIDEntry.Text = databaseEngine.GenerateID().ToString();
	}

    private async void OnBackButtonClick(object sender, EventArgs e)
    {
        // Navivate back to previous page
        await Navigation.PopAsync();
    }

    private async void OnSubmitClick(object sender, EventArgs e)
    {
        // Validate input and save the new food item to the database
        long itemId = long.Parse(ItemIDEntry.Text);
        string name = NameEntry.Text;
        string description = DescriptionEntry.Text;
        string imageURL = ImageURLEntry.Text;
        double price;

        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(description) || !double.TryParse(PriceEntry.Text, out price))
        {
            await DisplayAlert("Error", "Please enter valid information for all fields.", "OK");
            return;
        }

        // Create a new FoodItem object
        FoodItem newFoodItem = new()
        {
            Name = name,
            ItemID = itemId,
            Description = description,
            Price = price,
            FoodItemImageURL = imageURL
        };

        var isStored = await databaseEngine.StoreFoodItemAsync(newFoodItem);

        if (isStored.Item1)
        {
            // Display success message
            await DisplayAlert("Success", "New food item added successfully!!!", "OK");

            // Navigate back to the AdminPage
            await Navigation.PopAsync();
        }

        else
        {
            //Display Error Message
            await DisplayAlert("Error", $"Could not add this item!!! \n\n{isStored.Item2}", "OK");
        }
    }
}