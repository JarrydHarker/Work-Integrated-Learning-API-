﻿using System;
using System.Collections.Generic;

namespace WILAPI.Models;

public partial class TblModule
{
    public string ModuleCode { get; set; } = null!;

    public string? ModuleName { get; set; }

    public virtual ICollection<TblLecture> TblLectures { get; set; } = new List<TblLecture>();
}
