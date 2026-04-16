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
        
        private static List<EvoCampusApplication> _temporarySheet = new();

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
                var dataList = _csvImportService.ProcessCvsFile(file);
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
            IEnumerable<EvoCampusApplication> query = _temporarySheet;
            if (!string.IsNullOrWhiteSpace(filter.filterTimestamp))
            {
                query = query.Where(student => (student.Timestamp ?? "").ToLower().Contains(filter.filterTimestamp.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filter.filterName))
            {
                query = query.Where(student => (student.Name ?? "").ToLower().Contains(filter.filterName.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filter.filterEmail))
            {
                query = query.Where(student => (student.Email ?? "").ToLower().Contains(filter.filterEmail.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filter.filterPhoneNumber))
            {
                query = query.Where(student => (student.PhoneNumber ?? "").ToLower().Contains(filter.filterPhoneNumber.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filter.filterMajor))
            {
                query = query.Where(student => (student.Major ?? "").ToLower().Contains(filter.filterMajor.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filter.filterIsFirstTime))
            {
                query = query.Where(student => (student.IsFirstTime ?? "").ToLower().Contains(filter.filterIsFirstTime.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filter.filterGoals))
            {
                query = query.Where(student => (student.Goals ?? "").ToLower().Contains(filter.filterGoals.ToLower()));
            }

            /*
            if (!string.IsNullOrWhiteSpace(filter.filterStayInTeam))
            {
                query = query.Where(student => (student.StayInTeam ?? "").ToLower().Contains(filter.filterStayInTeam.ToLower()));
            } 
            */

            if (!string.IsNullOrWhiteSpace(filter.filterOtherComments))
            {
                query = query.Where(student => (student.OtherComments ?? "").ToLower().Contains(filter.filterOtherComments.ToLower()));
            }
            var finalData = filter.rowCount > 0 ? query.Take(filter.rowCount.Value) : query;
            var date = DateTime.Now.ToString("yyyyMMdd-HHmmss");
            
            if (filter.Format == ExportFormat.xlsx)
            {
                var fileBytes = _excelExportService.CreateExcelFile(finalData, filter); 
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                return File(fileBytes, contentType, $"evoNaplo-Export-{date}.xlsx");
            }
            else
            {
                var fileBytes = _csvExportService.CreateCsvFile(finalData, filter);
                return File(fileBytes, "text/csv", $"evoNaplo-Export-{date}.csv");
            }
            
        }
    }
}