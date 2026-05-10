using evoNaplo.DTO;
using Microsoft.AspNetCore.Mvc;
using evoNaplo.Services;

namespace evoNaplo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {
        private readonly IExcelImportService _excelImportService;
        private readonly IExcelExportService _excelExportService;
        private readonly ICsvImportService _csvImportService;
        private readonly ICsvExportService _csvExportService;
        
        private static List<ImportData> _temporarySheet = new();

        public DataController(IExcelImportService excelImportService, IExcelExportService excelExportService, ICsvImportService csvImportService, ICsvExportService csvExportService)
        {
            _excelImportService = excelImportService;
            _excelExportService = excelExportService;
            _csvImportService = csvImportService;
            _csvExportService = csvExportService;
        }
        
        [HttpPost("import")]
        public IActionResult ImportExcel(IFormFile file)
        {
            if (file is null || file.Length == 0)
            {
                return BadRequest("No file provided.");
            }

            if (!file.FileName.EndsWith(".xlsx") && (!file.FileName.EndsWith(".csv")))
            {
                return BadRequest("Only .xlsx and .csv file formats are supported.");
            }
            
            if (file.FileName.EndsWith(".csv"))
            {
                var dataList = _csvImportService.ProcessCsvFile(file);
                _temporarySheet = dataList; 
                return Ok(dataList);
            }
            else
            {
                var dataList = _excelImportService.ProcessExcelFile(file);
                _temporarySheet = dataList; 
                return Ok(dataList);
            }
        }

        [HttpGet("export")]
        public IActionResult ExportExcel([FromQuery] ExportData filter)
        {
            if (_temporarySheet is null || !_temporarySheet.Any())
            {
                return BadRequest("No file found. Import a spreadsheet first (.xlsx or .csv).");
            }
            // Queries
            IEnumerable<ImportData> query = _temporarySheet;
            if (!string.IsNullOrWhiteSpace(filter.FilterTimestamp))
            {
                query = query.Where(student => student.Timestamp.ToString("yyyy.MM.dd HH:mm:ss").Contains(filter.FilterTimestamp));
            }

            if (!string.IsNullOrWhiteSpace(filter.FilterName))
            {
                query = query.Where(student => (student.Name ?? "").ToLower().Contains(filter.FilterName.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filter.FilterEmail))
            {
                query = query.Where(student => (student.Email ?? "").ToLower().Contains(filter.FilterEmail.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filter.FilterPhoneNumber))
            {
                query = query.Where(student => (student.PhoneNumber ?? "").ToLower().Contains(filter.FilterPhoneNumber.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filter.FilterMajor))
            {
                query = query.Where(student => (student.Major ?? "").ToLower().Contains(filter.FilterMajor.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filter.FilterIsFirstTime))
            {
                query = query.Where(student => (student.IsFirstTime ?? "").ToLower().Contains(filter.FilterIsFirstTime.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filter.filterGoals))
            {
                query = query.Where(student => (student.Goals ?? "").ToLower().Contains(filter.filterGoals.ToLower()));
            }

            
            if (!string.IsNullOrWhiteSpace(filter.FilterStayInTeam))
            {
                query = query.Where(student => (student.StayInTeam ?? "").ToLower().Contains(filter.FilterStayInTeam.ToLower()));
            } 

            if (!string.IsNullOrWhiteSpace(filter.FilterOtherComments))
            {
                query = query.Where(student => (student.OtherComments ?? "").ToLower().Contains(filter.FilterOtherComments.ToLower()));
            }
            var finalData = filter.RowCount > 0 ? query.Take(filter.RowCount.Value) : query;
            var date = DateTime.Now.ToString("yyyyMMdd-HHmmss");
            
            if (filter.Format == ExportFormat.xlsx)
            {
                var fileBytes = _excelExportService.CreateFile(finalData, filter); 
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                return File(fileBytes, contentType, $"evoNaplo-Export-{date}.xlsx");
            }
            else
            {
                var fileBytes = _csvExportService.CreateFile(finalData, filter);
                return File(fileBytes, "text/csv", $"evoNaplo-Export-{date}.csv");
            }
            
        }
    }
}