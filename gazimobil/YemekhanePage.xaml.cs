using System;
using Microsoft.Maui.Controls;

namespace gazimobil
{
    public partial class YemekhanePage : ContentPage
    {
        public YemekhanePage()
        {
            InitializeComponent();
            var url = "https://mediko.gazi.edu.tr/view/page/20412";
            yemekhaneWebView.Source = url;
        }
    }
}
