using AccidentesDeMadrid.Repository.Load;
using Microsoft.Extensions.Logging;
using Moq;

namespace AccicdentesDeMadridTest.Repository.Dataframes;

public class DataframeLoaderTest
{
    private DataframeLoader _loader;

    [SetUp]
    public void Setup()
    {
        var logger = new Mock<ILogger<DataframeLoader>>();
        _loader = new DataframeLoader(logger.Object);
        
        
    }
    [Test]
    public void LoadCsv_FileDoesNotExist_ThrowsFileNotFoundException()
    {
        // Arrange
        var paths = new List<string>
        {
            "archivo-que-no-existe.csv"
        };

        // Act + Assert
        Assert.ThrowsAsync<FileNotFoundException>(
            async () => await _loader.LoadCsv(paths)
        );
    }
    
    [Test]
    public async Task LoadCsv_OnlyHeader_ReturnsEmptyDataFrame()
    {
        // Arrange
        var path = Path.Combine(
            Path.GetTempPath(),
            $"{Guid.NewGuid()}.csv"
        );

        var header =
            "C1;C2;C3;C4;C5;C6;C7;C8;C9;C10;" +
            "C11;C12;C13;C14;C15;C16;C17;C18;C19";

        try
        {
            await File.WriteAllTextAsync(path, header);

            // Act
            var result = await _loader.LoadCsv(new[] { path });

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Rows.Count, Is.EqualTo(0));
            Assert.That(result.Columns.Count, Is.EqualTo(19));
        }
        finally
        {
            File.Delete(path);
        }
    }
    
    [Test]
    public async Task LoadCsv_OneRow_ReturnsDataFrameWithOneRow()
    {
        // Arrange
        var path = Path.Combine(
            Path.GetTempPath(),
            $"{Guid.NewGuid()}.csv"
        );

        var csv =
            "C1;C2;C3;C4;C5;C6;C7;C8;C9;C10;" +
            "C11;C12;C13;C14;C15;C16;C17;C18;C19\n" +
            "1;2024-01-15;18:30;Madrid;25;1;Centro;" +
            "Colision;Despejado;Turismo;Conductor;21-30;" +
            "Hombre;1;Leve;441000;4470000;NO;NO";

        try
        {
            await File.WriteAllTextAsync(path, csv);

            // Act
            var result = await _loader.LoadCsv(new[] { path });

            // Assert
            Assert.That(result.Rows.Count, Is.EqualTo(1));
            Assert.That(result.Columns.Count, Is.EqualTo(19));
        }
        finally
        {
            File.Delete(path);
        }
    }
    [Test]
    public async Task LoadCsv_OneRow_ReturnsCorrectData()
    {
        // Arrange
        var path = Path.Combine(
            Path.GetTempPath(),
            $"{Guid.NewGuid()}.csv"
        );

        var csv =
            "C1;C2;C3;C4;C5;C6;C7;C8;C9;C10;" +
            "C11;C12;C13;C14;C15;C16;C17;C18;C19\n" +
            "1;2024-01-15;18:30;Madrid;25;1;Centro;" +
            "Colision;Despejado;Turismo;Conductor;21-30;" +
            "Hombre;1;Leve;441000;4470000;NO;NO";

        try
        {
            await File.WriteAllTextAsync(path, csv);

            // Act
            var result = await _loader.LoadCsv(new[] { path });

            // Assert
            Assert.That(result.Rows.Count, Is.EqualTo(1));

            Assert.That(
                result["num_expediente"][0],
                Is.EqualTo("1")
            );

            Assert.That(
                result["fecha"][0],
                Is.EqualTo("2024-01-15")
            );

            Assert.That(
                result["distrito"][0],
                Is.EqualTo("Centro")
            );
        }
        finally
        {
            File.Delete(path);
        }
    }
    
    [Test]
    public async Task LoadCsv_MultipleFiles_CombinesAllRows()
    {
        // Arrange
        var path1 = Path.Combine(
            Path.GetTempPath(),
            $"{Guid.NewGuid()}.csv"
        );

        var path2 = Path.Combine(
            Path.GetTempPath(),
            $"{Guid.NewGuid()}.csv"
        );

        var header =
            "C1;C2;C3;C4;C5;C6;C7;C8;C9;C10;" +
            "C11;C12;C13;C14;C15;C16;C17;C18;C19";

        var row1 =
            "1;2024-01-15;18:30;Madrid;25;1;Centro;" +
            "Colision;Despejado;Turismo;Conductor;21-30;" +
            "Hombre;1;Leve;441000;4470000;NO;NO";

        var row2 =
            "2;2024-02-15;19:30;Madrid;30;2;Retiro;" +
            "Colision;Despejado;Turismo;Conductor;31-40;" +
            "Mujer;1;Leve;441001;4470001;NO;NO";

        try
        {
            await File.WriteAllTextAsync(
                path1,
                header + "\n" + row1
            );

            await File.WriteAllTextAsync(
                path2,
                header + "\n" + row2
            );

            // Act
            var result = await _loader.LoadCsv(
                new[] { path1, path2 }
            );

            // Assert
            Assert.That(result.Rows.Count, Is.EqualTo(2));
            Assert.That(result.Columns.Count, Is.EqualTo(19));
        }
        finally
        {
            File.Delete(path1);
            File.Delete(path2);
        }
    }
    
}