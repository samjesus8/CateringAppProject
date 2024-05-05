using App.Database;
using System.Collections.ObjectModel;

namespace App.Pages.Admin;

public partial class OrderDetailPage : ContentPage
{
	public Order CurrentOrder { get; set; }
    public ObservableCollection<KeyValuePair<string, int>> Items { get; set; }
    public OrderDetailPage(Order order)
	{
		InitializeComponent();

		//Setting fields
		this.CurrentOrder = order;
        this.Items = new ObservableCollection<KeyValuePair<string, int>>(order.Items);
        this.OrderIDEntry.Text = order.OrderID.ToString();
		this.UsernameEntry.Text = order.Username;

		BindingContext = this;
	}

	private async void OnBackButtonClick(object sender, EventArgs e)
	{
		await Navigation.PopModalAsync();
	}

	private async void OnStatusInProgressClick(object sender, EventArgs e)
	{

	}

	private async void OnStatusCompleteClick(object sender, EventArgs e)
	{

	}
}