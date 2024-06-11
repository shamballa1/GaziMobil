using Microsoft.Maui.Controls;
using System;

namespace gazimobil
{
    public partial class HaritaPage : ContentPage
    {
        double currentScale = 1;
        double startScale = 1;
        double xOffset = 0;
        double yOffset = 0;

        public HaritaPage()
        {
            InitializeComponent();
            Title = "Harita";
        }

        private void OnPinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        {
            if (e.Status == GestureStatus.Started)
            {
                startScale = mapImage.Scale;
                mapImage.AnchorX = 0;
                mapImage.AnchorY = 0;
            }
            if (e.Status == GestureStatus.Running)
            {
                // Calculate the scale factor to be applied.
                currentScale = startScale * e.Scale;
                currentScale = Math.Max(1, currentScale);

                // The ScaleOrigin is in relative coordinates to the wrapped user interface element.
                double renderedX = mapImage.X + xOffset;
                double deltaX = renderedX / Width;
                double deltaWidth = Width / (mapImage.Width * startScale);
                double originX = (e.ScaleOrigin.X - deltaX) * deltaWidth;

                double renderedY = mapImage.Y + yOffset;
                double deltaY = renderedY / Height;
                double deltaHeight = Height / (mapImage.Height * startScale);
                double originY = (e.ScaleOrigin.Y - deltaY) * deltaHeight;

                // Calculate the transformed element bounds.
                double targetX = xOffset - (originX * mapImage.Width) * (currentScale - startScale);
                double targetY = yOffset - (originY * mapImage.Height) * (currentScale - startScale);

                // Apply translation based on the change in origin.
                mapImage.TranslationX = Math.Min(0, Math.Max(targetX, -mapImage.Width * (currentScale - 1)));
                mapImage.TranslationY = Math.Min(0, Math.Max(targetY, -mapImage.Height * (currentScale - 1)));

                // Apply scale factor.
                mapImage.Scale = currentScale;
            }
            if (e.Status == GestureStatus.Completed)
            {
                // Store the current translation applied during the pinch gesture.
                xOffset = mapImage.TranslationX;
                yOffset = mapImage.TranslationY;
            }
        }

        private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Running:
                    // Translate and ensure we don't pan beyond the wrapped user interface element bounds.
                    double newX = (e.TotalX * currentScale) + xOffset;
                    double newY = (e.TotalY * currentScale) + yOffset;

                    double maxX = mapImage.Width * (currentScale - 1);
                    double maxY = mapImage.Height * (currentScale - 1);

                    mapImage.TranslationX = Math.Min(0, Math.Max(newX, -maxX));
                    mapImage.TranslationY = Math.Min(0, Math.Max(newY, -maxY));
                    break;

                case GestureStatus.Completed:
                    // Store the translation applied during the pan gesture.
                    xOffset = mapImage.TranslationX;
                    yOffset = mapImage.TranslationY;
                    break;
            }
        }

        private async void HaritaButtonClicked(object sender, EventArgs e)
        {
            var url = "https://www.google.com/maps/place/Gazi+Üniversitesi/@39.9396355,32.8032515,15z/data=!4m10!1m2!2m1!1sGazi+Üniversitesi!3m6!1s0x14d34eda2bd572e3:0xc4944b9ae7b9927!8m2!3d39.9396355!4d32.8223059!15sChJHYXppIMOcbml2ZXJzaXRlc2kiA4gBAZIBEXB1YmxpY191bml2ZXJzaXR54AEA!16zL20vMDdoNjM4?hl=tr&entry=ttu";
            await Navigation.PushAsync(new WebViewPage(url));
        }
    }
}
