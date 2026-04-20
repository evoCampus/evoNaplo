namespace evoNaplo.DTO;

public enum ExportFormat
{
    xlsx,
    csv
}

public class ExportData
{
    public bool includeTimestamp { get; set; } = false;
    public string? filterTimestamp { get; set; } = null;
    
    public bool includeName  { get; set; } = false;
    public string? filterName { get; set; } = null;
    
    public bool includeEmail  { get; set; } = false;
    public string? filterEmail { get; set; } = null;
    
    public bool includePhoneNumber { get; set; } = false;
    public string? filterPhoneNumber { get; set; } = null;
    
    public bool includeMajor  { get; set; } = false;
    public string? filterMajor { get; set; } = null;
    
    public bool includeIsFirstTime  { get; set; } = false;
    public string? filterIsFirstTime { get; set; } = null;
    
    public bool includeGoals  { get; set; } = false;
    public string? filterGoals { get; set; } = null;
    
    //public bool includeStayInTeam  { get; set; } = false;
    //public string? filterStayInTeam { get; set; } = null;
    
    public bool includeOtherComments  { get; set; } = false;
    public string? filterOtherComments { get; set; } = null;
    
    public int? rowCount { get; set; }
    public ExportFormat Format { get; set; } = ExportFormat.xlsx;
}