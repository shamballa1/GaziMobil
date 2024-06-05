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
            await Navigation.PushAsync(new WebViewPage("https://kutuphane.gazi.edu.tr"));
        }
    }
}
