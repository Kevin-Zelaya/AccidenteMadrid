namespace AccidentesDeMadrid.Entity.Enum;

/// <summary>
/// Representar el distrito sin especificar varios como en otros casos
/// ya que pueden haber otros distritos sin agregar
/// </summary>
/// <param name="Code"></param>
/// <param name="Name"></param>
public record District(short Code, string Name);