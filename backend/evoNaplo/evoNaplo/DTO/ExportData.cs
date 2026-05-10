namespace evoNaplo.DTO;

public enum ExportFormat
{
    xlsx,
    csv
}

public class ExportData
{
    public bool? IncludeTimestamp { get; set; }
    public string? FilterTimestamp { get; set; }
    
    public bool? IncludeName  { get; set; }
    public string? FilterName { get; set; }
    
    public bool? IncludeEmail  { get; set; }
    public string? FilterEmail { get; set; }
    
    public bool? IncludePhoneNumber { get; set; }
    public string? FilterPhoneNumber { get; set; }
    
    public bool? IncludeMajor  { get; set; }
    public string? FilterMajor { get; set; }
    
    public bool? IncludeIsFirstTime  { get; set; }
    public string? FilterIsFirstTime { get; set; }
    
    public bool? includeGoals  { get; set; }
    public string? filterGoals { get; set; }
    
    public bool? IncludeStayInTeam  { get; set; }
    public string? FilterStayInTeam { get; set; }
    
    public bool? IncludeOtherComments  { get; set; }
    public string? FilterOtherComments { get; set; }
    
    public int? RowCount { get; set; }
    public ExportFormat Format { get; set; } = ExportFormat.xlsx;
}