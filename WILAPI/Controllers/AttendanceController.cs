using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections;
using System.Text;
using WILAPI.Models;


namespace AttendanceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        DbWilContext context = new DbWilContext();

        [HttpPost]
        public string PostAttendance([FromBody] AttendanceRequest request)
        {
            Random random = new Random();

            Attendance attendance = new Attendance(request);



            var lecture = context.TblLectures.Where(x => x.UserID == attendance.UserID && x.LectureDate == attendance.Date).FirstOrDefault();

            if (lecture != null)
            {
                lecture.ScanOut = attendance.Time;

                return "Success";
            }
            else
            {
                if (!attendance.classroomCode.IsNullOrEmpty() && !attendance.UserID.IsNullOrEmpty() && !attendance.moduleCode.IsNullOrEmpty())
                {
                    context.TblLectures.Add(new TblLecture()
                    {
                        LectureId = random.Next(100).ToString(),
                        UserID = attendance.UserID,
                        ClassroomCode = attendance.classroomCode,
                        LectureDate = (DateOnly)attendance.Date,
                        ScanIn = (TimeOnly)attendance.Time,
                        ModuleCode = attendance.moduleCode,
                    });

                    context.SaveChanges();
                    return "Success";
                }
                else
                {
                    return "Error: Null values in request";
                }

                
                //"Server=localhost;Database=dbWIL;Trusted_Connection=True;TrustServerCertificate=true;");
            }
        }

        [HttpGet]
        public List<string> getModules()
        {
            List<string> Output = new List<string>();

            var modules = context.TblModules.Where(x => x != null).Select(x => x.ModuleCode).ToList();

            if (modules != null)
            {
                Output.AddRange(modules);
            }

            return Output;
        }
    }
}

