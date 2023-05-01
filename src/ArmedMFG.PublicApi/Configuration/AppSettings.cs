namespace ArmedMFG.PublicApi.Configuration;

public class ConfigFilesSettings
{
    public string? ProductInventoryJsonFilePath { get; }
}

public class DateParsingSettings
{
    public string? DefaultDisplayDateFormat { get; set; }
    public string? DefaultInputDateFormat { get; set; }
}
