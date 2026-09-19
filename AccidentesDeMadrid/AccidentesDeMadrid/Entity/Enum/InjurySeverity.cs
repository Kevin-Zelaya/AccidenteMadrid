namespace AccidentesDeMadrid.Entity.Enum;

public record InjurySeverity(short Code, string Message)
{
    public static readonly InjurySeverity EmergencyRoomDischarge = new(1, "Atención en urgencias sin posterior ingreso");
    public static readonly InjurySeverity HospitalizationUpTo24Hours = new(2, "Ingreso inferior o igual a 24 horas");
    public static readonly InjurySeverity HospitalizationOver24Hours = new(3, "Ingreso superior a 24 horas");
    public static readonly InjurySeverity FatalWithin24Hours = new(4, "Fallecido 24 horas");
    public static readonly InjurySeverity OutpatientCareFollowUp = new(5, "Asistencia sanitaria ambulatoria con posterioridad");
    public static readonly InjurySeverity ImmediateClinicCare = new(6, "Asistencia sanitaria inmediata en centro de salud o mutua");
    public static readonly InjurySeverity OnSiteMedicalCareOnly = new(7, "Asistencia sanitaria sólo en el lugar del accidente");
    public static readonly InjurySeverity NoMedicalCareRequired = new(14, "Sin asistencia sanitaria");
    public static readonly InjurySeverity Unknown = new(77, "Se desconoce");
}

