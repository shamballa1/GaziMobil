namespace gazimobil
{
    public partial class AkademikTakvimPage : ContentPage
    {
        public AkademikTakvimPage()
        {
            InitializeComponent();
        }

        private async void AkademikTakvimButtonClicked(object sender, EventArgs e)
        {
            string pdfUrl = "https://example.com/akademik_takvim.pdf";
            await Navigation.PushAsync(new WebViewPage(pdfUrl));
        }
    }
}
