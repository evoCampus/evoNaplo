using Microsoft.AspNetCore.Http;
using evoNaplo.Models;

namespace evoNaplo.Services;

public interface IExcelImportService
{
    List<EvoCampusApplication> ProcessExcelFile(IFormFile file);
}