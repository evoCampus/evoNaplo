using evoNaplo.DTO;
using Microsoft.AspNetCore.Mvc;
using evoNaplo.Services;
using System;

namespace evoNaplo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExcelController : ControllerBase
    {
        private readonly IExcelImportService _excelImportService;
        private readonly IExcelExportService _excelExportService;
        
        private static List<EvoCampusApplication> _temporarySheet = new();

        public ExcelController(IExcelImportService excelImportService, IExcelExportService excelExportService)
        {
            _excelImportService = excelImportService;
            _excelExportService = excelExportService;
        }
        
        [HttpPost("import")]
        public IActionResult ImportExcel(IFormFile file)
        {
            if (file is null || file.Length == 0)
            {
                return BadRequest("No file provided.");
            }

            if (!file.FileName.EndsWith(".xlsx"))
            {
                return BadRequest("Only .xlsx and .csv file formats are supported.");
            }
            
            var dataList = _excelImportService.ProcessExcelFile(file);
            
            _temporarySheet = dataList; 
            
            return Ok(dataList);
        }

        [HttpGet("export")]
        public IActionResult ExportExcel([FromQuery] ExportData filter)
        {
            if (_temporarySheet is null || !_temporarySheet.Any())
            {
                return BadRequest("No file found. Import a spreadsheet first.");
            }
            // Queries
            IEnumerable<EvoCampusApplication> query = _temporarySheet;
            if (!string.IsNullOrWhiteSpace(filter.filterTimestamp))
            {
                query = query.Where(ts => (ts.Timestamp ?? "").ToLower().Contains(filter.filterTimestamp.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filter.filterName))
            {
                query = query.Where(nm => (nm.Name ?? "").ToLower().Contains(filter.filterName.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filter.filterEmail))
            {
                query = query.Where(em => (em.Email ?? "").ToLower().Contains(filter.filterEmail.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filter.filterPhoneNumber))
            {
                query = query.Where(pn => (pn.PhoneNumber ?? "").ToLower().Contains(filter.filterPhoneNumber.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filter.filterMajor))
            {
                query = query.Where(mj => (mj.Major ?? "").ToLower().Contains(filter.filterMajor.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filter.filterIsFirstTime))
            {
                query = query.Where(ft => (ft.IsFirstTime ?? "").ToLower().Contains(filter.filterIsFirstTime.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(filter.filterGoals))
            {
                query = query.Where(gs => (gs.Goals ?? "").ToLower().Contains(filter.filterGoals.ToLower()));
            }

            /*
            if (!string.IsNullOrWhiteSpace(filter.filterStayInTeam))
            {
                query = query.Where(st => (st.StayInTeam ?? "").ToLower().Contains(filter.filterStayInTeam.ToLower()));
            } 
            */

            if (!string.IsNullOrWhiteSpace(filter.filterOtherComments))
            {
                query = query.Where(cm => (cm.OtherComments ?? "").ToLower().Contains(filter.filterOtherComments.ToLower()));
            }
            var finalData = filter.rowCount > 0 ? query.Take(filter.rowCount.Value) : query;
            
            var fileBytes = _excelExportService.CreateExcelFile(finalData, filter); 
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var date = DateTime.Now.ToString("yyyyMMdd-HHmmss");
            return File(fileBytes, contentType, $"evoNaplo-Export-{date}.xlsx");
        }
    }
}