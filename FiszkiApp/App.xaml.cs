using Microsoft.Extensions.Configuration;
using System.IO;


namespace FiszkiApp
{
    public partial class App : Application
    {
        public static IConfiguration Configuration { get; private set; }
        public App()
        {
            InitializeComponent();

            var builder = new ConfigurationBuilder()
            .SetBasePath(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData))
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();


            MainPage = new AppShell();
        }
    }
}
