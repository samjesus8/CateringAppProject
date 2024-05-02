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

	private async void OnDecreaseQuantityClicked(object sender, EventArgs e)
	{
		int value = int.Parse(this.QuantityEntry.Text);

		if (value > 0)
		{
            this.QuantityEntry.Text = (--value).ToString();
        }
	}

	private async void OnIncreaseQuantityClicked(object sender, EventArgs e)
	{
        int value = int.Parse(this.QuantityEntry.Text);

		if (value < 99)
		{
            this.QuantityEntry.Text = (++value).ToString();
        }
    }


    private async void OnBasketAddButtonClicked(object sender, EventArgs e)
	{
		//If item dosen't exist in the basket then add it
		if (!MauiProgram.currentUserBasket.ContainsKey(this.currentItem.Name))
		{
			if (int.TryParse(this.QuantityEntry.Text, out var quantity))
			{
                MauiProgram.currentUserBasket.Add(this.currentItem.Name, quantity);
				await DisplayAlert("Success", "Added item to basket!!!", "OK");
				await Navigation.PopModalAsync();
            }
			else
			{
				await DisplayAlert("Error", "Please enter a valid integer!!!", "OK");
			}
		}
		//Otherwise add the quantity of items to its existing quantity
		else
		{
			MauiProgram.currentUserBasket[this.currentItem.Name] = MauiProgram.currentUserBasket[this.currentItem.Name] + int.Parse(this.QuantityEntry.Text);
            await DisplayAlert("Success", "Added item to basket!!!", "OK");
            await Navigation.PopModalAsync();
        }
	}
}