namespace gazimobil
{
    public partial class KutuphanePage : ContentPage
    {
        public KutuphanePage()
        {
            InitializeComponent();
        }

        private async void KutuphaneButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.DisplayAlert("Fiziki Kütüphane Çalýþma Saatleri", "Hafta Ýçi | Cumartesi : 08:30 - 22:00\r\nZemin Kat Okuma Salonu : 7/24\r\nSýnav Dönemlerinde : 7/24", "Kapat");
            await Navigation.PushAsync(new WebViewPage("https://kutuphane.gazi.edu.tr"));
        }
    }
}
