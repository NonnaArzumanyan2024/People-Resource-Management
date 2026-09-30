using PeopleResourceManagement.Domain.Entities;

namespace PeopleResourceManagement.Application.Interfaces;

public interface IOrganizationTreeHtmlExportService
{
    string ExportToHtml(List<Unit> units);
}
