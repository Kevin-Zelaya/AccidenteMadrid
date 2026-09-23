using System.Globalization;
using AccidentesDeMadrid.Entity;
using AccidentesDeMadrid.Interfaces;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Data.Analysis;
using Microsoft.Extensions.Logging;

namespace AccidentesDeMadrid.Repository.Load;

public class DataframeLoader : IDataframeLoader, ITransientService
{
    private readonly ILogger<DataframeLoader> _logger;

    public DataframeLoader(ILogger<DataframeLoader> logger)
    {
        _logger = logger;
    }

    public async Task<DataFrame> LoadCsv(IEnumerable<string> paths)
    {
        var tasks = paths.Select(LoadFile);

        var results = await Task.WhenAll(tasks);

        var numExpediente = new StringDataFrameColumn("num_expediente");
        var fecha = new StringDataFrameColumn("fecha");
        var hora = new StringDataFrameColumn("hora");
        var localizacion = new StringDataFrameColumn("localizacion");
        var numero = new StringDataFrameColumn("numero");
        var codDistrito = new StringDataFrameColumn("cod_distrito");
        var distrito = new StringDataFrameColumn("distrito");
        var tipoAccidente = new StringDataFrameColumn("tipo_accidente");
        var estadoMeteorologico = new StringDataFrameColumn("estado_meteorológico");
        var tipoVehiculo = new StringDataFrameColumn("tipo_vehiculo");
        var tipoPersona = new StringDataFrameColumn("tipo_persona");
        var rangoEdad = new StringDataFrameColumn("rango_edad");
        var sexo = new StringDataFrameColumn("sexo");
        var codLesividad = new StringDataFrameColumn("cod_lesividad");
        var lesividad = new StringDataFrameColumn("lesividad");
        var coordenadaXUtm = new StringDataFrameColumn("coordenada_x_utm");
        var coordenadaYUtm = new StringDataFrameColumn("coordenada_y_utm");
        var positivaAlcohol = new StringDataFrameColumn("positiva_alcohol");
        var positivaDroga = new StringDataFrameColumn("positiva_droga");

        foreach (var rows in results)
        {
            foreach (var row in rows)
            {
                numExpediente.Append(row[0]);
                fecha.Append(row[1]);
                hora.Append(row[2]);
                localizacion.Append(row[3]);
                numero.Append(row[4]);
                codDistrito.Append(row[5]);
                distrito.Append(row[6]);
                tipoAccidente.Append(row[7]);
                estadoMeteorologico.Append(row[8]);
                tipoVehiculo.Append(row[9]);
                tipoPersona.Append(row[10]);
                rangoEdad.Append(row[11]);
                sexo.Append(row[12]);
                codLesividad.Append(row[13]);
                lesividad.Append(row[14]);
                coordenadaXUtm.Append(row[15]);
                coordenadaYUtm.Append(row[16]);
                positivaAlcohol.Append(row[17]);
                positivaDroga.Append(row[18]);
            }
        }

        return new DataFrame(
            numExpediente,
            fecha,
            hora,
            localizacion,
            numero,
            codDistrito,
            distrito,
            tipoAccidente,
            estadoMeteorologico,
            tipoVehiculo,
            tipoPersona,
            rangoEdad,
            sexo,
            codLesividad,
            lesividad,
            coordenadaXUtm,
            coordenadaYUtm,
            positivaAlcohol,
            positivaDroga
        );
    }

    private async Task<List<string[]>> LoadFile(string path)
    {
        if (!Path.Exists(path))
        {
            throw new FileNotFoundException(
                "No se pudo encontrar el archivo",
                path);
        }

        var rows = new List<string[]>();

        using var reader = new StreamReader(path);

        var config = new CsvConfiguration(
            CultureInfo.InvariantCulture)
        {
            Delimiter = ";"
        };

        using var csv = new CsvReader(reader, config);

        await csv.ReadAsync();

        while (await csv.ReadAsync())
        {
            var row = new string[19];

            for (int i = 0; i < 19; i++)
            {
                row[i] = csv.GetField<string>(i);
            }

            rows.Add(row);
        }

        return rows;
    }
}