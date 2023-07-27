using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Company.Models;

public partial class Empoyee
{
    public decimal Id { get; set; }
    [Display(Name = "Имя")]
    public string FirstName { get; set; } = null!;
    [Display(Name = "Фамилия")]
    public string SurName { get; set; } = null!;
    [Display(Name = "Отчество")]
    public string? Patronymic { get; set; }
    [Display(Name = "Дата рождения")]
    public DateTime DateOfBirth { get; set; }
    [Display(Name = "Серия документа")]
    public string? DocSeries { get; set; }
    [Display(Name = "Номер документа")]
    public string? DocNumber { get; set; }
    [Display(Name = "Должность")]
    public string Position { get; set; } = null!;

    public Guid DepartmentId { get; set; }
    [Display(Name = "Отдел")]
    public virtual Department Department { get; set; } = null!;
}
