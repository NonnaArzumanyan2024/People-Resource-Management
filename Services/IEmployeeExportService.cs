using People_Specification.Api.Models;

namespace People_Specification.Api.Services;

public interface IEmployeeExportService
{
    byte[] ExportToExcel(List<Employee> employees);
    string ExportToHtml(List<Employee> employees);
}