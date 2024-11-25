using System;
using System.Collections.Generic;

namespace WILAPI.Models;

public partial class TblStaffLecture
{
    public string LectureId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string ModuleCode { get; set; } = null!;

    public string? ClassroomCode { get; set; }

    public DateOnly Date { get; set; }

    public TimeOnly? Start { get; set; }

    public TimeOnly? Finish { get; set; }

    public virtual TblModule ModuleCodeNavigation { get; set; } = null!;

    public virtual ICollection<TblStudentLecture> TblStudentLectures { get; set; } = new List<TblStudentLecture>();

    public virtual TblStaff User { get; set; } = null!;
}
