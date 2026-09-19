namespace AccidentesDeMadrid.Configuration;

using Microsoft.Extensions.Configuration;

public class AppConfig
{

    static AppConfig()
    {
        // Indicamos el archivo para la configuración
        Config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile(
                "appsettings.json",
                optional: false,
                reloadOnChange: true
            )
            .Build();
    }
    /// <summary>
    /// Configuración para .net equivalente a un @configuration
    /// </summary>
    public static IConfiguration Config { get; }
    
    
}