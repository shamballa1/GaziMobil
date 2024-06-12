using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;

namespace gazimobil
{
    public partial class NotPage : ContentPage
    {
        private List<Dersler> dersler = new List<Dersler>();

        public NotPage()
        {
            InitializeComponent();
        }

        private void HesaplamaTuruDegisti(object sender, EventArgs e)
        {
            bool donemlik = HesaplamaTuruPicker.SelectedIndex == 0;
            bool genel = HesaplamaTuruPicker.SelectedIndex == 1;

            DonemGirdileri.IsVisible = donemlik || genel;
            GenelGirdileri.IsVisible = genel;
        }

        private async void DersEkleClicked(object sender, EventArgs e)
        {
            string dersAdi = DersAdiEntry.Text;
            if (string.IsNullOrWhiteSpace(dersAdi))
            {
                await DisplayAlert("Hata", "Lütfen ders adını girin.", "Tamam");
                return;
            }

            if (KrediPicker.SelectedIndex == -1)
            {
                await DisplayAlert("Hata", "Lütfen Kredi / AKTS seçin.", "Tamam");
                return;
            }

            if (NotPicker.SelectedIndex == -1)
            {
                await DisplayAlert("Hata", "Lütfen harf notu seçin.", "Tamam");
                return;
            }

            double kredi = double.Parse(KrediPicker.SelectedItem.ToString());
            string harfNotu = NotPicker.SelectedItem.ToString();
            double not = HarfNotunuAl(harfNotu);

            Dersler ders = new Dersler { Adi = dersAdi, Kredi = kredi, Not = not, HarfNotu = harfNotu };
            dersler.Add(ders);
            DerslerListView.ItemsSource = null;
            DerslerListView.ItemsSource = dersler;

            OrtalamalariGuncelle();

            
            DersAdiEntry.Text = string.Empty;
            KrediPicker.SelectedIndex = -1;
            NotPicker.SelectedIndex = -1;
        }

        private double HarfNotunuAl(string harfNotu)
        {
            return harfNotu switch
            {
                "AA" => 4.0,
                "BA" => 3.5,
                "BB" => 3.0,
                "CB" => 2.5,
                "CC" => 2.0,
                "DC" => 1.5,
                "DD" => 1.0,
                "FD" => 0.5,
                "FF" => 0.0,
                _ => throw new ArgumentException("Geçersiz harf notu!"),
            };
        }

        private void OrtalamalariGuncelle()
        {
            double toplamPuan = 0;
            double toplamKredi = 0;

            foreach (var ders in dersler)
            {
                toplamPuan += ders.Kredi * ders.Not;
                toplamKredi += ders.Kredi;
            }

            double donemOrtalamasi = toplamPuan / toplamKredi;
            DonemOrtalamasiLabel.Text = donemOrtalamasi.ToString("F2");
        }

        private async void GenelNotOrtalamasiHesaplaClicked(object sender, EventArgs e)
        {
            if (!double.TryParse(GenelKrediEntry.Text, out double mevcutKredi) || mevcutKredi <= 0)
            {
                await DisplayAlert("Hata", "Lütfen geçerli bir mevcut kredi girin.", "Tamam");
                return;
            }

            if (!double.TryParse(GenelNotOrtalamasiEntry.Text, out double mevcutNotOrtalamasi) || mevcutNotOrtalamasi < 0 || mevcutNotOrtalamasi > 4)
            {
                await DisplayAlert("Hata", "Lütfen geçerli bir mevcut ortalama girin.", "Tamam");
                return;
            }

            double toplamPuan = mevcutNotOrtalamasi * mevcutKredi;
            double toplamKredi = mevcutKredi;

            foreach (var ders in dersler)
            {
                toplamPuan += ders.Kredi * ders.Not;
                toplamKredi += ders.Kredi;
            }

            double genelNotOrtalamasi = toplamPuan / toplamKredi;
            GenelGenelNotOrtalamasiLabel.Text = genelNotOrtalamasi.ToString("F2");

            OrtalamalariGuncelle();
        }

        private void DersSil(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var ders = menuItem?.BindingContext as Dersler;
            if (ders != null)
            {
                dersler.Remove(ders);
                DerslerListView.ItemsSource = null;
                DerslerListView.ItemsSource = dersler;
                OrtalamalariGuncelle();
            }
        }
    }

    public class Dersler
    {
        public string Adi { get; set; }
        public double Kredi { get; set; }
        public double Not { get; set; }
        public string HarfNotu { get; set; }

        public override string ToString()
        {
            return $"{Adi}  Kredi/AKTS: {Kredi/10} Harf Notu: {HarfNotu}";
        }
    }
}
