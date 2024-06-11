using Microsoft.Maui.Controls;

namespace gazimobil
{
    public partial class AkademikTakvimPage : ContentPage
    {
        public AkademikTakvimPage()
        {
            InitializeComponent();
            LoadPdf();
        }

        private void LoadPdf()
        {
            string pdfUrl = "https://webupload.gazi.edu.tr/upload/12/2023/8/31/0d6e6222-3ba9-495b-9ef7-1303fb562b6c-2023-2024-akademik-takvim-onlisans-ve-lisans-.pdf";
            string pdfViewerUrl = $"https://docs.google.com/gview?embedded=true&url={pdfUrl}";

            pdfWebView.Source = new UrlWebViewSource
            {
                Url = pdfViewerUrl
            };
        }
    }
}
