using ClosedXML.Excel;
using People_Specification.Api.Models;

namespace People_Specification.Api.Services;

public class EmployeeExportService : IEmployeeExportService
{
    public byte[] ExportToExcel(List<Employee> employees)
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Employees");

        worksheet.Cell(1, 1).Value = "Id";
        worksheet.Cell(1, 2).Value = "EmployeeNumber";
        worksheet.Cell(1, 3).Value = "FirstName";
        worksheet.Cell(1, 4).Value = "LastName";
        worksheet.Cell(1, 5).Value = "WorkEmail";
        worksheet.Cell(1, 6).Value = "PhoneNumber";
        worksheet.Cell(1, 7).Value = "Department";
        worksheet.Cell(1, 8).Value = "Position";
        worksheet.Cell(1, 9).Value = "HireDate";
        worksheet.Cell(1, 10).Value = "IsActive";

        for (int i = 0; i < employees.Count; i++)
        {
            var employee = employees[i];
            var row = i + 2;

            worksheet.Cell(row, 1).Value = employee.Id;
            worksheet.Cell(row, 2).Value = employee.EmployeeNumber;
            worksheet.Cell(row, 3).Value = employee.FirstName;
            worksheet.Cell(row, 4).Value = employee.LastName;
            worksheet.Cell(row, 5).Value = employee.WorkEmail;
            worksheet.Cell(row, 6).Value = employee.PhoneNumber;
            worksheet.Cell(row, 7).Value = employee.Department;
            worksheet.Cell(row, 8).Value = employee.Position;
            worksheet.Cell(row, 9).Value = employee.HireDate;
            worksheet.Cell(row, 10).Value = employee.IsActive;
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);

        return stream.ToArray();
    }


    public string ExportToHtml(List<Employee> employees)
{
    var html = """
        <!DOCTYPE html>
        <html>
        <head>
            <meta charset="UTF-8">
            <title>Employees</title>
            <style>
                table {
                    border-collapse: collapse;
                    width: 100%;
                }

                th, td {
                    border: 1px solid black;
                    padding: 8px;
                    text-align: left;
                }

                th {
                    font-weight: bold;
                }
            </style>
        </head>
        <body>
            <h1>Employees</h1>
            <table>
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>EmployeeNumber</th>
                        <th>FirstName</th>
                        <th>LastName</th>
                        <th>WorkEmail</th>
                        <th>PhoneNumber</th>
                        <th>Department</th>
                        <th>Position</th>
                        <th>HireDate</th>
                        <th>IsActive</th>
                    </tr>
                </thead>
                <tbody>
        """;

    foreach (var employee in employees)
    {
        html += $"""
                    <tr>
                        <td>{employee.Id}</td>
                        <td>{employee.EmployeeNumber}</td>
                        <td>{employee.FirstName}</td>
                        <td>{employee.LastName}</td>
                        <td>{employee.WorkEmail}</td>
                        <td>{employee.PhoneNumber}</td>
                        <td>{employee.Department}</td>
                        <td>{employee.Position}</td>
                        <td>{employee.HireDate}</td>
                        <td>{employee.IsActive}</td>
                    </tr>
        """;
    }

    html += """
                </tbody>
            </table>
        </body>
        </html>
        """;

    return html;
}

}