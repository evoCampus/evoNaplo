using evoNaplo.DTO;
using System.Text;

namespace evoNaplo.Services;

public class CsvExportService : ICsvExportService
{
    public byte[] CreateCsvFile(IEnumerable<EvoCampusApplication> data, ExportData filter)
    {
        var csv = new StringBuilder();
        
        // Header
        var headers = new List<string>();
        if (filter.includeTimestamp) headers.Add(EscapeCsvField("Időbélyeg"));
        if (filter.includeName) headers.Add(EscapeCsvField("Név"));
        if (filter.includeEmail) headers.Add(EscapeCsvField("E-mail cím"));
        if (filter.includePhoneNumber) headers.Add(EscapeCsvField("Telefonszám (pl: 70/1234567)"));
        if (filter.includeMajor) headers.Add(EscapeCsvField("Tanulmányi évfolyam és szak megnevezése (pl.: Miskolci Egyetem Gépészmérnöki és Informatikai Kar ... szak ...specializáció, 3. félév)"));
        if (filter.includeIsFirstTime) headers.Add(EscapeCsvField("Először veszel részt az evoCampus programban?"));
        if (filter.includeGoals)
            headers.Add(EscapeCsvField("Milyen személyes céljaid vannak az evoCampus 2026 tavaszi szemeszteri részvételével?"));
        //if (filter.includeStayInTeam) headers.Add(EscapeCsvField("A jelenlegi csapatban maradnál?"));
        if (filter.includeOtherComments) headers.Add(EscapeCsvField("Egyéb észrevétel, megjegyzés"));

        csv.AppendLine(string.Join(",", headers));

        foreach (var item in data)
        {
            var rowData = new List<string>();
            
            if (filter.includeTimestamp) rowData.Add(EscapeCsvField(item.Timestamp));
            if (filter.includeName) rowData.Add(EscapeCsvField(item.Name));
            if (filter.includeEmail) rowData.Add(EscapeCsvField(item.Email));
            if (filter.includePhoneNumber) rowData.Add(EscapeCsvField(item.PhoneNumber));
            if (filter.includeMajor) rowData.Add(EscapeCsvField(item.Major));
            if (filter.includeIsFirstTime) rowData.Add(EscapeCsvField(item.IsFirstTime));
            if (filter.includeGoals) rowData.Add(EscapeCsvField(item.Goals));
            //if (filter.includeStayInTeam) rowData.Add(EscapeCsvField(item.StayInTeam));
            if (filter.includeOtherComments) rowData.Add(EscapeCsvField(item.OtherComments));

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