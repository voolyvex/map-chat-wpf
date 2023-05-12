using Microsoft.Extensions.Configuration;
using System.Windows;

namespace map_chat_wpf
{

    public partial class App : Application
    {
        private IConfiguration Configuration;

        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ConfigurationBuilder()
                .AddUserSecrets();
            Configuration = builder.Build();

            var apiKey = Configuration["ARCGIS_API_Key"];
            Esri.ArcGISRuntime.ArcGISRuntimeEnvironment.ApiKey = apiKey;
        }
    }
}
