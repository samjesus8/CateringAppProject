using App.Database;
using System.Collections.ObjectModel;

namespace App.Pages.Admin;

public partial class ViewItemsPage : ContentPage
{
    public ObservableCollection<FoodItem> FoodItem { get; set; }
    public string FoodItemImageURL { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Price { get; set; }
    private readonly AppDatabaseEngine databaseEngine;
    public ViewItemsPage()
	{
		InitializeComponent();
        FoodItem = new ObservableCollection<FoodItem>();
        databaseEngine = new AppDatabaseEngine();
        BindingContext = this;
        LoadItems();
    }

    private async void LoadItems()
    {
        // Retrieve the list of food items from the database
        var items = await databaseEngine.GetAllFoodItemsAsync();

        // Clear the existing items
        FoodItem.Clear();

        // Add the retrieved items to the ObservableCollection
        foreach (var item in items.Item2)
        {
            this.FoodItemImageURL = item.FoodItemImageURL;
            this.Name = item.Name;
            this.Description = item.Description;
            this.Price = item.Price.ToString();
            FoodItem.Add(item);
        }
    }

    private async void OnBackButtonClick(object sender, EventArgs e)
    {
        // Navivate back to previous page
        await Navigation.PopAsync();
    }

    private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null && e.SelectedItem is FoodItem selectedFoodItem)
        {
            await DisplayAlert("Item", $"ItemID: {selectedFoodItem.ItemID} \n" +
                           $"Name: {selectedFoodItem.Name} \n" +
                           $"Description: {selectedFoodItem.Description} \n" +
                           $"Price: {selectedFoodItem.Price}", "Close");
        }
    }
}