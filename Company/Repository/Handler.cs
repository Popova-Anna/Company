using Company.Models;
using System.Text;

namespace Company.Repository
{
    public static class Handler
    {
        public static string ParentStruct(Department company)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<ul class=\"tree\" >");
            sb.AppendLine("<li class=\"li-default\"> <a class=\"link-default\" href =\"/Departments/Employee/" + company.Id + "\"><h3>" +  company.Name+ "</h3></a></li>");
            if (company.InverseParentDepartment.Count > 0)
            {
                sb.AppendLine("<ul>");
                foreach (var item in company.InverseParentDepartment)
                {
                    sb.AppendLine(ParentStruct(item) );
                }
                sb.AppendLine("</ul>");
            }
            sb.AppendLine("</ul>");
            return sb.ToString();
        }
    }
}
