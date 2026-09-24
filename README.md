# Accidentes de Madrid

Práctica sobre el acceso a datos y el manejo de colecciones. En esta práctica veremos el manejo de operaciones asíncronas dentro de la aplicación y dos formas de manejar y estructurar los datos.

## Objetivo

Debemos procesar tres archivos CSV con datos reales sobre accidentes de tráfico en Madrid. Para ello, deberemos en primer lugar dar estructura a los datos provenientes del exterior. Esto se realizará mediante colecciones (listas y arrays) o de forma tabular, como en el caso de los DataFrames.

Dentro de los objetivos a cumplir dentro de la aplicación tenemos:

- [x] Leer los 3 ficheros CSV.
- [x] Combinar los datos en una sola colección.
- [x] Realizar 30 consultas LINQ sobre el conjunto combinado.
- [x] Realizar las mismas 30 consultas usando DataFrames.
- [x] Medir y mostrar los tiempos de ejecución de cada operación.
- [x] Optimizar el rendimiento total del programa usando los recursos del sistema disponibles y justificar las decisiones tomadas.

## Estructura del proyecto

El proyecto está organizado mediante una separación lógica por capas, con el objetivo de distribuir las diferentes responsabilidades de la aplicación.

Accidentes de Madrid
```
AccidentesMadrid/
├── Program.cs
├── AccidentesMadrid.csproj
├── Program.cs
├── appsettings.json                            ← Configuración en JSON
├── Configurations/                             
├── └── AppConfig.cs                            ← Proveedor de configuraciones 
├── data/                                       ← Contenedora de los csv
│   ├── 2024_Accidentalidad.csv    
│   ├── 2025_Accidentalidad.csv      
│   └── 2026_Accidentalidad.csv      
├── Dependency/
│   └── DependencyProvider.cs                  ← Inyección de dependencias.
├── Entity/
│   ├── Accident.cs                            ← Entidad principal
│   └── Enum/                                  ← Records para diferentes propiedades
│        ├── AccidentType.cs                   ← Actuan como enums
│        ├── Distric.cs
│        ├── Gender.cs
│        ├── InjurySeverity.cs
│        ├── PersonType.cs
│        ├── VehicleType.cs
│        └── WeatherCondition.cs
├── Dto/                                       ← Modelos para el primer paso 
│   ├── AccidentCsvRow.cs                      ← de conversión
│   └── AccidentDto.cs 
├── Interfaces/
│   ├── IScopedService.cs
│   ├── ITransient.cs
│   └── ISingletoService.cs
├── Mapper/                                   ← Patrón mapper, facilita la converción 
│   ├── IScopedService.cs                     ← de dto a entidad
├── Repositories/                             ← Repositorios para cada caso
│   ├── Dataframe/
│   │     ├── CsvDataframeRepository.cs         
│   │     └── IDataframeRepository.cs
│   ├── Linq/
│   │     ├── CsvAccidentRepository.cs
│   │     └── IRepository.cs
│   ├── Load/                                 ← Capa de lectura, acceso a los csvs
│        ├── DataframeLoader.cs
│        ├── IDatarameloader.cs
│        ├── ICsvLoader.cs
│        └── CsvLoader.cs
└── Services/                                 ← Servicios de consultas para cada caso
    ├── Common/
    │       ├── IAccidentAnalizer.cs          ← Interfaz común para los analizer
    ├── DataframesService/
    │       ├── AccidentsDataframesAnalizer.cs
    │       └── IDataframesAccidentAnalizer.cs
    └── LinqService/
            ├── AccidentsLinqAnalizer.cs
            └── ILinqAccidentAnalizer.cs

```
En el esquema anterior se reflejan, mediante pequeños comentarios, las responsabilidades de las diferentes partes que conforman la estructura. Profundizaré más en unas que en otras, pero las agruparé por similitudes.

### Configuración y proveedor de dependencias.

Para empezar, tenemos nuestro .csproj. En este declaramos las dependencias (paquetes) que utilizaremos en la aplicación. Aquí también se definen los archivos que se deberán cargar junto al compilado de la aplicación, como en el caso de nuestros CSV.

Con respecto a las dependencias de este proyecto tenemos:

    - coverlet → Cobertura de tests.
    - CSharpFunctionalExtensions → Manejo de resultados.
    - CsvHelper → Lectura de CSV.
    - Deedle → Análisis de datos con DataFrames.
    - Configuration → Configuración mediante JSON.
    - Moq → Mocks para tests.
    - Serilog → Logs de la aplicación.
    - Microsoft.Extensions.DependencyInjection → Inyección de dependencias.
    - Scrutor → Registro automático de servicios.

Con respecto a la configuración, esto lo hago desde la clase AppConfig. En esta utilizo mi appsettings.json para cargar valores predefinidos que en él asignamos, de tal forma que se centraliza la definición de los valores que queremos asignar: cadenas de conexión, configuración para logs y rutas de archivos. Estos son los usos que en este caso le di a JSON, y desde AppConfig me encargo de leerlos e insertarlos en campos que serán accesibles en toda la aplicación.

Lo siguiente es el proveedor de dependencias, que nos permite centralizar la inyección de dependencias, permitiendo además asignar un tiempo de vida a las diferentes implementaciones. Además, a esto le podemos aplicar automatización gracias a...

#### Scrutor: 

Es una librería para .NET Core que permite escanear la aplicación en busca de dependencias para las diferentes implementaciones que configuremos. Facilita:
    - Escaneo automático de ensamblados.
    - Registración automática de servicios.
    - Personalización del escaneo para filtrar los tipos que se incluirán o excluiran del registro
    - Reglas de convención y registro de servicios basados en determinadas convenciones de nomenclatura o estructura.


### Entidades y mapeo

La aplicación está basada en accidentes, por lo que uno de los primeros pasos a realizar en cualquier programa es el diseño de los datos. En este caso, se trata de obtener los datos y estructurar de qué manera van a ser tratados en la aplicación.

En mi caso, la entidad principal es Accident, formada a partir de un record con propiedades entre las cuales se encuentran otros record que me ayudan a mapear de forma más controlada los datos. Este enfoque permite separar o prescindir de parte de la información en caso de solo poder disponer de una parte de ella, como en el caso de:

    - AccidentType
    - Distric
    - Gender
    - InjurySeveriry
    - PersonType
    - VehicleType
    - WeatherCondition

Los anteriores son record y están compuestos, por lo general, por dos propiedades: Message e Id (esto en el caso de que en la tabla disponga de ambos valores). Tomé este enfoque tomando como referencia el patrón enum. Esto lo hago creando un record principal y anidando dentro de él, de forma estática, definiciones de este mismo record, permitiéndome tener valores predefinidos representados por algo similar a una clave o Id, y un valor o Message.

Los datos de los CSV se obtienen y transforman en clases auxiliares (DTOs) que permiten darles un formato temporal para recibirlos y posteriormente formatearlos.

Aquí entra en juego el Mapper, que mediante métodos de extensión me permite aplicar el patrón Mapper, facilitando la comprensión de los datos y el formateo de los mismos.

En este caso utilicé una característica del lenguaje que ya había aprendido anteriormente en Java. Esta característica es...

#### Reflexión

Permite obtener información sobre ensamblados cargados y los tipos definidos en ellos, en tiempo de ejecución.

Esta característica la pongo en práctica al parsear los campos, ya que de esta forma puedo obtener los elementos anidados en los record representativos de los accidentes. Un ejemplo sería:

// Condición climatica
    public static Gender? ParseGender(string gender)
    {
        return typeof(Gender)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Select(f => f.GetValue(null) as Gender)
            .FirstOrDefault(g => g.Message == gender);
    }

Este enfoque está orientado sobre todo a la parte de LINQ, ya que LINQ la utilizamos en este caso para manejar colecciones de objetos.

Para el caso de los DataFrames, utilizo una librería diferente; en este caso es Deedle.

<PackageReference Include="Deedle" Version="8.1.0" />

Lo elegí debido a su facilidad de uso y a las herramientas que ofrece en comparación con su contraparte de Microsoft. Por lo que su uso se extiende a lo largo de esta parte de la aplicación, principalmente para la agrupación y el filtrado.


### Lectura y manejo

En ambos casos me apoyo en el paquete CsvHelper para la lectura de los registros. La lectura se hace de manera asíncrona, ejecutando el mismo proceso para cada archivo.

Posteriormente, los datos se mapean a las clases. En el caso del loader de LINQ, se convierte el CSV a DTO para luego mapearlo a la entidad. En el loader de DataFrames, se convierten los datos en una clase que simula las columnas de una tabla para posteriormente convertir cada registro en filas de un DataFrame por cada archivo, unificando los tres en su retorno.

Luego, en cada repositorio se almacenan los registros, ya en la forma en la que serán consultados: una colección para LINQ y un DataFrame para el otro caso.

Por último, cada servicio llama a su respectivo repositorio para obtener los registros y poder trabajar sobre ellos.

#### Servicio

En cada servicio se encuentran las diferentes consultas planteadas en la práctica, con un total de 30 consultas para cada implementación y 60 consultas entre ambas.

Aunque los dos servicios tienen que realizar las mismas operaciones, cada uno trabaja sobre una estructura de datos diferente. Para ello, ambos implementan una interfaz común que define los métodos que deben proporcionar.

En el servicio de LINQ, las consultas se realizan directamente sobre la colección de objetos obtenida del repositorio. Se utilizan operaciones como GroupBy, Select, OrderByDescending y Take para agrupar, transformar y filtrar los datos.

Por ejemplo, para obtener los cinco distritos con más accidentes:
```csharp
public IEnumerable<(string District, int Total)> GetAccidentsByDistrictAsync() { return _repository.GetALL() .GroupBy(r => r.District.Name) .Select(g => ( District: g.Key, Total: g.Count() )) .OrderByDescending(x => x.Total) .Take(5); }
```
En el caso de los DataFrames, las consultas parten de los datos almacenados en forma tabular. Por ejemplo, para realizar la misma consulta se agrupan las filas utilizando la columna correspondiente:
```csharp
public IEnumerable<(string District, int Total)> GetAccidentsByDistrictAsync() { var data = _repository.GetALL(); var grouped = data.GroupRowsBy<string>("Distrito"); return grouped .RowKeys .Select(g => ( District: g.Item1, Total: g.Item2 )) .OrderByDescending(x => x.Total) .Take(5) .ToList(); }
```
En el caso de los DataFrames, todas las consultas utilizan una combinación de operaciones propias de DataFrames y LINQ. Las operaciones sobre el DataFrame permiten realizar el filtrado y agrupación de los datos, mientras que LINQ se utiliza posteriormente para transformar y procesar los resultados obtenidos.

Esto implica que las consultas de DataFrames tienen un paso adicional de procesamiento mediante LINQ. Además, permite mantener el contrato definido por la interfaz común y devolver los resultados con una estructura equivalente a la utilizada por el servicio de LINQ.

#### Capa de muestra.

Esta se encuentra en Program, donde se realizan las consultas a los diferentes servicios. Aunque se denominan servicios, en este caso actúan como una capa encargada de consultar y procesar los datos. Además, utilizo un método auxiliar para reutilizar los textos que aparecen en pantalla.

Al final, ambos servicios retornan la misma información, por lo que se pueden reutilizar las mismas plantillas para mostrar los resultados.

## Comparativa

En cuanto al manejo de los datos, ambas herramientas presentan diferencias importantes, aunque comparten ciertas similitudes en la lógica de las consultas. Los DataFrames representan los datos de forma tabular, mientras que LINQ permite trabajar directamente sobre colecciones de objetos mediante operaciones encadenadas.

Sin embargo, la forma de manipular y almacenar los datos es diferente. Con LINQ, las consultas se realizan directamente sobre las colecciones mediante operaciones como agrupaciones, filtros y proyecciones. En el caso de los DataFrames, los datos se organizan en filas y columnas y las operaciones se realizan sobre esta estructura tabular.

Ambas herramientas tienen sus ventajas y sus limitaciones, y su enfoque puede resultar más adecuado dependiendo del tipo y volumen de datos que se necesite manejar. Los DataFrames están orientados al manejo y análisis de datos estructurados en grandes cantidades.

En esta implementación concreta, todas las consultas realizadas con DataFrames utilizan también LINQ para procesar los resultados obtenidos. Esto añade un paso adicional de procesamiento que debe tenerse en cuenta al comparar los tiempos de ejecución.


## Comparativa de rendimiento

## Comparativa de rendimiento

| Métrica | LINQ Analyzer | DataFrames Analyzer |
|---|---:|---:|
| **Tiempo total** | 5152 ms | 8681 ms |
| **Tiempo medio** | 52,53 ms | 213,60 ms |
| **Más rápido** | 6 ms | 39 ms |
| **Más lento** | 134 ms | 1097 ms |

En las pruebas realizadas, las consultas mediante LINQ presentan un menor tiempo de ejecución tanto en el tiempo total como en el tiempo medio. En el caso de los DataFrames, el tiempo de ejecución es mayor, algo que puede estar relacionado con el procesamiento adicional mediante LINQ utilizado en esta implementación.
