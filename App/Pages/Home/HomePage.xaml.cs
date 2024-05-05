using App.Database;
using System.Collections.ObjectModel;

namespace App.Pages.Home;

public partial class HomePage : ContentPage
{
    public ObservableCollection<FoodItem> FoodItem { get; set; }
    public string FoodItemImageURL { get; set; }
    public string Name { get; set; }
    public string Price { get; set; }

    private readonly AppDatabaseEngine databaseEngine;
    public HomePage()
	{
		InitializeComponent();
        databaseEngine = new AppDatabaseEngine();
        FoodItem = new ObservableCollection<FoodItem>();
        BindingContext = this;
        LoadItems();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
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
            this.Price = item.Price.ToString();
            FoodItem.Add(item);
        }
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        // Reset global user property
        MauiProgram.currentlyLoggedInUser = new User();

        // Navigate back to the login page
        await Navigation.PopToRootAsync();
    }

    private async void OnBasketClicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new BasketPage());
    }

    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is FoodItem selectedFoodItem)
        {
            await Navigation.PushModalAsync(new ItemDetailedViewPage(selectedFoodItem));
        }

        // Handle deselection of the selected item
        if (e.CurrentSelection == null)
            return;

        if (sender is CollectionView collectionView)
            collectionView.SelectedItem = null;
    }
}