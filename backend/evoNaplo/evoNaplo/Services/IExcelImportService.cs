using Microsoft.AspNetCore.Http;
using evoNaplo.DTO;

namespace evoNaplo.Services;

public interface IExcelImportService
{
    List<EvoCampusApplication> ProcessExcelFile(IFormFile file);
}