using Company.Models;
using System.Text;

namespace Company.Repository
{
    public static class Handler
    {
        /// <summary>
        /// Метод для вывода структуры компании
        /// </summary>
        /// <param name="company">Отдел</param>
        /// <returns>Строка для вывода на страницу</returns>
        public static string ParentStruct(Department company)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<ul>");
            sb.AppendLine("<li class=\"li-default \"> <a class=\"link-default\" href =\"/Departments/Employee/" + company.Id + "\"><h3>" +  company.Name+ "</h3><p class=\"p-link \">" + company.Empoyees.Count+"</p></a></li>");
            if (company.InverseParentDepartment.Count > 0)
            {
                foreach (var item in company.InverseParentDepartment)
                {
                    sb.AppendLine(ParentStruct(item) );
                }
            }
            sb.AppendLine("</ul>");
            return sb.ToString();
        }
    }
}
