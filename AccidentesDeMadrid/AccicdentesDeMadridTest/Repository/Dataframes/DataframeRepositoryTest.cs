using AccidentesDeMadrid.Repository;
using AccidentesDeMadrid.Repository.Load;
using Microsoft.Data.Analysis;
using Microsoft.Extensions.Logging;
using Moq;

namespace AccicdentesDeMadridTest.Repository.Dataframes;

public class DataframeRepositoryTest
{
    private Mock<ILogger<CsvDataframeRepository>> _loggerMock;
    private Mock<IDataframeLoader> _loaderMock;
    
    private CsvDataframeRepository _repository;
    
    private DataframeLoader _loader;
    [SetUp]
    public void Setup()
    {
        _loggerMock = new Mock<ILogger<CsvDataframeRepository>>();
        _loaderMock = new Mock<IDataframeLoader>();
        _loader = new DataframeLoader(new Mock<ILogger<DataframeLoader>>().Object);
        
        var csvs = new Dictionary<string, string>
        {
            { "2024", "2024.csv" },
            { "2025", "2025.csv" },
            { "2026", "2026.csv" }
        };
        
        _repository = new CsvDataframeRepository(
            _loggerMock.Object,
            _loaderMock.Object,
            csvs);
    }
    
    [Test]
    public async Task LoadData_LoadsDataFrame()
    {
        // Arrange
        var dataframe = new DataFrame();

        _loaderMock
            .Setup(x => x.LoadCsv(It.IsAny<IEnumerable<string>>()))
            .ReturnsAsync(dataframe);

        // Act
        await _repository.LoadData();

        // Assert
        var result = _repository.GetALL();

        Assert.That(result, Is.SameAs(dataframe));
    }
    
    [Test]
    public async Task GetALL_ReturnsData()
    {
        // Arrange
        var dataframe = new DataFrame();

        _loaderMock
            .Setup(x => x.LoadCsv(It.IsAny<IEnumerable<string>>()))
            .ReturnsAsync(dataframe);

        await _repository.LoadData();

        // Act
        var result = _repository.GetALL();

        // Assert
        Assert.That(result, Is.SameAs(dataframe));
    }
    
    [Test]
    public async Task ClearData_RemovesAllData()
    {
        // Arrange
        var dataframe = new DataFrame();

        _loaderMock
            .Setup(x => x.LoadCsv(It.IsAny<IEnumerable<string>>()))
            .ReturnsAsync(dataframe);

        await _repository.LoadData();

        // Act
        _repository.ClearData();

        // Assert
        var result = _repository.GetALL();

        Assert.That(result.Rows.Count, Is.EqualTo(0));
        Assert.That(result.Columns.Count, Is.EqualTo(0));
    }
}