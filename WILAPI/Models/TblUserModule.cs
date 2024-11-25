using System;
using System.Collections.Generic;

namespace WILAPI.Models;

public partial class TblUserModule
{
    public string ModuleCode { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string? UserModuleId { get; set; }

    public virtual TblModule ModuleCodeNavigation { get; set; } = null!;

    public virtual TblUser User { get; set; } = null!;
}
