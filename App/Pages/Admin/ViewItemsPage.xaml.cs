namespace App.Pages.Admin;

public partial class ViewItemsPage : ContentPage
{
	public ViewItemsPage()
	{
		InitializeComponent();
	}

    private async void OnBackButtonClick(object sender, EventArgs e)
    {
        // Navivate back to previous page
        await Navigation.PopAsync();
    }
}