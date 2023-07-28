using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Company.Models;

public partial class Department
{
    public Guid Id { get; set; }
    [Display(Name = "Название")]
    [Required(ErrorMessage = "Поле <b>Название</b> должно быть заполнено!")]
    [MaxLength(50, ErrorMessage = "Длина поля <b>Название</b> не должна превышать больше 50 символов!")]
    public string Name { get; set; } = null!;
    [Display(Name = "Код")]
    [MaxLength(10)]
    public string? Code { get; set; }

    public Guid? ParentDepartmentId { get; set; }
    [Display(Name = "Сотрудники")]
    public virtual ICollection<Empoyee> Empoyees { get; set; } = new List<Empoyee>();
    [Display(Name = "Подотделы")]
    public virtual ICollection<Department> InverseParentDepartment { get; set; } = new List<Department>();
    [Display(Name = "Главный отдел")]
    public virtual Department? ParentDepartment { get; set; }
}
