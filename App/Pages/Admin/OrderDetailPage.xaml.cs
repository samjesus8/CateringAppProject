using App.Database;
using System.Collections.ObjectModel;

namespace App.Pages.Admin;

public partial class OrderDetailPage : ContentPage
{
	public Order CurrentOrder { get; set; }
    public ObservableCollection<KeyValuePair<string, int>> Items { get; set; }
    private readonly AppDatabaseEngine databaseEngine;
    public OrderDetailPage(Order order)
	{
		InitializeComponent();
		databaseEngine = new AppDatabaseEngine();

		//Setting fields
		this.CurrentOrder = order;
        this.Items = new ObservableCollection<KeyValuePair<string, int>>(order.Items);
        this.OrderIDEntry.Text = order.OrderID.ToString();
		this.UsernameEntry.Text = order.Username;
		this.StatusEntry.Text = order.Status;

		BindingContext = this;
	}

	private async void OnBackButtonClick(object sender, EventArgs e)
	{
		await Navigation.PopModalAsync();
	}

	private async void OnStatusInProgressClick(object sender, EventArgs e)
	{
		var isChanged = await databaseEngine.ModifyOrderStatusAsync("In Progress", this.CurrentOrder.OrderID);
		if (isChanged.Item1 == true)
		{
			await DisplayAlert("Success", "Updated order status to 'In Progress'", "OK");
			await Navigation.PopModalAsync();
		}
		else
		{
			await DisplayAlert("Error", $"Something went wrong when trying to change the status of this order\n\n{isChanged.Item2}", "OK");
		}
	}

	private async void OnStatusCompleteClick(object sender, EventArgs e)
	{
        var isChanged = await databaseEngine.ModifyOrderStatusAsync("Complete", this.CurrentOrder.OrderID);
        if (isChanged.Item1 == true)
        {
            await DisplayAlert("Success", "Updated order status to 'Complete'", "OK");
            await Navigation.PopModalAsync();
        }
        else
        {
            await DisplayAlert("Error", $"Something went wrong when trying to change the status of this order\n\n{isChanged.Item2}", "OK");
        }
    }
}