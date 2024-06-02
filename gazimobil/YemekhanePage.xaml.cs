using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace gazimobil.Views
{
    public partial class YemekhanePage : ContentPage
    {
        private readonly string dosyaYolu;
        private DateTime currentDate;

        public YemekhanePage()
        {
            InitializeComponent();
            dosyaYolu = Path.Combine(AppContext.BaseDirectory, "Resources", "YemekhaneMenusu.txt");
            currentDate = DateTime.Now;
            LoadMenu();
        }

        private async void LoadMenu()
        {
            try
            {
                var menu = await BugununMenusuAsync(currentDate);
                TarihLabel.Text = currentDate.ToString("dd.MM.yyyy dddd", new CultureInfo("tr-TR"));
                if (!string.IsNullOrEmpty(menu))
                {
                    BugununMenusuLabel.Text = menu;
                }
                else
                {
                    BugununMenusuLabel.Text = "Bugün için menü bulunamadý.";
                }
            }
            catch (Exception ex)
            {
                BugununMenusuLabel.Text = $"Menü yüklenirken hata oluþtu: {ex.Message}";
            }
        }

        private void OnOncekiButtonClicked(object sender, EventArgs e)
        {
            currentDate = currentDate.AddDays(-1);
            LoadMenu();
        }

        private void OnSonrakiButtonClicked(object sender, EventArgs e)
        {
            currentDate = currentDate.AddDays(1);
            LoadMenu();
        }

        public async Task<string> BugununMenusuAsync(DateTime date)
        {
            string bugun = date.ToString("dd.MM.yyyy dddd", new CultureInfo("tr-TR"));

            if (!File.Exists(dosyaYolu))
            {
                throw new FileNotFoundException($"Dosya bulunamadý: {dosyaYolu}");
            }

            string[] satirlar = await File.ReadAllLinesAsync(dosyaYolu);
            bool tarihBulundu = false;

            foreach (var satir in satirlar)
            {
                if (satir.Trim().StartsWith(bugun))
                {
                    tarihBulundu = true;
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
            return "Bugün için menü bulunamadý.";
        }
    }
}
