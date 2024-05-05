using App.Database;
using System.Collections.ObjectModel;

namespace App.Pages.Admin;

public partial class ViewOrdersPage : ContentPage
{
	public ObservableCollection<Order> IncomingOrders { get; set; }
    public ObservableCollection<Order> InProgressOrders { get; set; }
    public ObservableCollection<Order> CompletedOrders { get; set; }

    private readonly AppDatabaseEngine databaseEngine;
	public ViewOrdersPage()
	{
		InitializeComponent();

		//Initialise Collections
		IncomingOrders = new ObservableCollection<Order>();
		InProgressOrders = new ObservableCollection<Order>();
		CompletedOrders = new ObservableCollection<Order>();

		//Other Properties
		databaseEngine = new AppDatabaseEngine();
		BindingContext = this;
		LoadOrders();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		LoadOrders();
    }

    public async void LoadOrders()
	{
		// Load orders from database
		var orders = await databaseEngine.GetAllOrdersAsync();

		// Clear orders list
		IncomingOrders.Clear();

		// Add the orders by status type to its associated list
		foreach (var order in orders.Item2)
		{
			if (order.Status == "Submitted Order")
			{
				IncomingOrders.Add(order);
			}

			if (order.Status == "In Progress")
			{
				InProgressOrders.Add(order);
			}

			if (order.Status == "Complete")
			{
				CompletedOrders.Add(order);
			}
		}
	}

	private async void OnBackButtonClick(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}

    private async void Incoming_ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
		if (e.SelectedItem != null && e.SelectedItem is Order selectedOrder)
		{
			await Navigation.PushModalAsync(new OrderDetailPage(selectedOrder));
		}

        if (e.SelectedItem == null)
            return;

        if (sender is ListView listView)
            listView.SelectedItem = null;
    }

    private async void InProgress_ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
	{
        if (e.SelectedItem != null && e.SelectedItem is Order selectedOrder)
        {
            await Navigation.PushModalAsync(new OrderDetailPage(selectedOrder));
        }

        if (e.SelectedItem == null)
            return;

        if (sender is ListView listView)
            listView.SelectedItem = null;
    }

    private async void Completed_ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
	{
        if (e.SelectedItem != null && e.SelectedItem is Order selectedOrder)
        {
            await Navigation.PushModalAsync(new OrderDetailPage(selectedOrder));
        }

        if (e.SelectedItem == null)
            return;

        if (sender is ListView listView)
            listView.SelectedItem = null;
    }
}