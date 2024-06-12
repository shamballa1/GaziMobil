using Microsoft.Maui.Controls;
using System;

namespace gazimobil
{
    public partial class HaritaPage : ContentPage
    {
        double mevcutOlcek = 1;
        double baslangicOlcek = 1;
        double xKaydirma = 0;
        double yKaydirma = 0;

        public HaritaPage()
        {
            InitializeComponent();
            Title = "Harita";
        }

        private void OnPinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        {
            if (e.Status == GestureStatus.Started)
            {
                baslangicOlcek = haritaResmi.Scale;
                haritaResmi.AnchorX = 0;
                haritaResmi.AnchorY = 0;
            }
            if (e.Status == GestureStatus.Running)
            {
                mevcutOlcek = baslangicOlcek * e.Scale;
                mevcutOlcek = Math.Max(1, mevcutOlcek);

                double renderX = haritaResmi.X + xKaydirma;
                double deltaX = renderX / Width;
                double deltaWidth = Width / (haritaResmi.Width * baslangicOlcek);
                double orijinX = (e.ScaleOrigin.X - deltaX) * deltaWidth;

                double renderY = haritaResmi.Y + yKaydirma;
                double deltaY = renderY / Height;
                double deltaHeight = Height / (haritaResmi.Height * baslangicOlcek);
                double orijinY = (e.ScaleOrigin.Y - deltaY) * deltaHeight;

                double hedefX = xKaydirma - (orijinX * haritaResmi.Width) * (mevcutOlcek - baslangicOlcek);
                double hedefY = yKaydirma - (orijinY * haritaResmi.Height) * (mevcutOlcek - baslangicOlcek);

                haritaResmi.TranslationX = Math.Min(0, Math.Max(hedefX, -haritaResmi.Width * (mevcutOlcek - 1)));
                haritaResmi.TranslationY = Math.Min(0, Math.Max(hedefY, -haritaResmi.Height * (mevcutOlcek - 1)));

                haritaResmi.Scale = mevcutOlcek;
            }
            if (e.Status == GestureStatus.Completed)
            {
                xKaydirma = haritaResmi.TranslationX;
                yKaydirma = haritaResmi.TranslationY;
            }
        }

        private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Running:
             
                    double yeniX = (e.TotalX * mevcutOlcek) + xKaydirma;
                    double yeniY = (e.TotalY * mevcutOlcek) + yKaydirma;

                    double maxX = haritaResmi.Width * (mevcutOlcek - 1);
                    double maxY = haritaResmi.Height * (mevcutOlcek - 1);

                    haritaResmi.TranslationX = Math.Min(0, Math.Max(yeniX, -maxX));
                    haritaResmi.TranslationY = Math.Min(0, Math.Max(yeniY, -maxY));
                    break;

                case GestureStatus.Completed:
                    
                    xKaydirma = haritaResmi.TranslationX;
                    yKaydirma = haritaResmi.TranslationY;
                    break;
            }
        }

        private async void HaritaButtonClicked(object sender, EventArgs e)
        {
            var url = "https://www.google.com/maps/place/Gazi+Üniversitesi/@39.9396355,32.8032515,15z/data=!4m10!1m2!2m1!1sGazi+Üniversitesi!3m6!1s0x14d34eda2bd572e3:0xc4944b9ae7b9927!8m2!3d39.9396355!4d32.8223059!15sChJHYXppIMOcbml2ZXJzaXRlc2kiA4gBAZIBEXB1YmxpY191bml2ZXJzaXR54AEA!16zL20vMDhoNjM4?hl=tr&entry=ttu";
            await Navigation.PushAsync(new WebViewPage(url));
        }
    }
}
