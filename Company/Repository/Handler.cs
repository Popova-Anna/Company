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
            sb.AppendLine("<li>"+company.Name+"</li>");
            if (company.InverseParentDepartment.Count > 0)
            {
                sb.AppendLine("<ul>");
                foreach (var item in company.InverseParentDepartment)
                {
                    sb.AppendLine(ParentStruct(item));
                }
                sb.AppendLine("</ul>");
            }
            sb.AppendLine("</ul>");
            return sb.ToString();
        }
    }
}
