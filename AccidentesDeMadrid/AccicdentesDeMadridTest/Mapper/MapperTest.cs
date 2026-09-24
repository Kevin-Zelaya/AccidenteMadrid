using AccidentesDeMadrid.Dto;
using AccidentesDeMadrid.Entity;
using AccidentesDeMadrid.Mapper;

namespace AccicdentesDeMadridTest.Mapper;

public class MapperTest
{
    [Test]
    public void MapDto_ToEntity()
    {
        // Arrange
        var accident = new AccidentDto(
            "2025S000056",
            "01/01/2025",
            "0:49:00",
            "CALL. LOPEZ DE HOYOS / CALL. ROS DE OLANO",
            "140",
            "5",
            "CHAMARTÍN",
            "Colisión fronto-lateral",
            "Despejado",
            "Ciclomotor",
            "Conductor",
            "De 30 a 34 años",
            "Hombre",
            "7",
            "Asistencia sanitaria sólo en el lugar del accidente",
            "442966",
            "4477385",
            "N",
            ""
        );
        // Acc + Assert
        Assert.That(accident.ToEntity(), Is.InstanceOf<Accident>());
    }[Test]
    public void MapWrongDto_ToEntity()
    {
        // Arrange
        var accident = new AccidentDto(
            "2025S000056",
            "01/01/2025",
            "0:49:00",
            "CALL. LOPEZ DE HOYOS / CALL. ROS DE OLANO",
            "140",
            "5",
            "CHAMARTÍN",
            "Colisión fronto-lateral",
            "Bien frio",
            "Motobici",
            "Conductor",
            "De 30 a 34 años",
            "Hombre",
            "",
            "",
            "442966",
            "4477385",
            "",
            "1"
        );
        
        var accident2 = new AccidentDto(
            "2025S000056",
            "01/01/2025",
            "0:49:00",
            "CALL. LOPEZ DE HOYOS / CALL. ROS DE OLANO",
            "140",
            "5",
            "CHAMARTÍN",
            "",
            "Bien frio",
            "Motobici",
            "Conductor",
            "De 30 a 34 años",
            "Hombre",
            "28",
            "Pierna pa atras",
            "442966",
            "4477385",
            "",
            "1"
        );
        // Acc + Assert
        Assert.That(accident.ToEntity(), Is.InstanceOf<Accident>()); 
        Assert.That(accident2.ToEntity(), Is.InstanceOf<Accident>());
        // Dos intancias con datos correctos para comprobar el mapper
    }
}