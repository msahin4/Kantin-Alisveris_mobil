using System.Security.Cryptography;
using System.Text;
using MauiApp10.Services;

namespace MauiApp10
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        private string MD5_Hesapla(string sifre)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(sifre);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Hash'i hexadecimal formata dönüþtür
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            string username = usernameEntry.Text;
            string password = passwordEntry.Text;

            string passwordHASH = MD5_Hesapla(password);


            if (int.TryParse(username, out int ogrenciNo) && await ValidateUser(ogrenciNo, passwordHASH))
            {
                // Öðrenci numarasýný UserService'e kaydet
                App.UserService.OgrenciNo = ogrenciNo;

                await Shell.Current.GoToAsync("//MainPage");
            }
            else
            {
                await DisplayAlert("Hata", "Kullanýcý adý veya parola hatalý.", "Tamam");
            }
        }

        private void OnForgotPasswordButtonClicked(object sender, EventArgs e)
        {
            // Þifreyi unuttum iþlemleri
        }

        private async Task<bool> ValidateUser(int ogrenciNo, string password)
        {
            return await App.DatabaseService.ValidateUserAsync(ogrenciNo, password);
        }
    }
}
