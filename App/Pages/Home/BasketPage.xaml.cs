using App.Database;
using System.ComponentModel;

namespace App.Pages.Home;

public partial class BasketPage : ContentPage, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private Dictionary<string, int> _basketItems;
    public Dictionary<string, int> BasketItems
    {
        get => _basketItems;
        set
        {
            _basketItems = value;
            OnPropertyChanged(nameof(BasketItems));
        }
    }

    private double _totalPrice;
    public double TotalPrice
    {
        get => _totalPrice;
        set
        {
            if (_totalPrice != value)
            {
                _totalPrice = value;
                OnPropertyChanged(nameof(TotalPrice));
            }
        }
    }

    private readonly AppDatabaseEngine databaseEngine;
    public BasketPage()
	{
		InitializeComponent();
        databaseEngine = new AppDatabaseEngine();
        BasketItems = new Dictionary<string, int>();
        LoadBasketItems();
        CalculateTotalPrice();
        BindingContext = this;
    }

    private void LoadBasketItems()
    {
        foreach (var item in MauiProgram.currentUserBasket)
        {
            BasketItems.Add(item.Key, item.Value);
        }
    }

    private async void OnBackButtonClick(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private async void OnDecreaseQuantityClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        string itemName = (string)button.CommandParameter;

        if (BasketItems.ContainsKey(itemName))
        {
            MauiProgram.currentUserBasket[itemName]--;
            BasketItems[itemName]--;

            if (BasketItems[itemName] <= 0)
            {
                MauiProgram.currentUserBasket.Remove(itemName);
                BasketItems.Remove(itemName);
            }
            await CalculateTotalPrice();
        }
    }

    private async void OnIncreaseQuantityClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        string itemName = (string)button.CommandParameter;

        if (BasketItems.ContainsKey(itemName))
        {
            MauiProgram.currentUserBasket[itemName]++;
            BasketItems[itemName]++;
        }
        else
        {
            MauiProgram.currentUserBasket[itemName] = 1;
            BasketItems[itemName] = 1;
        }
        await CalculateTotalPrice();
    }

    private void OnRemoveItemClicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        string itemName = (string)button.CommandParameter;

        if (BasketItems.ContainsKey(itemName))
        {
            MauiProgram.currentUserBasket.Remove(itemName);
            BasketItems.Remove(itemName);
            CalculateTotalPrice();
        }
    }

    private void OnProceedToPaymentClicked(object sender, EventArgs e)
    {
        // Implement logic to proceed to payment
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private async Task<double> CalculateTotalPrice()
    {
        double totalPrice = 0;

        foreach (var item in BasketItems.Keys)
        {
            double itemPrice = await GetItemPrice(item);
            int quantity = BasketItems[item];

            var totalAmount = itemPrice * quantity;
            totalPrice += totalAmount;
        }

        this.TotalPrice = double.Parse(totalPrice.ToString("F2"));

        return totalPrice;
    }

    private async Task<double> GetItemPrice(string itemName)
    {
        var items = await databaseEngine.GetAllFoodItemsAsync();
        foreach (var item in items.Item2)
        {
            if (item.Name == itemName)
            {
                return item.Price;
            }
        }

        //If no item was found, return 0
        return 0.0;
    }
}