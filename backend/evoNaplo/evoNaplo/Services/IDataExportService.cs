using evoNaplo.DTO;

namespace evoNaplo.Services;

public interface IDataExportService<T>
{
    byte[] CreateFile(IEnumerable<T> data, ExportData filter);
}