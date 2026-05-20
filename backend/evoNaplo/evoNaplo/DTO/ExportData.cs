namespace evoNaplo.DTO;

public enum ExportFormat
{
    xlsx,
    csv
}

public class ExportData
{
    public string? FilterTimestamp { get; set; }
    public string? FilterName { get; set; }
    public string? FilterEmail { get; set; }
    public string? FilterPhoneNumber { get; set; }
    public string? FilterMajor { get; set; }
    public string? FilterIsFirstTime { get; set; }
    public string? FilterGoals { get; set; }
    public string? FilterStayInTeam { get; set; }
    public string? FilterOtherComments { get; set; }
    
    public int? RowCount { get; set; }
    public ExportFormat Format { get; set; } = ExportFormat.xlsx;
}