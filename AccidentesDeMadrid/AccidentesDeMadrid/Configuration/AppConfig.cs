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
        
        ///
        ///
        ///
        var csvFiles = AppConfig.Config
            .GetSection("CSV:Files")
            .GetChildren()
            .ToDictionary(x => x.Key, x => x.Value)!;
        var directory = AppConfig.Config["CSV:Directory"]!;

        CsvsPaths = csvFiles.ToDictionary(
            x => x.Key,
            x => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, directory, x.Value));
        


    }
    ///
    ///
    ///
    public static Dictionary<string, string> CsvsPaths = new();
    
    /// <summary>
    /// Configuración para .net equivalente a un @configuration
    /// </summary>
    public static IConfiguration Config { get; }

    



}