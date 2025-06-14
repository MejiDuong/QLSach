using System;
using System.Collections.Generic;

namespace Test1.Models;

public partial class Sach
{
    public string MaSach { get; set; } = null!;

    public string? TenSach { get; set; }

    public string MaTg { get; set; } = null!;

    public int? NamXuatBan { get; set; }

    public int? SoTrang { get; set; }

    public virtual TacGium MaTgNavigation { get; set; } = null!;
}
