using PeopleResourceManagement.Domain.Entities;

namespace PeopleResourceManagement.Application.Interfaces;

public interface IEmployeeExportService
{
    byte[] ExportToExcel(List<Employee> employees);
    string ExportToHtml(List<Employee> employees);
}

