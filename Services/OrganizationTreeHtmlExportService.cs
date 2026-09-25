using System.Net;
using System.Text;
using People_Specification.Api.Models;

namespace People_Specification.Api.Services;

public class OrganizationTreeHtmlExportService
    : IOrganizationTreeHtmlExportService
{
    public string ExportToHtml(List<Unit> units)
    {
        var rootUnits = units
            .Where(unit => unit.ParentUnitId == null)
            .ToList();

        var html = new StringBuilder();

        html.AppendLine("<!DOCTYPE html>");
        html.AppendLine("<html>");
        html.AppendLine("<head>");
        html.AppendLine("    <meta charset=\"UTF-8\">");
        html.AppendLine("    <title>Organization Structure</title>");
        html.AppendLine("</head>");
        html.AppendLine("<body>");

        html.AppendLine("    <h1>Organization Structure</h1>");
        html.AppendLine("<ul>");

        foreach (var rootUnit in rootUnits)
        {
            BuildHtmlTree(html, rootUnit, units);
        }

        html.AppendLine("</ul>");
        html.AppendLine("</body>");
        html.AppendLine("</html>");

        return html.ToString();
    }

    private void BuildHtmlTree(
        StringBuilder html,
        Unit unit,
        List<Unit> allUnits)
    {
        html.AppendLine("<li>");

        html.AppendLine(
            $"<strong>{WebUtility.HtmlEncode(unit.Name)}</strong>");

        foreach (var employee in unit.Employees)
        {
            html.AppendLine("<div>");

            html.AppendLine(
                $"Employee: {WebUtility.HtmlEncode(employee.FirstName)} " +
                $"{WebUtility.HtmlEncode(employee.LastName)}<br>");

            html.AppendLine(
                $"Employee Number: {WebUtility.HtmlEncode(employee.EmployeeNumber)}<br>");

            html.AppendLine(
                $"Email: {WebUtility.HtmlEncode(employee.Email)}<br>");

            html.AppendLine(
                $"Phone: {WebUtility.HtmlEncode(employee.PhoneNumber)}<br>");

            html.AppendLine(
                $"Department: {WebUtility.HtmlEncode(employee.Department)}<br>");

            html.AppendLine(
                $"Position: {WebUtility.HtmlEncode(employee.Position)}<br>");

            html.AppendLine(
                $"Hire Date: {employee.HireDate:dd.MM.yyyy}<br>");

            html.AppendLine(
                $"Status: {(employee.IsActive ? "Active" : "Inactive")}");

            html.AppendLine("</div>");
        }

        var children = allUnits
            .Where(child => child.ParentUnitId == unit.Id)
            .ToList();

        if (children.Count > 0)
        {
            html.AppendLine("<ul>");

            foreach (var child in children)
            {
                BuildHtmlTree(html, child, allUnits);
            }

            html.AppendLine("</ul>");
        }

        html.AppendLine("</li>");
    }
}