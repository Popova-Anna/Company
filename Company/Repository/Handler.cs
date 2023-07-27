using Company.Models;
using System.Text;

namespace Company.Repository
{
    public static class Handler
    {
        public static string ParentStruct(Department company)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<ul>");
            sb.AppendLine("<li class=\"li-default\">" + company.Name+"</li>");
            if (company.InverseParentDepartment.Count > 0)
            {
                sb.AppendLine("<ul>");
                foreach (var item in company.InverseParentDepartment)
                {
                    sb.AppendLine("<a href=\"/Departments/Employee/"+item.Id +"\">" + ParentStruct(item) + "</a>");
                }
                sb.AppendLine("</ul>");
            }
            sb.AppendLine("</ul>");
            return sb.ToString();
        }
    }
}
