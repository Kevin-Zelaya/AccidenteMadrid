using AccidentesDeMadrid.Entity;
using AccidentesDeMadrid.Entity.Enum;
using AccidentesDeMadrid.Repository;
using AccidentesDeMadrid.Repository.Load;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;

namespace AccicdentesDeMadridTest.Repository;

public class CsvAccidentRepositoryTest
{
    private Mock<ILogger<CSVAccidentRepository>> _loggerMock;
    private Mock<ICsvLoader> _csvLoaderMock;

    private CSVAccidentRepository _repository;

    private CsvLoader _loader;

    [SetUp]
    public void setup()
    {
        _loggerMock = new Mock<ILogger<CSVAccidentRepository>>();
        _csvLoaderMock = new Mock<ICsvLoader>();
        _loader = new CsvLoader(new Mock<ILogger<CsvLoader>>().Object);

        var csvs = new Dictionary<string, string>
        {
            { "2024", "2024.csv" },
            { "2025", "2025.csv" },
            { "2026", "2026.csv" }
        };

        _repository = new CSVAccidentRepository(
            _loggerMock.Object,
            _csvLoaderMock.Object,
            csvs);
    }

    [Test]
    public async Task LoadData_LoadsData()
    {
        // Arrange
        var accidents2024 = new List<Accident>();

        _csvLoaderMock
            .Setup(x => x.LoadCsv("2024.csv"))
            .ReturnsAsync(accidents2024);
        _csvLoaderMock
            .Setup(x => x.LoadCsv("2025.csv"))
            .ReturnsAsync(new List<Accident>());
        _csvLoaderMock
            .Setup(x => x.LoadCsv("2026.csv"))
            .ReturnsAsync(new List<Accident>());
        
        // Act
        await _repository.LoadData();
        
        // Assert
        var result = _repository.GetByYear("2024");

        Assert.That(result, Is.Not.Null);

    }

    [Test]
    public async Task GetByYear_ReturnsCorrectYear()
    {
        // Arange
        var accidents2024 = new List<Accident>();
        
        _csvLoaderMock // llamar al csv del 2024
            .Setup(x => x.LoadCsv("2024.csv"))
            .ReturnsAsync(accidents2024);
        _csvLoaderMock
            .Setup(x => x.LoadCsv(It.IsAny<string>()))
            .ReturnsAsync(new List<Accident>());
        await _repository.LoadData();
        // Act
        var result = _repository.GetByYear("2024");
        // Assert
        Assert.That(result, Is.EqualTo(accidents2024));
    }

    [Test]
    public async Task GetAll_ReturnsAllAccidents()
    {
        // Arrange
        var accidents2024 = new List<Accident>
        {
            CreateAccident("5694fd5"),
            CreateAccident("5694zd5")
        };
        var accidents2025 = new List<Accident>
        {
            CreateAccident("5694xd5"),
            CreateAccident("5692fd5"),
            CreateAccident("5694fd6")
        };
        
        _csvLoaderMock
            .Setup(x => x.LoadCsv("2024.csv"))
            .ReturnsAsync(accidents2024);
        _csvLoaderMock
            .Setup(x => x.LoadCsv("2025.csv"))
            .ReturnsAsync(accidents2025);
        _csvLoaderMock
            .Setup(x => x.LoadCsv("2026.csv"))
            .ReturnsAsync(new List<Accident>());
        
        // Act
        await _repository.LoadData();
        var result = _repository.GetALL();
        // Assert
        Assert.That(result, Has.Count.EqualTo(5));
    }

    [Test]
    public async Task GetByYear_ReturnsAccidentsOfRequestedYear()
    {
        // Arange
        var accidents2024 = new List<Accident>
        {
            CreateAccident("5694fd5"),
            CreateAccident("5694zd5")
        };

        _csvLoaderMock
            .Setup(x => x.LoadCsv("2024.csv"))
            .ReturnsAsync(accidents2024);
        
        await _repository.LoadData();
        // Act
        var result = _repository.GetByYear("2024");
        // Assert
        Assert.That(result, Has.Count.EqualTo(2));
        Assert.That(result[0].CaseNumber, Is.EqualTo("5694fd5"));
    }

    [Test]
    public async Task ClearData_RemovesAllData()
    {
        // Arrange
        var accidents2024 = new List<Accident>
        {
            CreateAccident("5694fd5")
        };
        
        _csvLoaderMock
            .Setup(x => x.LoadCsv("2024.csv"))
            .ReturnsAsync(accidents2024);
        // Act
        await _repository.LoadData();
        _repository.ClearData();
        // Assert
        Assert.That(_repository.GetALL(), Is.Empty);
    }
    
    private Accident CreateAccident(string caseNumber)
    {
        return new Accident(
            caseNumber,
            new DateTime(2024, 1, 15),
            new TimeOnly(18, 30),
            "Calle Alcalá",
            "25",
            new District(1, "Centro"),
            AccidentType.AngleCollision,
            WeatherCondition.Cloudy,
            VehicleType.ArticulatedVehicle,
            PersonType.Driver,
            "21-30",
            Gender.Male,
            InjurySeverity.FatalWithin24Hours,
            "441000",
            "4470000",
            false,
            false
        );
        
    } 
    [Test]
    public async Task GetByYear_YearDoesNotExist_ReturnsNull()
    {
        // Arrange
        _csvLoaderMock
            .Setup(x => x.LoadCsv(It.IsAny<string>()))
            .ReturnsAsync(new List<Accident>());

        await _repository.LoadData();

        // Act
        var result = _repository.GetByYear("1999");

        // Assert
        Assert.That(result, Is.Null);
    }
    [Test]
    public async Task LoadData_CallsLoaderForEachCsv()
    {
        // Arrange
        _csvLoaderMock
            .Setup(x => x.LoadCsv(It.IsAny<string>()))
            .ReturnsAsync(new List<Accident>());

        // Act
        await _repository.LoadData();

        // Assert
        _csvLoaderMock.Verify(
            x => x.LoadCsv(It.IsAny<string>()),
            Times.Exactly(3)
        );
    }
    [Test]
    public async Task LoadCsv_InvalidData_ThrowsException()
    {
        // Arrange
        var path = Path.Combine(
            Path.GetTempPath(),
            $"{Guid.NewGuid()}.csv"
        );

        var csv =
            "C1;C2;C3;C4;C5;C6;C7;C8;C9;C10;C11;C12;C13;C14;C15;C16;C17;C18;C19\n" +
            "a;b;c;d;e;f;g;h;i;j;k;l;m;n;o;p;q;r;s";

        await File.WriteAllTextAsync(path, csv);

        // Act + Assert
        Assert.ThrowsAsync<FormatException>(
            async () => await _loader.LoadCsv(path)
        );
    }
}