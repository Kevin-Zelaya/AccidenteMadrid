using AccidentesDeMadrid.Repository.Load;
using Moq;

using Microsoft.Extensions.Logging;

namespace AccicdentesDeMadridTest.Repository.Dataframes;

public class CsvDataframeLoaderTest
{
    
    private Mock<ILogger<IDataframeLoader>> _loggerMock;
    private IDataframeLoader _loader;
    
    [SetUp]
    public void Setup()
    {
        _loggerMock = new Mock<ILogger<IDataframeLoader>>();
        _loader = new DataframeLoader(_loggerMock.Object);
    }

    [Test]
    public void LoadCsv_FileNotFound()
    {
        // Arrange
        var path = new List<string> {
             "Prueba.csv"
        };
        // Act + Assert
        Assert.ThrowsAsync<FileNotFoundException>(
            async () => await _loader.LoadCsv(path)
            );
    }
}