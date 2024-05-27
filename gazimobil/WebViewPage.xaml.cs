using Microsoft.Maui.Controls;

namespace gazimobil
{
    public partial class WebViewPage : ContentPage
    {
        public WebViewPage(string url)
        {
            InitializeComponent();
            webView.Source = url;
        }
    }
}