using evoNaplo.DTO;

namespace evoNaplo.Services;
public interface IExcelExportService
{
    byte[] CreateExcelFile(IEnumerable<EvoCampusApplication> data, ExportData filter);
}