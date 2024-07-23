using Dapper;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MauiApp10;

public partial class FiyatListesi : ContentPage
{
    private readonly DatabaseService _databaseService;

    public FiyatListesi()
    {
        InitializeComponent();
        _databaseService = new DatabaseService();
        LoadData();
    }

    private async void LoadData()
    {
        var urunler = await GetUrunlerAsync();
        UrunlerCollectionView.ItemsSource = urunler;
    }

    private async Task<IEnumerable<Urun>> GetUrunlerAsync()
    {
        string query = "SELECT urunAdi, marka, fiyat FROM tbl_urunler";
        return await _databaseService.SqlConnection.QueryAsync<Urun>(query);
    }
}

public class Urun
{
    public string urunAdi { get; set; }
    public string marka { get; set; }
    public decimal fiyat { get; set; }
}
