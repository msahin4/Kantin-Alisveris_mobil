using Dapper;
using System;
using System.Collections.ObjectModel;


namespace MauiApp10
{
    public partial class History : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private ObservableCollection<Alisveris> _alisverisler;

        public History()
        {
            InitializeComponent();
            int ogrenciNo = App.UserService.OgrenciNo;
            _databaseService = new DatabaseService();
            _alisverisler = new ObservableCollection<Alisveris>();
            BindingContext = _alisverisler;
            LoadUrunlerAsync(ogrenciNo);
        }

        private async void LoadUrunlerAsync(int ogrenciNo)
        {
            string query = "SELECT u.urunAdi AS UrunAdi, u.fiyat AS Fiyat, s.tarih AS SatisTarihi " +
                           "FROM tbl_kart k " +
                           "JOIN tbl_satis s ON k.kartID = s.kartID " +
                           "JOIN tbl_satisDetaylari sd ON s.satisID = sd.satisID " +
                           "JOIN tbl_urunler u ON sd.urunID = u.urunID " +
                           "WHERE k.ogrenciNo = @ogrenciNo";

            var urunler = await _databaseService.SqlConnection.QueryAsync<Alisveris>(query, new { ogrenciNo });

            foreach (var urun in urunler)
            {
                _alisverisler.Add(urun);
            }
        }

        public class Alisveris
        {
            public string UrunAdi { get; set; }
            public decimal Fiyat { get; set; }
            public DateTime SatisTarihi { get; set; }
        }
    }
}
