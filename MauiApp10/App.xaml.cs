using MauiApp10.Services;

namespace MauiApp10
{
    public class UserService
    {
        public int OgrenciNo { get; set; }

    }
    public partial class App : Application
    {
        public static DatabaseService DatabaseService { get; private set; }

        public static UserService UserService { get; private set; }



        public App()
        {


            InitializeComponent();
            DatabaseService = new DatabaseService();
            UserService = new UserService();    

            MainPage = new AppShell();

            // İlk olarak giriş sayfasını göster
            Shell.Current.GoToAsync("//LoginPage");



        }
    }
}
