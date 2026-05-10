using evoNaplo.DTO;
using static evoNaplo.Services.Common.ExportHeaders;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.InkML;

namespace evoNaplo.Services
{
    public class ExcelExportService : IExcelExportService
    {
        
        public byte[] CreateFile(IEnumerable<ImportData> data, ExportData filter)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Exported Data");
                
                int headerCol = 1;
                if (filter.IncludeTimestamp ?? true) 
                    worksheet.Cell(1, headerCol++).Value = HeaderTimestamp;
                if (filter.IncludeName ?? true) 
                    worksheet.Cell(1, headerCol++).Value = HeaderName;
                if (filter.IncludeEmail ?? true)
                    worksheet.Cell(1, headerCol++).Value = HeaderEmail;
                if (filter.IncludePhoneNumber ?? true)
                    worksheet.Cell(1, headerCol++).Value = HeaderPhoneNumber;
                if (filter.IncludeMajor ?? true)
                    worksheet.Cell(1, headerCol++).Value = HeaderMajor;
                if (filter.IncludeIsFirstTime ?? true)
                    worksheet.Cell(1, headerCol++).Value = HeaderIsFirstTime;
                if (filter.includeGoals ?? true)
                    worksheet.Cell(1, headerCol++).Value = HeaderGoals;
                if (filter.IncludeStayInTeam ?? true)
                    worksheet.Cell(1, headerCol++).Value = HeaderStayInTeam;
                if (filter.IncludeOtherComments ?? true)
                    worksheet.Cell(1, headerCol++).Value = HeaderOtherComments;
                
                int currentRow = 2;
                foreach (var item in data)
                {
                    int dataCol = 1;
                    if (filter.IncludeTimestamp ?? true) 
                    {
                        string amPm = item.Timestamp.Hour < 12 ? "de." : "du.";
                        string formattedDate = item.Timestamp.ToString("yyyy/MM/dd HH:mm:ss ") + amPm + " CET";
        
                        worksheet.Cell(currentRow, dataCol++).Value = formattedDate; 
                    }
                    if (filter.IncludeName ?? true)
                        worksheet.Cell(currentRow, dataCol++).Value = item.Name;
                    if (filter.IncludeEmail ?? true)
                        worksheet.Cell(currentRow, dataCol++).Value = item.Email;
                    if (filter.IncludePhoneNumber ?? true)
                        worksheet.Cell(currentRow, dataCol++).Value = item.PhoneNumber;
                    if (filter.IncludeMajor ?? true)
                        worksheet.Cell(currentRow, dataCol++).Value = item.Major;
                    if (filter.IncludeIsFirstTime ?? true)
                        worksheet.Cell(currentRow, dataCol++).Value = item.IsFirstTime;
                    if (filter.includeGoals ?? true)
                        worksheet.Cell(currentRow, dataCol++).Value = item.Goals;
                    if (filter.IncludeStayInTeam ?? true)
                        worksheet.Cell(currentRow, dataCol++).Value = item.StayInTeam;
                    if (filter.IncludeOtherComments ?? true)
                        worksheet.Cell(currentRow, dataCol++).Value = item.OtherComments;
                    currentRow++;
                }
                
                worksheet.Columns().AdjustToContents();
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }
    }
}