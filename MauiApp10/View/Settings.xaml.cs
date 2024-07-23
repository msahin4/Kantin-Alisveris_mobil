namespace MauiApp10;

public partial class Settings : ContentPage
{
	public Settings()
	{
		InitializeComponent();
	}

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Çýkýþ Yap", "Emin misiniz?", "Evet", "Hayýr");

        if (answer)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
        else
        {

        }
    }

    private async void sifreDegistirClicked(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PushAsync(new sifreDegistir());

    }
}