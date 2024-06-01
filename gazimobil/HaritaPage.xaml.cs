namespace gazimobil
{
    public partial class HaritaPage : ContentPage
    {
        public HaritaPage()
        {
            InitializeComponent();
        }

        private async void HaritaButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WebViewPage("https://www.google.com/maps/place/Gazi+Üniversitesi/@39.9396355,32.8032515,15z/data=!4m10!1m2!2m1!1sGazi+Üniversitesi!3m6!1s0x14d34eda2bd572e3:0xc4944b9ae7b9927!8m2!3d39.9396355!4d32.8223059!15sChJHYXppIMOcbml2ZXJzaXRlc2kiA4gBAZIBEXB1YmxpY191bml2ZXJzaXR54AEA!16zL20vMDdoNjM4?hl=tr&entry=ttu"));
        }
    }
}
