﻿using System;
using System.Collections.Generic;

namespace WILAPI.Models;

public partial class TblLecture
{
    public string LectureId { get; set; } = null!;

    public string UserID { get; set; } = null!;

    public DateOnly LectureDate { get; set; }

    public string ClassroomCode { get; set; } = null!;

    public TimeOnly ScanIn { get; set; }

    public TimeOnly? ScanOut { get; set; }

    public string ModuleCode { get; set; } = null!;

    public virtual TblModule ModuleCodeNavigation { get; set; } = null!;

    public virtual TblUser User { get; set; } = null!;
}
