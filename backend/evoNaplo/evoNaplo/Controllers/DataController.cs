using evoNaplo.DTO;
using Microsoft.AspNetCore.Mvc;
using evoNaplo.Services;
using evoNaplo.Models;
using evoNaplo.DTO.StudentDTOs;

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

        private const string XlsxExtension = $".{nameof(ExportFormat.xlsx)}";
        private const string CsvExtension = $".{nameof(ExportFormat.csv)}";

        private static List<ImportData> _temporarySheet = new();

        public DataController(
            IExcelImportService excelImportService,
            IExcelExportService excelExportService,
            ICsvImportService csvImportService,
            ICsvExportService csvExportService)
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

            if (!file.FileName.EndsWith(XlsxExtension) && !file.FileName.EndsWith(CsvExtension))
            {
                return BadRequest($"Only {XlsxExtension} and {CsvExtension} file formats are supported.");
            }

            if (file.FileName.EndsWith(CsvExtension))
            {
                var csvDataList = _csvImportService.ProcessCsvFile(file);
                _temporarySheet = csvDataList;
                return Ok(csvDataList);
            }

            var excelDataList = _excelImportService.ProcessExcelFile(file);
            _temporarySheet = excelDataList;

            return Ok(excelDataList);
        }

        [HttpGet("export")]
        public IActionResult ExportExcel([FromQuery] ExportData filter)
        {

            IEnumerable<ImportData> query = _temporarySheet;

            if (filter.FilterTimestamp is not null)
                query = query.Where(x => x.Timestamp.ToString("yyyy.MM.dd HH:mm:ss").Contains(filter.FilterTimestamp));

            if (filter.FilterName is not null)
                query = query.Where(x => x.Name == filter.FilterName);

            if (filter.FilterEmail is not null)
                query = query.Where(x => x.Email == filter.FilterEmail);

            if (filter.FilterPhoneNumber is not null)
                query = query.Where(x => x.PhoneNumber == filter.FilterPhoneNumber);

            if (filter.FilterMajor is not null)
                query = query.Where(x => x.Major == filter.FilterMajor);

            if (filter.FilterIsFirstTime is not null)
                query = query.Where(x => x.IsFirstTime == filter.FilterIsFirstTime);

            if (filter.FilterGoals is not null)
                query = query.Where(x => x.Goals == filter.FilterGoals);

            if (filter.FilterStayInTeam is not null)
                query = query.Where(x => x.StayInTeam == filter.FilterStayInTeam);

            if (filter.FilterOtherComments is not null)
                query = query.Where(x => x.OtherComments == filter.FilterOtherComments);

            var finalData = filter.RowCount > 0 ? query.Take(filter.RowCount.Value) : query;
            var date = DateTime.Now.ToString("yyyyMMdd-HHmmss");

            if (filter.Format == ExportFormat.xlsx)
            {
                var fileBytes = _excelExportService.CreateFile(finalData, filter);
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                return File(fileBytes, contentType, $"evoNaplo-Export-{date}{XlsxExtension}");
            }

            var csvBytes = _csvExportService.CreateFile(finalData, filter);
            return File(csvBytes, "text/csv", $"evoNaplo-Export-{date}{CsvExtension}");
        }
    }
}