using Dapper;

namespace MauiApp10;

public partial class limitGuncelle : ContentPage
{
    public limitGuncelle()
    {
        InitializeComponent();
        _databaseService = new DatabaseService(); // DatabaseService sýnýfýndan bir örnek oluþturuyoruz.
    }

    private readonly DatabaseService _databaseService;


    private async void kaydetClicked(object sender, EventArgs e)
    {
        string limitText = inputLimit.Text;
        if (int.TryParse(limitText, out int limit))
        {
            int ogrenciNo = App.UserService.OgrenciNo;
            string query = "update tbl_kart set gunlukLimit = @yeniLimit where ogrenciNo=@ogrenciNo";

            try
            {
                int affectedRows = await _databaseService.SqlConnection.ExecuteAsync(query, new { yeniLimit = limit, ogrenciNo = ogrenciNo });

                if (affectedRows > 0)
                {
                    await DisplayAlert("Baþarýlý", "Günlük limit baþarýyla güncellendi.", "Tamam");
                }
                else
                {
                    await DisplayAlert("Hata", "Günlük limit güncellenirken bir hata oluþtu.", "Tamam");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Hata", "Günlük limit güncellenirken bir hata oluþtu: " + ex.Message, "Tamam");
            }
        }
        else
        {
            await DisplayAlert("Hata", "Geçersiz bir sayý girdiniz.", "Tamam");
        }
    }

}