using Microsoft.AspNetCore.Http;
using evoNaplo.DTO;
using static evoNaplo.Services.Common.ExportHeaders;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using System.Globalization;

namespace evoNaplo.Services;

public class CsvImportService : ICsvImportService
{
    public List<ImportData> ProcessCsvFile(IFormFile file)
    {
        var list = new List<ImportData>();
        
        using (var stream = file.OpenReadStream())
        using (var parser = new TextFieldParser(stream))
        {
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");
            
            if (parser.EndOfData)
                return list;

            string[] headers = parser.ReadFields();
            var columnMap = new Dictionary<string, int>();
            
            if (headers != null)
            {
                for (int i = 0; i < headers.Length; i++)
                    columnMap[headers[i].Trim()] = i;
            }
            
            string GetValue(string[] rowValues, string headerName)
            {
                if (columnMap.TryGetValue(headerName, out int index) && index < rowValues.Length)
                {
                    return rowValues[index];
                }
                return string.Empty;
            }

            while (!parser.EndOfData)
            {
                string[] values = parser.ReadFields();
                if (values == null) continue;
                
                var currentApplication = new ImportData
                {
                    Name = GetValue(values, HeaderName),
                    Email = GetValue(values, HeaderEmail),
                    PhoneNumber = GetValue(values, HeaderPhoneNumber),
                    Major = GetValue(values, HeaderMajor),
                    IsFirstTime = GetValue(values, HeaderIsFirstTime),
                    Goals = GetValue(values, HeaderGoals),
                    StayInTeam = GetValue(values, HeaderStayInTeam),
                    OtherComments = GetValue(values, HeaderOtherComments)
                };
                
                string rawTimestamp = GetValue(values, HeaderTimestamp).Trim();
                currentApplication.Timestamp = ParseCustomDate(rawTimestamp);

                list.Add(currentApplication);
            }
        }
        return list;
    }
    
    private DateTime ParseCustomDate(string rawDate)
    {
        if (string.IsNullOrWhiteSpace(rawDate)) return DateTime.Now;

        string cleanDate = rawDate.Replace(" CET", "").Replace(" CEST", "").Replace("de.", "AM").Replace("du.", "PM").Trim();

        string[] formats =
        {
            "yyyy/MM/dd HH:mm:ss",
            "yyyy/MM/dd hh:mm:ss tt",
            "yyyy.MM.dd HH:mm:ss"
        };

        if (DateTime.TryParseExact(cleanDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsed))
            return parsed;
        
        return DateTime.TryParse(cleanDate, out DateTime fallback) ? fallback : DateTime.Now;
    }
}