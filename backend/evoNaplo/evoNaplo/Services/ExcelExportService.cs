using evoNaplo.DTO;
using ClosedXML.Excel;

namespace evoNaplo.Services
{
    public class ExcelExportService : IExcelExportService
    {
        public byte[] CreateExcelFile(IEnumerable<EvoCampusApplication> data, ExportData filter)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Exported Data");
                
                // Headers
                int headerCol = 1;
                if (filter.includeTimestamp) { worksheet.Cell(1, headerCol++).Value = "Időbélyeg"; }
                if (filter.includeName) { worksheet.Cell(1, headerCol++).Value = "Név"; }
                if (filter.includeEmail) { worksheet.Cell(1, headerCol++).Value = "E-mail cím"; }
                if (filter.includePhoneNumber) { worksheet.Cell(1, headerCol++).Value = "Telefonszám (pl: 70/1234567)"; }
                if (filter.includeMajor) { worksheet.Cell(1, headerCol++).Value = "Tanulmányi évfolyam és szak megnevezése (pl.: Miskolci Egyetem Gépészmérnöki és Informatikai Kar ... szak ...specializáció, 3. félév)"; }
                if (filter.includeIsFirstTime) { worksheet.Cell(1, headerCol++).Value = "Először veszel részt az evoCampus programban?"; }
                if (filter.includeGoals) { worksheet.Cell(1, headerCol++).Value = "Milyen személyes céljaid vannak az evoCampus 2026 tavaszi szemeszteri részvételével?"; }
                //if (filter.includeStayInTeam) { worksheet.Cell(1, headerCol++).Value = "A jelenlegi csapatban maradnál?"; }
                if (filter.includeOtherComments) { worksheet.Cell(1, headerCol++).Value = "Egyéb észrevétel, megjegyzés"; }
                
                // Data
                int currentRow = 2;
                foreach (var item in data)
                {
                    int dataCol = 1;
                    if (filter.includeTimestamp) { worksheet.Cell(currentRow, dataCol++).Value = item.Timestamp; }
                    if (filter.includeName) { worksheet.Cell(currentRow, dataCol++).Value = item.Name; }
                    if (filter.includeEmail) { worksheet.Cell(currentRow, dataCol++).Value = item.Email; }
                    if (filter.includePhoneNumber) { worksheet.Cell(currentRow, dataCol++).Value = item.PhoneNumber; }
                    if (filter.includeMajor) { worksheet.Cell(currentRow, dataCol++).Value = item.Major; }
                    if (filter.includeIsFirstTime) { worksheet.Cell(currentRow, dataCol++).Value = item.IsFirstTime; }
                    if (filter.includeGoals) { worksheet.Cell(currentRow, dataCol++).Value = item.Goals; }
                    //if (filter.includeStayInTeam) { worksheet.Cell(currentRow, dataCol++).Value = item.StayInTeam; }
                    if (filter.includeOtherComments) { worksheet.Cell(currentRow, dataCol++).Value = item.OtherComments; }
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