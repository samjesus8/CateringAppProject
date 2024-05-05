using App.Database;
using System.Collections.ObjectModel;

namespace App.Pages.Admin;

public partial class ViewOrdersPage : ContentPage
{
	public ObservableCollection<Order> Orders { get; set; }
	private readonly AppDatabaseEngine databaseEngine;
	public ViewOrdersPage()
	{
		InitializeComponent();
		Orders = new ObservableCollection<Order>();
		databaseEngine = new AppDatabaseEngine();
		BindingContext = this;
		LoadOrders();
	}

	public async void LoadOrders()
	{
		// Load orders from database
		var orders = await databaseEngine.GetAllOrdersAsync();

		// Clear orders list
		Orders.Clear();

		// Add the orders to the list
		foreach (var order in orders.Item2)
		{
			Orders.Add(order);
		}
	}

	private async void OnBackButtonClick(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}

    private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {

    }
}