using evoNaplo.DTO;

namespace evoNaplo.Services;

public interface ICsvExportService
{
    byte[] CreateCsvFile(IEnumerable<EvoCampusApplication> data, ExportData filter);
}