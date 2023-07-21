using System;
using System.Collections.Generic;

namespace Company.Models;

public partial class Empoyee
{
    public decimal Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string SurName { get; set; } = null!;

    public string? Patronymic { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string? DocSeries { get; set; }

    public string? DocNumber { get; set; }

    public string Position { get; set; } = null!;

    public Guid DepartmentId { get; set; }

    public virtual Department Department { get; set; } = null!;
}
