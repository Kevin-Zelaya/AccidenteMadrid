using System.Globalization;
using System.Linq.Expressions;
using AccidentesDeMadrid.Dto;
using AccidentesDeMadrid.Entity;
using AccidentesDeMadrid.Entity.Enum;
using AccidentesDeMadrid.Interfaces;
using AccidentesDeMadrid.Mapper;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Logging;
using ILogger = Serilog.ILogger;

namespace AccidentesDeMadrid.Repository.Load;

public class CsvLoader : ICsvLoader, ITransientService
{
    private readonly ILogger<CsvLoader> _logger;
    
    public CsvLoader(
        ILogger<CsvLoader> logger
        )
    {
        _logger = logger;
    }

    public async Task<IEnumerable<Accident>> LoadCsv(string path)
    {
    
        /// comprobamos si existe el archivo
        if (!Path.Exists(path))
        {
            _logger.LogError("[CSV-LOADER] No se pudo cargar el archivo: {path}]", path);
            throw new FileNotFoundException("No se pudo encontrar el archivo", path);
        }

        try
        {
            var reader = new StreamReader(path);
            // Configurar el delimitador de columnas
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";"
            };
            // Crear el reader con el archivo y la configuración
            var csv = new CsvReader(reader, config);
            // Leer de forma asincrona
            await csv.ReadAsync();
                
            var accidents = new List<Accident>();

            while (await csv.ReadAsync())
            {
                var dto = new AccidentDto(
                    csv.GetField<string>(0),
                    csv.GetField<string>(1),
                    csv.GetField<string>(2),
                    csv.GetField<string>(3),
                    csv.GetField<string>(4),
                    csv.GetField<string>(5),
                    csv.GetField<string>(6),
                    csv.GetField<string>(7),
                    csv.GetField<string>(8),
                    csv.GetField<string>(9),
                    csv.GetField<string>(10),
                    csv.GetField<string>(11),
                    csv.GetField<string>(12),
                    csv.GetField<string>(13),
                    csv.GetField<string>(14),
                    csv.GetField<string>(15),
                    csv.GetField<string>(16),
                    csv.GetField<string>(17),
                    csv.GetField<string>(18)
                );

                accidents.Add(dto.ToEntity());
            }

            return accidents;
        }
        catch (Exception e)
        {
            _logger.LogError("Error al cargar el csv en la ruta: {path}\nCon el siguiente mensaje de error {message}", path, e.Message);
            throw;
        }
        
        
    }


}