using App.Database;
using System.Collections.ObjectModel;

namespace App.Pages.Orders;

public partial class UserViewOrdersPage : ContentPage
{
    public ObservableCollection<Order> Orders { get; set; }
    private readonly AppDatabaseEngine databaseEngine;
    public UserViewOrdersPage()
    {
        InitializeComponent();
        Orders = new ObservableCollection<Order>();
        databaseEngine = new AppDatabaseEngine();
        BindingContext = this;
        LoadUserOrders();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadUserOrders();
    }

    public async Task LoadUserOrders()
    {
        // Load orders from database
        var orders = await databaseEngine.GetAllOrdersAsync();

        // Clear orders list
        Orders.Clear();

        // Sort orders by username
        foreach (var order in orders.Item2)
        {
            if (order.Username == MauiProgram.currentlyLoggedInUser.Username)
            {
                Orders.Add(order);
            }
        }
    }

    private async void OnBackButtonClick(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null && e.SelectedItem is Order selectedOrder)
        {
            await Navigation.PushModalAsync(new UserOrderDetailPage(selectedOrder));
        }

        if (e.SelectedItem == null)
            return;

        if (sender is ListView listView)
            listView.SelectedItem = null;
    }
}