using System.Text.Json.Serialization;
//using WIL_API_Library.Models;

namespace WILAPI.Models
{
    public class Attendance
    {
        public string UserID { get; set; }
        public string classroomCode { get; set; }
        public string moduleCode { get; set; }
        public DateOnly? Date { get; set; }
        public TimeOnly? Time { get; set; }

        [JsonConstructor]
        public Attendance(string UserID, string classroomCode, string moduleCode)
        {
            this.UserID = UserID;
            this.classroomCode = classroomCode;
            this.moduleCode = moduleCode;
            Time = TimeOnly.FromDateTime(DateTime.Now);
            Date = DateOnly.FromDateTime(DateTime.Now);
        }

        public static Attendance fromLecture(TblLecture lecture)
        {
            Attendance attendance = new Attendance(lecture.UserID, lecture.ClassroomCode, lecture.ModuleCode);

            attendance.Date = lecture.LectureDate;

            return attendance;
        }

        public Attendance(AttendanceRequest attendanceRequest)
        {
            UserID = attendanceRequest.UserID;
            classroomCode = attendanceRequest.classroomCode;
            moduleCode = attendanceRequest.moduleCode;
            Time = TimeOnly.FromDateTime(DateTime.Now);
            Date = DateOnly.FromDateTime(DateTime.Now);
        }
    }

    public class AttendanceRequest
    {
        public string UserID { get; set; }
        public string classroomCode { get; set; }
        public string moduleCode { get; set; }

        public AttendanceRequest(string UserID, string classroomCode, string moduleCode)
        {
            this.UserID = UserID;
            this.classroomCode = classroomCode;
            this.moduleCode = moduleCode;
        }
    }
}
