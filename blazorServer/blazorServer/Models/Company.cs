using System;
using System.Collections.Generic;

namespace blazorServer.Models;

public partial class Company
{
    public int IdCompany { get; set; }

    public string Name { get; set; } = null!;

    public string Inn { get; set; } = null!;

    public string LegalAddress { get; set; } = null!;

    public string ActualAddress { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; } = new List<Employee>();
}
