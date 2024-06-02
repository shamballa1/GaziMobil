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

        public YemekhanePage()
        {
            InitializeComponent();
            dosyaYolu = Path.Combine(AppContext.BaseDirectory, "Resources", "YemekhaneMenusu.txt");
            LoadMenu();
        }

        private async void LoadMenu()
        {
            try
            {
                var menu = await BugununMenusuAsync();
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

        public async Task<string> BugununMenusuAsync()
        {
            string bugun = DateTime.Now.ToString("dd.MM.yyyy dddd", new CultureInfo("tr-TR"));

            if (!File.Exists(dosyaYolu))
            {
                throw new FileNotFoundException($"Dosya bulunamadý: {dosyaYolu}");
            }

            string[] satirlar = await File.ReadAllLinesAsync(dosyaYolu);
            for (int i = 0; i < satirlar.Length; i++)
            {
                if (satirlar[i].Trim().StartsWith(bugun))
                {
                    return string.Join("\n", satirlar, i + 1, 6);
                }
            }

            return "Bugün için menü bulunamadý.";
        }
    }
}
