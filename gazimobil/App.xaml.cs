using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using System.Threading.Tasks;

namespace gazimobil
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AcilisEkrani();
        }

        protected override async void OnStart()
        {
            await Task.Delay(4000);

            MainPage = new NavigationPage(new MainPage());
        }
    }
}