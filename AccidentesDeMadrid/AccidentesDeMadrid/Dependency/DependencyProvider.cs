using AccidentesDeMadrid.Configuration;
using AccidentesDeMadrid.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace AccidentesDeMadrid.Dependency;

public class DependencyProvider
{
    public static ServiceProvider Configure()
    {
        ///
        ///  Colección para la inyección de dependencias
        ///  Registra interfaces y las clases que las impelemtnan
        var services = new ServiceCollection();
        /// Ciclo de vida
        /// 1. Transient: nueva instancia por cada solicitud
        ///
        /// 2. Scoped: Instancia por petición Http
        ///
        /// 3. Singleton: Una instancia durante todo el ciclo de vida de la aplicación
        
        // Configuramos Serilog utilizando appsettings.json.
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(AppConfig.Config)
            .WriteTo.Console(
                theme: LogTheme.Theme,
                outputTemplate:
                "{Timestamp:HH:mm:ss} [{Level:u3}] [{SourceContext}] {Message:lj}{NewLine}{Exception}"
            )
            .CreateLogger(); // ← aquí
        Log.Logger.Debug("PRUEBA DIRECTA DE SERILOG");
        services.AddLogging(logging =>
        {
            logging.AddSerilog(Log.Logger);
        });
        /// Rutas de los csv para uso en el repositorio
        services.AddSingleton(AppConfig.CsvsPaths);
        
        /// Las clases que implementen la interfaz seran escaneadas
        /// seran inyectadas con el respectivo ciclo de vida
        /// reconociendo donde se deben implementar
        services.Scan(scan => scan
            .FromAssemblyOf<Program>()
                .AddClasses(classes => classes.AssignableTo<ITransientService>())
                    .AsImplementedInterfaces()
                    .WithTransientLifetime()
                .AddClasses(classes => classes.AssignableTo<IScopedService>())
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
                .AddClasses(classes => classes.AssignableTo<ISingletonService>())
                    .AsImplementedInterfaces()
                    .WithSingletonLifetime()
        );
        
        return services.BuildServiceProvider();
    }
}