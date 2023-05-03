using System.Windows;

namespace map_chat_wpf
{
    /// <summary>
    /// Displays a map
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            // Note: it is not best practice to store API keys in source code.
            // The API key is referenced here for the convenience of this tutorial.
            Esri.ArcGISRuntime.ArcGISRuntimeEnvironment.ApiKey = "AAPKcfcd234a65af4d4fb07dacb35b8d66d2isBkq1F7fE1_sSzm-TN6GIy2f-WO0fFAYqGdB-YSIfynrIPGAXA_JOFnJSfTBCwY";
        }

    }
}
