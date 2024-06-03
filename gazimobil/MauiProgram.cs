using Microsoft.Extensions.Logging;

namespace gazimobil
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Qadisah.ttf");
                    fonts.AddFont("Roboto-Italic.ttf");
                    fonts.AddFont("Roboto-Regular.ttf");
                    fonts.AddFont("Satisfy-Regular.ttf","SatisfyRegular");
                    fonts.AddFont("Sofia Pro Black Az.otf", "SofiaPro");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
