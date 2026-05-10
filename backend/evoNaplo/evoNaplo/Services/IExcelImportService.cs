using Microsoft.AspNetCore.Http;
using evoNaplo.DTO;

namespace evoNaplo.Services;

public interface IExcelImportService
{
    List<ImportData> ProcessExcelFile(IFormFile file);
}