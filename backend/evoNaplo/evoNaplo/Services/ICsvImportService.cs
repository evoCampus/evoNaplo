using Microsoft.AspNetCore.Http;
using evoNaplo.DTO;

namespace evoNaplo.Services;

public interface ICsvImportService
{
    List<ImportData> ProcessCsvFile(IFormFile file);
}