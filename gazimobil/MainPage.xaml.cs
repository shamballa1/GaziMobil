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

        private async void HaritaButtonClicked(object sender, EventArgs e)
        {
            await AnimateAndNavigate("https://www.google.com/maps/place/Gazi+Üniversitesi/@39.9396355,32.8032515,15z/data=!4m10!1m2!2m1!1sGazi+Üniversitesi!3m6!1s0x14d34eda2bd572e3:0xc4944b9ae7b9927!8m2!3d39.9396355!4d32.8223059!15sChJHYXppIMOcbml2ZXJzaXRlc2kiA4gBAZIBEXB1YmxpY191bml2ZXJzaXR54AEA!16zL20vMDdoNjM4?hl=tr&entry=ttu");
        }

        private async void KutuphaneButtonClicked(object sender, EventArgs e)
        {
            await AnimateAndNavigate("https://kutuphane.gazi.edu.tr");
        }

        private async void DuyurularButtonClicked(object sender, EventArgs e)
        {
            await AnimateAndNavigate("https://gazi.edu.tr/view/announcement-list");
        }

        private async void YemekhaneButtonClicked(object sender, EventArgs e)
        {
            await AnimateAndNavigate("https://mediko.gazi.edu.tr/view/page/20412");
        }

        private async void Akademik_TakvimButtonClicked(object sender, EventArgs e)
        {
            string pdfUrl = "https://example.com/akademik_takvim.pdf";
            await Navigation.PushAsync(new WebViewPage(pdfUrl)); ;
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
