using PeopleResourceManagement.Domain.Entities;

namespace PeopleResourceManagement.Application.Interfaces;

public interface IOrganizationTreeExcelExportService
{
    byte[] ExportToExcel(List<Unit> units);
}
