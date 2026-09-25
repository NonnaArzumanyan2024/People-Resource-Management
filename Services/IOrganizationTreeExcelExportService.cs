using People_Specification.Api.Models;

namespace People_Specification.Api.Services;

public interface IOrganizationTreeExcelExportService
{
    byte[] ExportToExcel(List<Unit> units);
}