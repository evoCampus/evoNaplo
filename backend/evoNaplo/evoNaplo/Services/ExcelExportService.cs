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
                worksheet.Cell(1, headerCol++).Value = HeaderTimestamp;
                worksheet.Cell(1, headerCol++).Value = HeaderName;
                worksheet.Cell(1, headerCol++).Value = HeaderEmail;
                worksheet.Cell(1, headerCol++).Value = HeaderPhoneNumber;
                worksheet.Cell(1, headerCol++).Value = HeaderMajor;
                worksheet.Cell(1, headerCol++).Value = HeaderIsFirstTime;
                worksheet.Cell(1, headerCol++).Value = HeaderGoals;
                worksheet.Cell(1, headerCol++).Value = HeaderStayInTeam;
                worksheet.Cell(1, headerCol++).Value = HeaderOtherComments;
                
                int currentRow = 2;
                foreach (var item in data)
                {
                    int dataCol = 1;
                    string amPm = item.Timestamp.Hour < 12 ? "de." : "du.";
                    string formattedDate = item.Timestamp.ToString("yyyy/MM/dd HH:mm:ss ") + amPm + " CET";
        
                    worksheet.Cell(currentRow, dataCol++).Value = formattedDate; 
                    worksheet.Cell(currentRow, dataCol++).Value = item.Name;
                    worksheet.Cell(currentRow, dataCol++).Value = item.Email;
                    worksheet.Cell(currentRow, dataCol++).Value = item.PhoneNumber;
                    worksheet.Cell(currentRow, dataCol++).Value = item.Major;
                    worksheet.Cell(currentRow, dataCol++).Value = item.IsFirstTime;
                    worksheet.Cell(currentRow, dataCol++).Value = item.Goals;
                    worksheet.Cell(currentRow, dataCol++).Value = item.StayInTeam;
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