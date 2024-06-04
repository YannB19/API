using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class Hero
{
    public int Id { get; set; }

    public string? Nom { get; set; }

    public string? Description { get; set; }
}
