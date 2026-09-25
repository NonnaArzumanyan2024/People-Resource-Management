using People_Specification.Api.Models;

namespace People_Specification.Api.Services;

public interface IOrganizationTreeHtmlExportService
{
    string ExportToHtml(List<Unit> units);
}