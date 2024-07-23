using Microsoft.Data.SqlClient;
using Dapper;
using System.Threading.Tasks;

namespace MauiApp10
{
    public class DatabaseService
    {
        private const string ConnectionString = "Server=kantin.database.windows.net;Database=Kantin;User Id=kantinAdmin;Password=kantin123.;";
        public SqlConnection SqlConnection { get; private set; }

        public DatabaseService()
        {
            InitializeDatabaseConnection();
        }

        private void InitializeDatabaseConnection()
        {
            SqlConnection = new SqlConnection(ConnectionString);
            SqlConnection.Open();
        }

        public async Task<bool> ValidateUserAsync(int ogrenciNo, string password)
        {
            string query = "SELECT COUNT(1) FROM tbl_ogrenci WHERE ogrenciNo = @OgrenciNo AND parola = @Password";
            var result = await SqlConnection.QueryAsync<int>(query, new { OgrenciNo = ogrenciNo, Password = password });
            return result.FirstOrDefault() == 1;
        }
        public async Task<int> GetBalanceAsync(int ogrenciNo)
        {
            string query = "SELECT bakiye FROM tbl_kart WHERE ogrenciNo = @OgrenciNo";
            var result = await SqlConnection.QueryAsync<int>(query, new { OgrenciNo = ogrenciNo });
            return result.FirstOrDefault();
        }

        public async Task<bool> UpdateDailyLimitAsync(int ogrenciNo, int yeniLimit)
        {
            string query = "UPDATE tbl_kart SET gunlukLimit = @YeniLimit WHERE ogrenciNo = @OgrenciNo";

            try
            {
                var affectedRows = await SqlConnection.ExecuteAsync(query, new { YeniLimit = yeniLimit, OgrenciNo = ogrenciNo });

                return affectedRows > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Günlük limit güncellenirken bir hata oluştu: " + ex.Message);
                return false;
            }
        }

    }
}
