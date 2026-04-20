using Microsoft.AspNetCore.Http;
using evoNaplo.DTO;
using ClosedXML.Excel;

namespace evoNaplo.Services;

public class ExcelImportService : IExcelImportService
{
    public List<EvoCampusApplication> ProcessExcelFile(IFormFile file)
    {
        var applications = new List<EvoCampusApplication>();
        
        // Load file into memory
        using (var stream = new MemoryStream())
        {
            file.CopyTo(stream);
            
            // Open Excel file
            using (var workbook = new XLWorkbook(stream))
            {
                // Select first sheet in file AND get every row with data present
                var worksheet = workbook.Worksheet(1);
                var rows =  worksheet.RowsUsed();
                
                // Get and store data from file (skip header)
                foreach (var row in rows.Skip(1))
                {
                    var currentApplication = new EvoCampusApplication();
                    currentApplication.Timestamp = row.Cell(1).Value.ToString();
                    currentApplication.Name = row.Cell(2).Value.ToString();
                    currentApplication.Email = row.Cell(3).Value.ToString();
                    currentApplication.PhoneNumber = row.Cell(4).Value.ToString();
                    currentApplication.Major = row.Cell(5).Value.ToString();
                    currentApplication.IsFirstTime = row.Cell(6).Value.ToString();
                    currentApplication.Goals = row.Cell(7).Value.ToString();
                    //currentApplication.StayInTeam = row.Cell(8).Value.ToString();
                    currentApplication.OtherComments = row.Cell(8).Value.ToString();
                    
                    applications.Add(currentApplication);
                }
            }
        }
        
        return applications;
    }
}