using AccidentesDeMadrid.Entity;
using AccidentesDeMadrid.Entity.Enum;
using AccidentesDeMadrid.Repository;
using AccidentesDeMadrid.Service;
using Microsoft.Extensions.Logging;
using Moq;

namespace AccicdentesDeMadridTest.Service;

public class LinqAnalizerTest
{
    private Mock<IRepository> _repositoryMock;
    private AccidentesLinqAnalyzer _analyzer;


    [SetUp]
    public void Setup()
    {
        _repositoryMock = new Mock<IRepository>();

        _analyzer = new AccidentesLinqAnalyzer(
            _repositoryMock.Object
        );
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
    public void GetTotalAccidents_ReturnsTotal()
    {
        var accidents = new List<Accident>
        {
            CreateAccident("1"),
            CreateAccident("2"),
            CreateAccident("3")
        };

        _repositoryMock
            .Setup(x => x.GetALL())
            .Returns(accidents);

        var result = _analyzer.GetTotalAccidentsAsync();

        Assert.That(result, Is.EqualTo(3));
    }

    [Test]
    public void GetAccidentsByDistrict_ReturnsGroupedAccidents()
    {
        var accidents = new List<Accident>
        {
            CreateAccident("1"),
            CreateAccident("2"),
            CreateAccident("3")
        };

        _repositoryMock
            .Setup(x => x.GetALL())
            .Returns(accidents);

        var result = _analyzer.GetAccidentsByDistrictAsync();

        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result["Centro"], Has.Count.EqualTo(3));
    }

    [Test]
    public void GetAccidentsByType_ReturnsGroupedAccidents()
    {
        var accidents = new List<Accident>
        {
            CreateAccident("1"),
            CreateAccident("2"),
            CreateAccident("3")
        };

        _repositoryMock
            .Setup(x => x.GetALL())
            .Returns(accidents);

        var result = _analyzer.GetAccidentsByTypeAsync();

        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result.First().Value, Has.Count.EqualTo(3));
    }

    [Test]
    public void GetAccidentsByWeather_ReturnsGroupedAccidents()
    {
        var accidents = new List<Accident>
        {
            CreateAccident("1"),
            CreateAccident("2")
        };

        _repositoryMock
            .Setup(x => x.GetALL())
            .Returns(accidents);

        var result = _analyzer.GetAccidentsByWeatherAsync();

        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result.First().Value, Has.Count.EqualTo(2));
    }

    [Test]
    public void GetAccidentsBySex_ReturnsGroupedAccidents()
    {
        var male = CreateAccident("1");

        var female = new Accident(
            "2",
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
            Gender.Female,
            InjurySeverity.FatalWithin24Hours,
            "441000",
            "4470000",
            false,
            false
        );

        _repositoryMock
            .Setup(x => x.GetALL())
            .Returns(new List<Accident>
            {
                male,
                male with { CaseNumber = "3" },
                female
            });

        var result = _analyzer.GetAccidentsBySexAsync();

        Assert.That(result[Gender.Male.Message], Has.Count.EqualTo(2));
        Assert.That(result[Gender.Female.Message], Has.Count.EqualTo(1));
    }

    [Test]
    public void GetAccidentsByAgeRange_ReturnsGroupedAccidents()
    {
        var accidents = new List<Accident>
        {
            CreateAccident("1"),
            CreateAccident("2"),
            CreateAccident("3")
        };

        _repositoryMock
            .Setup(x => x.GetALL())
            .Returns(accidents);

        var result = _analyzer.GetAccidentsByAgeRangeAsync();

        Assert.That(result["21-30"], Has.Count.EqualTo(3));
    }


    [Test]
    public void GetAlcoholPositives_ReturnsGroupedAccidents()
    {
        var positive = CreateAccident("1") with
        {
            IsAlcoholPositive = true
        };

        var negative = CreateAccident("2");

        _repositoryMock
            .Setup(x => x.GetALL())
            .Returns(new List<Accident>
            {
                positive,
                negative
            });

        var result = _analyzer.GetAlcoholPositivesAsync();

        Assert.That(result[true], Has.Count.EqualTo(1));
        Assert.That(result[false], Has.Count.EqualTo(1));
    }
 //9
}

