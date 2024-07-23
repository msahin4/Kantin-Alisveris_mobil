using Dapper;

namespace MauiApp10;

public partial class limitGuncelle : ContentPage
{
    public limitGuncelle()
    {
        InitializeComponent();
        _databaseService = new DatabaseService(); // DatabaseService s�n�f�ndan bir �rnek olu�turuyoruz.
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
                    await DisplayAlert("Ba�ar�l�", "G�nl�k limit ba�ar�yla g�ncellendi.", "Tamam");
                }
                else
                {
                    await DisplayAlert("Hata", "G�nl�k limit g�ncellenirken bir hata olu�tu.", "Tamam");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Hata", "G�nl�k limit g�ncellenirken bir hata olu�tu: " + ex.Message, "Tamam");
            }
        }
        else
        {
            await DisplayAlert("Hata", "Ge�ersiz bir say� girdiniz.", "Tamam");
        }
    }

}