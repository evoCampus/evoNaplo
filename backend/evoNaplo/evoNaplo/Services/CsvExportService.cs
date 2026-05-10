using evoNaplo.DTO;
using System.Text;
using static evoNaplo.Services.Common.ExportHeaders;

namespace evoNaplo.Services;

public class CsvExportService : ICsvExportService
{
    public byte[] CreateFile(IEnumerable<ImportData> data, ExportData filter)
    {
        var csv = new StringBuilder();
        
        // Header
        var headers = new List<string>();
        if (filter.IncludeTimestamp ?? true) 
            headers.Add(EscapeCsvField(HeaderTimestamp));
        if (filter.IncludeName ?? true) 
            headers.Add(EscapeCsvField(HeaderName));
        if (filter.IncludeEmail ?? true) 
            headers.Add(EscapeCsvField(HeaderEmail));
        if (filter.IncludePhoneNumber ?? true) 
            headers.Add(EscapeCsvField(HeaderPhoneNumber));
        if (filter.IncludeMajor ?? true) 
            headers.Add(EscapeCsvField(HeaderMajor));
        if (filter.IncludeIsFirstTime ?? true) 
            headers.Add(EscapeCsvField(HeaderIsFirstTime));
        if (filter.includeGoals ?? true) 
            headers.Add(EscapeCsvField(HeaderGoals));
        if (filter.IncludeStayInTeam ?? true) 
            headers.Add(EscapeCsvField(HeaderStayInTeam));
        if (filter.IncludeOtherComments ?? true) 
            headers.Add(EscapeCsvField(HeaderOtherComments));

        csv.AppendLine(string.Join(",", headers));

        foreach (var item in data)
        {
            var rowData = new List<string>();
            
            if (filter.IncludeTimestamp ?? true) 
            {
                string amPm = item.Timestamp.Hour < 12 ? "de." : "du.";
                string formattedDate = item.Timestamp.ToString("yyyy/MM/dd HH:mm:ss ") + amPm + " CET";
        
                rowData.Add(EscapeCsvField(formattedDate));
            }
            if (filter.IncludeName ?? true) 
                rowData.Add(EscapeCsvField(item.Name));
            if (filter.IncludeEmail ?? true) 
                rowData.Add(EscapeCsvField(item.Email));
            if (filter.IncludePhoneNumber ?? true) 
                rowData.Add(EscapeCsvField(item.PhoneNumber));
            if (filter.IncludeMajor ?? true) 
                rowData.Add(EscapeCsvField(item.Major));
            if (filter.IncludeIsFirstTime ?? true) 
                rowData.Add(EscapeCsvField(item.IsFirstTime));
            if (filter.includeGoals ?? true) 
                rowData.Add(EscapeCsvField(item.Goals));
            if (filter.IncludeStayInTeam ?? true) 
                rowData.Add(EscapeCsvField(item.StayInTeam));
            if (filter.IncludeOtherComments ?? true) 
                rowData.Add(EscapeCsvField(item.OtherComments));

            csv.AppendLine(string.Join(",", rowData));
        }

        var utf8 = Encoding.UTF8.GetPreamble();
        var csvBytes = Encoding.UTF8.GetBytes(csv.ToString());
        
        return utf8.Concat(csvBytes).ToArray();
    }
    
    private string EscapeCsvField(string field)
    {
        if (string.IsNullOrEmpty(field)) 
            return "";

        if (field.Contains(',') || field.Contains('"') || field.Contains('\n'))
        {
            field = field.Replace("\"", "\"\"");
            return $"\"{field}\"";
        }

        return field;
    }
}