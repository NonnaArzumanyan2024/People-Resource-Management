using ClosedXML.Excel;
using People_Specification.Api.Models;

namespace People_Specification.Api.Services;

public class OrganizationTreeExcelExportService
    : IOrganizationTreeExcelExportService
{
    public byte[] ExportToExcel(List<Unit> units)
    {
        using var workbook = new XLWorkbook();

        var worksheet =
            workbook.Worksheets.Add("Organization Tree");

        worksheet.Cell(1, 1).Value = "Organization Tree";
        worksheet.Cell(1, 2).Value = "Employee Number";
        worksheet.Cell(1, 3).Value = "Position";
        worksheet.Cell(1, 4).Value = "Email";
        worksheet.Cell(1, 5).Value = "Phone";
        worksheet.Cell(1, 6).Value = "Hire Date";
        worksheet.Cell(1, 7).Value = "Status";

        using var stream = new MemoryStream();

        workbook.SaveAs(stream);

        return stream.ToArray();
    }

    private void BuildExcelTree(
    IXLWorksheet worksheet,
    Unit unit,
    List<Unit> allUnits,
    int level,
    ref int row)
    {
        var indent = new string(' ', level * 4);

        worksheet.Cell(row, 1).Value =
            $"{indent}↳ {unit.Name}";

        row++;

        foreach (var employee in unit.Employees)
        {
            var employeeIndent =
                new string(' ', (level + 1) * 4);

            worksheet.Cell(row, 1).Value =
                $"{employeeIndent}↳ {employee.FirstName} {employee.LastName}";

            worksheet.Cell(row, 2).Value =
                employee.EmployeeNumber;

            worksheet.Cell(row, 3).Value =
                employee.Position;

            worksheet.Cell(row, 4).Value =
                employee.Email;

            worksheet.Cell(row, 5).Value =
                employee.PhoneNumber;

            worksheet.Cell(row, 6).Value =
                employee.HireDate;

            worksheet.Cell(row, 7).Value =
                employee.IsActive ? "Active" : "Inactive";

            row++;
        }

        var children = allUnits
            .Where(child => child.ParentUnitId == unit.Id)
            .ToList();

        foreach (var child in children)
        {
            BuildExcelTree(
                worksheet,
                child,
                allUnits,
                level + 1,
                ref row);
        }
    }

}