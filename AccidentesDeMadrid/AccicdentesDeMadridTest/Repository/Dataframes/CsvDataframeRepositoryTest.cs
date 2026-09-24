using AccidentesDeMadrid.Repository;
using AccidentesDeMadrid.Repository.Load;
using Deedle;
using Microsoft.Extensions.Logging;
using Moq;

namespace AccicdentesDeMadridTest.Repository.Dataframes;

public class CsvDataframeRepositoryTest
{

    private Mock<ILogger<CsvDataframeRepository>> _logger = null;
    private Mock<IDataframeLoader> _loader = null!;
    private CsvDataframeRepository _repository = null!;
    
    [SetUp]
    public void Setup()
    {
        _logger = new Mock<ILogger<CsvDataframeRepository>>();
        _loader = new Mock<IDataframeLoader>();

        var csvs = new Dictionary<string, string>
        {
            ["2024"] = "2024.csv",
            ["2025"] = "2025.csv",
            ["2026"] = "2026.csv"
        };

        _repository = new CsvDataframeRepository(
            _logger.Object,
            _loader.Object,
            csvs);
    }
    
    [Test]
    public async Task LoadData_ShouldLoadData()
    {
        // Arrange
        var records = new[]
        {
            new
            {
                Nombre = "Kevin",
                Edad = 25
            },
            new
            {
                Nombre = "Peña",
                Edad = 30
            }
        };

        var frame = Frame.FromRecords(records);
        _loader
            .Setup(x => x.LoadCsv(It.IsAny<IEnumerable<string>>()))
            .ReturnsAsync(frame);
        // Act
        await _repository.LoadData();
        // Assert
        var result = _repository.GetALL();
        Assert.That(result.RowCount, Is.EqualTo(2));
        Assert.That(result.ColumnCount, Is.EqualTo(2));
    }
    [Test]
    public void GetALL_WithoutLoadData_ShouldThrow()
    {
        // Act + Assert
        Assert.Throws<InvalidOperationException>(
            () => _repository.GetALL()
        );
    }
    [Test]
    public async Task GetALL_ShouldReturnLoadedData()
    {
        // Arrange
        var records = new[]
        {
            new { Nombre = "Kevin", Edad = 25 },
            new { Nombre = "Peña", Edad = 30 }
        };
        var frame = Frame.FromRecords(records);

        _loader
            .Setup(x => x.LoadCsv(It.IsAny<IEnumerable<string>>()))
            .ReturnsAsync(frame);

        await _repository.LoadData();
        // Act
        var result = _repository.GetALL();
        // Assert
        Assert.That(result.RowCount, Is.EqualTo(2));
        Assert.That(result.ColumnCount, Is.EqualTo(2));
    }
    [Test]
    public async Task ClearData_ShouldClearData()
    {
        // Arrange
        var records = new[]
        {
            new { Nombre = "Kevin", Edad = 25 },
            new { Nombre = "Peña", Edad = 30 }
        };

        var frame = Frame.FromRecords(records);

        _loader
            .Setup(x => x.LoadCsv(It.IsAny<IEnumerable<string>>()))
            .ReturnsAsync(frame);

        await _repository.LoadData();

        // Act
        _repository.ClearData();

        // Assert
        Assert.Throws<InvalidOperationException>(
            () => _repository.GetALL()
        );
    }

}