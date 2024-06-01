namespace gazimobil
{
    public partial class WebViewPage : ContentPage
    {
        public WebViewPage()
        {
            InitializeComponent();
        }

        public WebViewPage(string url) : this()
        {
            webView.Source = url;
        }
    }
}
