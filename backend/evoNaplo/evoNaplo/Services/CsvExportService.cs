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
        headers.Add(EscapeCsvField(HeaderTimestamp));
        headers.Add(EscapeCsvField(HeaderName));
        headers.Add(EscapeCsvField(HeaderEmail));
        headers.Add(EscapeCsvField(HeaderPhoneNumber));
        headers.Add(EscapeCsvField(HeaderMajor));
        headers.Add(EscapeCsvField(HeaderIsFirstTime));
        headers.Add(EscapeCsvField(HeaderGoals));
        headers.Add(EscapeCsvField(HeaderStayInTeam));
        headers.Add(EscapeCsvField(HeaderOtherComments));

        csv.AppendLine(string.Join(",", headers));

        foreach (var item in data)
        {
            var rowData = new List<string>();
    
            string amPm = item.Timestamp.Hour < 12 ? "de." : "du.";
            string formattedDate = item.Timestamp.ToString("yyyy/MM/dd HH:mm:ss ") + amPm + " CET";
    
            rowData.Add(EscapeCsvField(formattedDate));
            rowData.Add(EscapeCsvField(item.Name));
            rowData.Add(EscapeCsvField(item.Email));
            rowData.Add(EscapeCsvField(item.PhoneNumber));
            rowData.Add(EscapeCsvField(item.Major));
            rowData.Add(EscapeCsvField(item.IsFirstTime));
            rowData.Add(EscapeCsvField(item.Goals));
            rowData.Add(EscapeCsvField(item.StayInTeam));
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