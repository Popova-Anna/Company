using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Company.Models;

public partial class Empoyee
{
    public decimal Id { get; set; }
    [Display(Name = "Имя")]
    [MaxLength(50, ErrorMessage = "Длина поля <b>Имя</b> не должна превышать больше 50 символов!")]
    [Required(ErrorMessage = "Поле <b>Имя</b> должно быть заполнено!")]
    public string FirstName { get; set; } = null!;
    [Display(Name = "Фамилия")]
    [Required(ErrorMessage = "Поле <b>Фамилия</b> должно быть заполнено!")]
    [MaxLength(50, ErrorMessage = "Длина поля <b>Фамилия</b> не должна превышать больше 50 символов!")]
    public string SurName { get; set; } = null!;
    [Display(Name = "Отчество")]
    [MaxLength(50, ErrorMessage = "Длина поля <b>Отчество</b> не должна превышать больше 50 символов!")]
    public string? Patronymic { get; set; }
    [Display(Name = "Дата рождения")]
    [Required(ErrorMessage = "Поле <b>Дата рождения</b> должно быть заполнено!")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
    public DateTime DateOfBirth { get; set; }
    [Display(Name = "Серия документа")]
    [MaxLength(4, ErrorMessage = "Длина поля <b>Серия документа</b> не должна превышать больше 4 символов!")]
    public string? DocSeries { get; set; }
    [Display(Name = "Номер документа")]
    [Range(0, int.MaxValue, ErrorMessage = "Поле Номер документа</b> должно состоять только цифры")]
    [MaxLength(6, ErrorMessage = "Длина поля <b>Номер документа</b> не должна превышать больше 6 символов!")]
    public string? DocNumber { get; set; }
    [Display(Name = "Должность")]
    [Required(ErrorMessage = "Поле <b>Должность</b> должно быть заполнено!")]
    [MaxLength(50, ErrorMessage = "Длина поля <b>Должность</b> не должна превышать больше 50 символов!")]
    public string Position { get; set; } = null!;
    [Display(Name = "Отдел")]
    [Required(ErrorMessage = "Поле <b>Отдел</b> должно быть заполнено!")]
    public Guid DepartmentId { get; set; }
    [Display(Name = "Отдел")]
    public virtual Department Department { get; set; } = null!;
}
