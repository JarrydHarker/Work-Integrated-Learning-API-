using System.Text.Json.Serialization;
//using WIL_API_Library.Models;

namespace WILAPI.Models
{
    public class Attendance
    {
        public string lectureID { get; set; }
        public string UserID { get; set; }
        public string classroomCode { get; set; }
        public string moduleCode { get; set; }
        public DateOnly? Date { get; set; }
        public TimeOnly? Time { get; set; }

        [JsonConstructor]
        public Attendance(string lectureID, string UserID, string classroomCode, string moduleCode)
        {
            this.lectureID = lectureID;
            this.UserID = UserID;
            this.classroomCode = classroomCode;
            this.moduleCode = moduleCode;
            Time = TimeOnly.FromDateTime(DateTime.Now);
            Date = DateOnly.FromDateTime(DateTime.Now);
        }

        public static Attendance FromLecture(TblLecture lecture)
        {
            Attendance attendance = new Attendance(lecture.LectureId, lecture.UserID, lecture.ClassroomCode, lecture.ModuleCode);

            attendance.Date = lecture.LectureDate;

            return attendance;
        }

        public Attendance(AttendanceRequest attendanceRequest)
        {
            lectureID = attendanceRequest.lectureID;
            UserID = attendanceRequest.UserID;
            classroomCode = attendanceRequest.classroomCode;
            moduleCode = attendanceRequest.moduleCode;
            Time = TimeOnly.FromDateTime(DateTime.Now);
            Date = DateOnly.FromDateTime(DateTime.Now);
        }
    }

    public class AttendanceRequest
    {
        public string lectureID {  get; set; }
        public string UserID { get; set; }
        public string classroomCode { get; set; }
        public string moduleCode { get; set; }

        public AttendanceRequest(string lectureID, string UserID, string classroomCode, string moduleCode)
        {
            this.lectureID = lectureID;
            this.UserID = UserID;
            this.classroomCode = classroomCode;
            this.moduleCode = moduleCode;
        }
    }
}
