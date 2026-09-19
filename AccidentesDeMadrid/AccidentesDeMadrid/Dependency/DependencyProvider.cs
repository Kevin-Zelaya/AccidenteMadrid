using AccidentesDeMadrid.Configuration;
using AccidentesDeMadrid.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

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
            .CreateLogger();
        Log.Logger.Debug("PRUEBA DIRECTA DE SERILOG");
        services.AddLogging(logging =>
        {
            logging.AddSerilog(Log.Logger);
        });
        
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