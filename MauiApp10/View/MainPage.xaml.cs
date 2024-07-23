using Microsoft.Maui.Controls;
namespace MauiApp10

{
    public partial class MainPage : ContentPage
    {
        private readonly DatabaseService _databaseService;

        public MainPage()
        {
            InitializeComponent();
            int ogrenciNo = App.UserService.OgrenciNo;
            
            _databaseService = new DatabaseService();
            LoadBalanceAsync(ogrenciNo);
        }

        private async void LoadBalanceAsync(int ogrenciNo)
        {
            try
            {
                int bakiye = await _databaseService.GetBalanceAsync(ogrenciNo);
                myLabel.Text = $"₺ {bakiye}";
            }
            catch (Exception ex)
            {
                myLabel.Text = "Bakiye alınırken hata oluştu.";
            }
        }



        private async void harcamaLimiti_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PushAsync(new limitGuncelle());
        }


        private void BakiyeYukle_Clicked(object sender, EventArgs e)
        {

        }


    }

}
