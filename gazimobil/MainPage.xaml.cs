using HtmlAgilityPack;
using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;


namespace gazimobil
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            

        }

        private async Task AnimateAndNavigate(string url)
        {
            await Navigation.PushAsync(new WebViewPage(url));
        }
        
        private async void FacebookButtonClicked(object sender, EventArgs e)
        {
            await AnimateAndNavigate("https://www.facebook.com/GaziUniversitesi.1926/?locale=tr_TR");
        }

        private async void TwitterButtonClicked(object sender, EventArgs e)
        {
            await AnimateAndNavigate("https://twitter.com/Gazi_Universite?ref_src=twsrc%5Egoogle%7Ctwcamp%5Eserp%7Ctwgr%5Eauthor");
        }

        private async void YoutubeButtonClicked(object sender, EventArgs e)
        {
            await AnimateAndNavigate("https://www.youtube.com/channel/UCeqmviAP_SXENDj3fzW1gJA");
        }

        private async void InstagramButtonClicked(object sender, EventArgs e)
        {
            await AnimateAndNavigate("https://www.instagram.com/gazi_universitesi/");
        }

        private async void LinkedinButtonClicked(object sender, EventArgs e)
        {
            await AnimateAndNavigate("https://www.linkedin.com/school/gazi-university/?originalSubdomain=tr");
        }
    }
}
