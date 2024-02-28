using App.Pages.SidebarPages;

namespace App.Pages;

public partial class MenuPage : ContentPage
{
	public MenuPage()
	{
		InitializeComponent();
	}

    private async void OnMenuItemClicked(object sender, EventArgs e)
    {
        // Close the sidebar menu
        await Navigation.PopAsync();

        // Navigate to the selected page or perform other actions based on the clicked menu item
        if (sender is Button button)
        {
            string buttonText = button.Text;
            switch (buttonText)
            {
                case "Home":
                    // Navigate to Home page
                    await Navigation.PushAsync(new HomePage());
                    break;
                case "Option 2":
                    // Navigate to Option 2 page
                    await Navigation.PushAsync(new TestPage());
                    break;

                    // Add more cases for other menu options
            }
        }
    }
}