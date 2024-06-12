using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.Maui.Controls;
using System.Web;
using System.Windows.Input;

namespace gazimobil
{
    public class DuyuruModel
    {
        public string Title { get; set; }
        public string Date { get; set; }
        public string Url { get; set; }
    }

    public partial class DuyurularPage : ContentPage
    {
        private int ÞuankiSayfa = 1;

        public ICommand OpenWebViewCommand { get; private set; }

        public DuyurularPage()
        {
            InitializeComponent();
            BindingContext = this; // BindingContext'i ayarladýk
            OpenWebViewCommand = new Command<string>(async (url) => await OpenWebView(url));
            DuyuruYükle();
            SayfaEtiketleriniGüncelle();
        }

        private async void DuyuruYükle(string searchString = "", int page = 1)
        {
            var url = $"https://gazi.edu.tr/view/announcement-list?id={page}&type=1&SearchString={searchString}&dates=&date=";
            var duyurular = await DuyurularýGetir(url);
            if (duyurular.Count > 0)
            {
                DuyurularCollectionView.ItemsSource = duyurular;
            }
            else
            {
                await DisplayAlert("Bilgi", "Gösterilecek duyuru bulunamadý.", "Tamam");
            }
        }

        private void AramaButonu(object sender, EventArgs e)
        {
            var searchString = SearchEntry.Text;
            ÞuankiSayfa = 1; // Arama yapýldýðýnda sayfa numarasýný sýfýrlýyoruz
            DuyuruYükle(searchString, ÞuankiSayfa);
            SayfaEtiketleriniGüncelle();
        }

        private void AramayýTemizleButonu(object sender, EventArgs e)
        {
            SearchEntry.Text = string.Empty;
            ÞuankiSayfa = 1; // Arama sýfýrlandýðýnda sayfa numarasýný sýfýrlýyoruz
            DuyuruYükle(page: ÞuankiSayfa);
            SayfaEtiketleriniGüncelle();
        }

        private void OncekiButonuTýklandýðýnda(object sender, EventArgs e)
        {
            if (ÞuankiSayfa > 1)
            {
                ÞuankiSayfa--;
                DuyuruYükle(SearchEntry.Text, ÞuankiSayfa);
                SayfaEtiketleriniGüncelle();
            }
        }

        private void SonrakiButonuTýklandýðýnda(object sender, EventArgs e)
        {
            ÞuankiSayfa++;
            DuyuruYükle(SearchEntry.Text, ÞuankiSayfa);
            SayfaEtiketleriniGüncelle();
        }

        private void SayfaEtiketleriniGüncelle()
        {
            PageLabel1.Text = (ÞuankiSayfa - 1).ToString();
            PageLabel2.Text = ÞuankiSayfa.ToString();
            PageLabel3.Text = (ÞuankiSayfa + 1).ToString();

            PageLabel1.IsVisible = ÞuankiSayfa > 1;
            PageLabel1.TextColor = Colors.Blue;
            PageLabel2.TextColor = Colors.Red;
            PageLabel3.TextColor = Colors.Blue;
        }

        private async Task OpenWebView(string url)
        {
            await Navigation.PushAsync(new WebViewPage(url));
        }

        private async Task<List<DuyuruModel>> DuyurularýGetir(string url)
        {
            List<DuyuruModel> duyurular = new List<DuyuruModel>();

            try
            {
                HttpClient httpClient = new HttpClient();
                var response = await httpClient.GetByteArrayAsync(url);
                var html = Encoding.UTF8.GetString(response);

                HtmlDocument document = new HtmlDocument();
                document.LoadHtml(html);

                var duyurularMainNode = document.DocumentNode.SelectSingleNode("//*[@id='subpage-maindiv']/div/div/div[1]");

                if (duyurularMainNode != null)
                {
                    foreach (var node in duyurularMainNode.SelectNodes(".//div[contains(@class, 'subpage-ann-single')]"))
                    {
                        var dateNode = node.SelectSingleNode(".//div[contains(@class, 'subpage-ann-date')]");
                        var titleNode = node.SelectSingleNode(".//div[contains(@class, 'subpage-ann-link')]/a");

                        if (titleNode != null && dateNode != null)
                        {
                            var title = HttpUtility.HtmlDecode(titleNode.InnerText.Trim());
                            var month = HttpUtility.HtmlDecode(dateNode.SelectSingleNode(".//span[contains(@class, 'ann-month')]").InnerText.Trim());
                            var day = HttpUtility.HtmlDecode(dateNode.SelectSingleNode(".//span[contains(@class, 'ann-day')]").InnerText.Trim());
                            var year = HttpUtility.HtmlDecode(dateNode.SelectSingleNode(".//span[contains(@class, 'ann-year')]").InnerText.Trim());
                            var date = $"{day} {month} {year}";
                            var link = "https://gazi.edu.tr" + titleNode.GetAttributeValue("href", string.Empty);
                            duyurular.Add(new DuyuruModel { Title = title, Date = date, Url = link });
                        }
                        else if (titleNode != null)
                        {
                            var title = HttpUtility.HtmlDecode(titleNode.InnerText.Trim());
                            var link = "https://gazi.edu.tr" + titleNode.GetAttributeValue("href", string.Empty);
                            duyurular.Add(new DuyuruModel { Title = title, Date = "Tarih Yok", Url = link });
                        }
                    }
                }
                else
                {
                    await DisplayAlert("Hata", "Duyurular bölümü bulunamadý.", "Tamam");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Hata", $"Duyurular alýnýrken bir hata oluþtu: {ex.Message}", "Tamam");
            }

            return duyurular;
        }
    }
}
