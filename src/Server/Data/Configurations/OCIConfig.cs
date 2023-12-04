namespace Server.Data.Configurations;

public class OCIConfig
{
    public string? User { get; set; }
    public string? Fingerprint { get; set; }
    public string? KeyFile { get; set; }
    public string? Tenancy { get; set; }
    public string? CustomCompartmentId { get; set; }
    public string? Region { get; set; }
}
