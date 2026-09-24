using AccidentesDeMadrid.Repository.Load;
using Microsoft.Extensions.Logging;
using Moq;
using Serilog;

namespace AccicdentesDeMadridTest.Repository;

public class CsvLoaderTest
{
    private Mock<ILogger<CsvLoader>> _loggerMock;
    private CsvLoader _loader;

    [SetUp]
    public void Setup()
    {
        _loggerMock = new Mock<ILogger<CsvLoader>>();
        _loader = new CsvLoader(_loggerMock.Object);
    }

    [Test]
    public void LoadCsv_FileDoesNotExist_ThrowsFileNotFoundException()
    {
        // Arrange
        var path = "Ruta-incorrecta";
        // act assert
        Assert.ThrowsAsync<FileNotFoundException>(async () => await _loader.LoadCsv(path));
    }

    
    [Test]
    public async Task LoadCsv_OnlyHeader_ReturnsEmptyList()
    {
        var path = Path.Combine(
            Path.GetTempPath(),
            $"{Guid.NewGuid()}.csv"
        );

        var header =
            "C1;C2;C3;C4;C5;C6;C7;C8;C9;C10;C11;C12;C13;C14;C15;C16;C17;C18;C19";

        await File.WriteAllTextAsync(path, header);

        var result = await _loader.LoadCsv(path);

        Assert.That(result, Is.Empty);
    }
}