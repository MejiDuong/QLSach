using System;
using System.Collections.Generic;

namespace Test1.Models;

public partial class TacGium
{
    public string MaTg { get; set; } = null!;

    public string? TenTacGia { get; set; }

    public virtual ICollection<Sach> Saches { get; set; } = new List<Sach>();
}
