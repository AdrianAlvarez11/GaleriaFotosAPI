using GaleriaFotosApp.Services;
using GaleriaFotosApp.ViewModels;
using GaleriaFotosMaui.Services;
using GaleriaFotosMaui.ViewModels;
using GaleriaFotosMaui.Views;
using Microsoft.Extensions.Logging;

namespace GaleriaFotosApp
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
                });

            builder.Services.AddSingleton<AuthService>();
            builder.Services.AddSingleton<FotoService>();

            builder.Services.AddSingleton(new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44303/")
            });

            builder.Services.AddSingleton<UsersViewModel>();
            builder.Services.AddSingleton<FotosViewmodel>();

            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<RegistroPage>();
            builder.Services.AddSingleton<MuroPage>();
            builder.Services.AddSingleton<SubirFotoPage>();
            builder.Services.AddSingleton<PerfilPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}