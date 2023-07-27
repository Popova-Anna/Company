using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Company.Models;

public partial class Department
{
    public Guid Id { get; set; }
    [Display(Name = "Название")]
    public string Name { get; set; } = null!;
    [Display(Name = "Код")]
    public string? Code { get; set; }

    public Guid? ParentDepartmentId { get; set; }
    [Display(Name = "Сотрудники")]
    public virtual ICollection<Empoyee> Empoyees { get; set; } = new List<Empoyee>();
    [Display(Name = "Подотделы")]
    public virtual ICollection<Department> InverseParentDepartment { get; set; } = new List<Department>();
    [Display(Name = "Главный отдел")]
    public virtual Department? ParentDepartment { get; set; }
}
