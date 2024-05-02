using App.Database;

namespace App.Pages.Home;

public partial class ItemDetailedViewPage : ContentPage
{
	public FoodItem currentItem { get; set; }
	public ItemDetailedViewPage(FoodItem item)
	{
		InitializeComponent();
		this.currentItem = item;

		//Setting fields
		this.NameEntry.Text = item.Name;
		this.DescriptionEntry.Text = item.Description;
		this.PriceEntry.Text = item.Price.ToString();
		BindingContext = this;
	}

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

	private async void OnBasketAddButtonClicked(object sender, EventArgs e)
	{

	}
}