using Microsoft.AspNetCore.Http;
using evoNaplo.DTO;

namespace evoNaplo.Services;

public interface ICsvImportService
{
    List<EvoCampusApplication> ProcessCvsFile(IFormFile file);
}