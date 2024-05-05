using App.Database;
using System.Collections.ObjectModel;

namespace App.Pages.Orders;

public partial class UserOrderDetailPage : ContentPage
{
    public Order CurrentOrder { get; set; }
    public ObservableCollection<KeyValuePair<string, int>> Items { get; set; }
    public UserOrderDetailPage(Order order)
	{
		InitializeComponent();

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
}