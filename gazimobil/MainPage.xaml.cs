using HtmlAgilityPack;
using Microsoft.Maui.Controls;
using System;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Windows.Input;
using System.Text;
using System.Collections.ObjectModel;
using System.Xml.Linq;


namespace gazimobil
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<DuyuruModel> DuyurularListesi { get; set; }
        public ICommand OpenWebViewCommand { get; private set; }
        private DateTime currentDate;
        private int currentPage = 1;

        public MainPage()
        {
            InitializeComponent();
            DuyurularListesi = new ObservableCollection<DuyuruModel>();
            OpenWebViewCommand = new Command<string>(async (url) => await OpenWebView(url));
            this.BindingContext = this;
            currentDate = DateTime.Now;
            LoadDuyurular();
            MenuYukle(currentDate);
            LoadWeatherData();
        }
        //Hava Durumu
        private async void LoadWeatherData()
        {
            try
            {
                string city = "Ankara";
                string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={city}&mode=xml&lang=tr&units=metric&appid=d8cb0ab350365f7b1f63c4f63a2b9373";

                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string xmlData = await response.Content.ReadAsStringAsync();
                    XDocument xmlDoc = XDocument.Parse(xmlData);

                    string temperature = xmlDoc.Root.Element("temperature").Attribute("value").Value;
                    string weatherDescription = xmlDoc.Root.Element("weather").Attribute("value").Value;
                    string icon = xmlDoc.Root.Element("weather").Attribute("icon").Value;
                    string iconUrl = $"https://openweathermap.org/img/wn/{icon}@2x.png";

                    WeatherLabel.Text = $"{weatherDescription}";
                    WeatherLabel2.Text = $"{temperature}°C";
                    WeatherIcon.Source = ImageSource.FromUri(new Uri(iconUrl));
                }
                else
                {
                    WeatherLabel.Text = "Hava durumu verileri alınamadı.";
                }
            }
            catch (Exception ex)
            {
                WeatherLabel.Text = $"Hata: {ex.Message}";
            }
        }
        
        //Ders Programı
        private async void DersProgramiButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DersprogramiPage());
        }


        //Duyurular
        private async Task OpenWebView(string url)
        {
            await Navigation.PushAsync(new WebViewPage(url));
        }

        private async void LoadDuyurular()
        {
            try
            {
                var url = $"https://gazi.edu.tr/view/announcement-list?id={currentPage}&type=1&SearchString=&dates=&date=";
                var duyurular = await GetDuyurularAsync(url);
                if (duyurular.Count > 0)
                {
                    DuyurularListesi.Clear();
                    foreach (var duyuru in duyurular)
                    {
                        DuyurularListesi.Add(duyuru);
                    }
                }
                else
                {
                    await DisplayAlert("Bilgi", "Gösterilecek duyuru bulunamadı.", "Tamam");
                }
                UpdatePageLabels();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Hata", $"Duyurular yüklenirken bir hata oluştu: {ex.Message}", "Tamam");
                System.Diagnostics.Debug.WriteLine($"Duyurular yüklenirken bir hata oluştu: {ex.Message}");
            }
        }

        private async Task<List<DuyuruModel>> GetDuyurularAsync(string url)
        {
            List<DuyuruModel> duyurular = new();

            try
            {
                HttpClient httpClient = new();
                var response = await httpClient.GetByteArrayAsync(url);
                var html = Encoding.UTF8.GetString(response);

                HtmlDocument document = new();
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
                    await DisplayAlert("Hata", "Duyurular bölümü bulunamadı.", "Tamam");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Hata", $"Duyurular alınırken bir hata oluştu: {ex.Message}", "Tamam");
                System.Diagnostics.Debug.WriteLine($"Duyurular alınırken bir hata oluştu: {ex.Message}");
            }

            return duyurular;
        }

        private void UpdatePageLabels()
        {
            PageLabel1.Text = (currentPage - 1).ToString();
            PageLabel2.Text = currentPage.ToString();
            PageLabel3.Text = (currentPage + 1).ToString();

            PageLabel1.IsVisible = currentPage > 1;
            PageLabel1.TextColor = Color.FromArgb("#1B3E75");
            PageLabel2.TextColor = Colors.Red;
            PageLabel3.TextColor = Color.FromArgb("#1B3E75");
        }

        //YEMEK LİSTESİ 
        private async void MenuYukle(DateTime date)
        {
            try
            {
                var menu = await BugununMenusu(date);
                BugununMenusuLabel.Text = menu;
                YemekhaneTarihLabel.Text = $"Günün Menüsü\n{date.ToString("dd.MM.yyyy dddd", new CultureInfo("tr-TR"))}";
            }
            catch (Exception ex)
            {
                BugununMenusuLabel.Text = $"Menü yüklenirken hata oluştu: {ex.Message}";
                System.Diagnostics.Debug.WriteLine($"Menü yüklenirken hata oluştu: {ex.Message}");
            }
        }

        public async Task<string> BugununMenusu(DateTime date)
        {
            string url = "https://drive.google.com/uc?export=download&id=1w4YCzs8drQPXyS1PzH11gPYTfrvbLv1H";
            string dosyaYolu = Path.Combine(AppContext.BaseDirectory, "Resources", "YemekhaneMenusu.txt");

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var fileContent = await response.Content.ReadAsByteArrayAsync();
                    Directory.CreateDirectory(Path.GetDirectoryName(dosyaYolu));
                    await File.WriteAllBytesAsync(dosyaYolu, fileContent);
                }
                else
                {
                    throw new Exception("Dosya indirilemedi.");
                }
            }

            string bugun = date.ToString("dd.MM.yyyy dddd", new CultureInfo("tr-TR"));

            if (!File.Exists(dosyaYolu))
            {
                throw new FileNotFoundException($"Dosya bulunamadı: {dosyaYolu}");
            }

            string[] satirlar = await File.ReadAllLinesAsync(dosyaYolu);

            foreach (var satir in satirlar)
            {
                if (satir.Trim().StartsWith(bugun))
                {
                    int index = Array.IndexOf(satirlar, satir);

                    if (index + 6 <= satirlar.Length)
                    {
                        return string.Join("\n", satirlar, index + 1, 6).Trim();
                    }
                    else
                    {
                        return "Menü bilgileri eksik.";
                    }
                }
            }
            return "Bugün için menü bulunamadı.";
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

        private void OnSwipeRight(object sender, EventArgs e)
        {
            currentDate = currentDate.AddDays(-1);
            MenuYukle(currentDate);
        }

        private void OnSwipeLeft(object sender, EventArgs e)
        {
            currentDate = currentDate.AddDays(1);
            MenuYukle(currentDate);
        }

        private void OnPreviousPageButtonClicked(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadDuyurular();
                UpdatePageLabels();
            }
        }

        private void OnNextPageButtonClicked(object sender, EventArgs e)
        {
            currentPage++;
            LoadDuyurular();
            UpdatePageLabels();
        }
    }
}
