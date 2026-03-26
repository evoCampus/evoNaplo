using Microsoft.AspNetCore.Mvc;
using evoNaplo.Services;

namespace evoNaplo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExcelController : ControllerBase
    {
        private readonly IExcelImportService _excelImportService;

        public ExcelController(IExcelImportService excelImportService)
        {
            _excelImportService = excelImportService;
        }
        
        [HttpPost("import")]
        public IActionResult ImportExcel(IFormFile file)
        {
            if (file is null || file.Length == 0)
            {
                return BadRequest("No file provided.");
            }

            if (!file.FileName.EndsWith(".xlsx") || !file.FileName.EndsWith(".csv"))
            {
                return BadRequest("Only .xlsx and .cvs file formats are supported.");
            }
            
            var dataList = _excelImportService.ProcessExcelFile(file);
            
            return Ok(dataList);
        }
    }
}