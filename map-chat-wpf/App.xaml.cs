using System.Configuration;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace map_chat_wpf
{
    /// <summary>
    /// Displays a map
    /// </summary>
    public partial class App : Application
    {
        private IConfiguration Configuration;

        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<Startup>();
            Configuration = builder.Build();

            var apiKey = Configuration["ARCGIS_API_Key"];
            Esri.ArcGISRuntime.ArcGISRuntimeEnvironment.ApiKey = apiKey;
        }
    }
}
