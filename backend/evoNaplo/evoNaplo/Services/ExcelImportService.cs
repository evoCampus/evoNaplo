using Microsoft.AspNetCore.Http;
using evoNaplo.DTO;
using static evoNaplo.Services.Common.ExportHeaders;
using ClosedXML.Excel;
using System.Globalization;

namespace evoNaplo.Services;

public class ExcelImportService : IExcelImportService
{
    public List<ImportData> ProcessExcelFile(IFormFile file)
    {
        var applications = new List<ImportData>();
        
        using (var stream = new MemoryStream())
        {
            file.CopyTo(stream);
            
            using (var workbook = new XLWorkbook(stream))
            {
                var worksheet = workbook.Worksheet(1);
                var rows =  worksheet.RowsUsed();

                var headerRow = rows.First();
                var columnMap =  new Dictionary<string, int>();

                foreach (var cell in headerRow.CellsUsed())
                {
                    string headerName = cell.Value.ToString().Trim();
                    columnMap[headerName] = cell.Address.ColumnNumber;
                }

                string GetCellValue(IXLRow row, string headerName)
                {
                    if (columnMap.TryGetValue(headerName, out int colIndex))
                    {
                        return row.Cell(colIndex).Value.ToString();
                    }
                    return string.Empty;
                }
                
                foreach (var row in rows.Skip(1))
                {
                    var currentApplication = new ImportData
                    {
                        Name = GetCellValue(row, HeaderName),
                        Email = GetCellValue(row, HeaderEmail),
                        PhoneNumber =  GetCellValue(row, HeaderPhoneNumber),
                        Major = GetCellValue(row, HeaderMajor),
                        IsFirstTime = GetCellValue(row, HeaderIsFirstTime),
                        Goals = GetCellValue(row, HeaderGoals),
                        StayInTeam = GetCellValue(row, HeaderStayInTeam),
                        OtherComments = GetCellValue(row, HeaderOtherComments)
                    };
                    string rawDate = GetCellValue(row, HeaderTimestamp).Trim();
                    currentApplication.Timestamp = ParseCustomDate(rawDate);
                    applications.Add(currentApplication);
                }
            }
        }
        
        return applications;
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