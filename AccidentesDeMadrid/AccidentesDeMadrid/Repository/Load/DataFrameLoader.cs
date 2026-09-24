using System.Globalization;
using AccidentesDeMadrid.Dto;
using AccidentesDeMadrid.Entity;
using AccidentesDeMadrid.Interfaces;
using CsvHelper;
using CsvHelper.Configuration;
using Deedle;
using Microsoft.Data.Analysis;
using Microsoft.Extensions.Logging;

namespace AccidentesDeMadrid.Repository.Load;

public class DataframeLoader : IDataframeLoader, ITransientService
{
    private readonly ILogger<IDataframeLoader> _logger;


    public DataframeLoader(ILogger<IDataframeLoader> logger)
    {
        _logger = logger;
    }

    public async Task<Frame<int, string>> LoadCsv(IEnumerable<string> paths)
    {
        var tasks = paths.Select(LoadFile); // por cada ruta obtiene la lista de filas

        var results = await Task.WhenAll(tasks); // Espera a que todas se lean

        var rows = results // Los junta en una misma colección
            .SelectMany(x => x)
            .ToList();

        return Frame.FromRecords(rows); // los retorna en un solo  dataframe

    }

    private async Task<List<AccidentCsvRow>> LoadFile(string path)
    {
        if (!Path.Exists(path))
        {
            _logger.LogError("[DATAFRAME-LOADER] No se ha encontrado el archivo: {path}", path);
            throw new FileNotFoundException($"No se encontro el archivo {path}");
        }

        var rows = new List<AccidentCsvRow>();

        using var reader = new StreamReader(path);
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";",
        };

        using var csv = new CsvReader(reader, config);

        await csv.ReadAsync();
        csv.ReadHeader();

        while (await csv.ReadAsync())
        {
            rows.Add(new AccidentCsvRow // Obtener de cada linea cada columna y guardar el conjunto en una lista como objeto
            {
                NumExpediente = csv.GetField<string>(0),
                Fecha = DateTime.ParseExact(
                    csv.GetField<string>(1),
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture),
                Hora = TimeSpan.Parse(
                    csv.GetField<string>(2),
                    CultureInfo.InvariantCulture),
                Localizacion = csv.GetField<string>(3),
                Numero = csv.GetField<string>(4),
                CodDistrito = csv.GetField<string>(5),
                Distrito = csv.GetField<string>(6),
                TipoAccidente = csv.GetField<string>(7),
                EstadoMeteorologico = csv.GetField<string>(8),
                TipoVehiculo = csv.GetField<string>(9),
                TipoPersona = csv.GetField<string>(10),
                RangoEdad = csv.GetField<string>(11),
                Sexo = csv.GetField<string>(12),
                CodLesividad = csv.GetField<string>(13),
                Lesividad = csv.GetField<string>(14),
                CoordenadaXUtm = csv.GetField<string>(15),
                CoordenadaYUtm = csv.GetField<string>(16),
                PositivaAlcohol = csv.GetField<string>(17),
                PositivaDroga = csv.GetField<string>(18)
            });
        }

        return rows;
    }

}