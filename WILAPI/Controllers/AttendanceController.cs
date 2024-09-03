﻿using Microsoft.AspNetCore.Http;
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
        public string PostAttendance([FromBody] AttendanceRequest request)//Post method to get data from scanner
        {
            Attendance attendance = new Attendance(request);

            var lecture = context.TblLectures.Where(x => x.UserID == attendance.UserID && x.LectureDate == attendance.Date).FirstOrDefault();

            if (lecture != null)//Check if lecture is already in DB
            {
                lecture.ScanOut = attendance.Time;//Update scan out time

                return "Success";
            } else //If there is no preexisting lecture in DB add a new one
            {
                if (!attendance.classroomCode.IsNullOrEmpty() && !attendance.UserID.IsNullOrEmpty() && !attendance.moduleCode.IsNullOrEmpty())
                {
                    context.TblLectures.Add(new TblLecture()
                    {
                        LectureId = "L" + context.TblLectures.Count(),
                        UserID = attendance.UserID,
                        ClassroomCode = attendance.classroomCode,
                        LectureDate = (DateOnly)attendance.Date!,
                        ScanIn = (TimeOnly)attendance.Time!,
                        ModuleCode = attendance.moduleCode,
                    });

                    context.SaveChanges();
                    return "Success";
                } else
                {
                    return "Error: Null values in request";
                }
            }
        }

        [HttpGet]
        public List<string> getModules()//Get method to populate module options in scanner
        {
            List<string> Output = new List<string>();

            var modules = context.TblModules.Where(x => x != null).Select(x => x.ModuleCode).ToList();

            if (modules != null)//Null check
            {
                Output.AddRange(modules);
            } else
            {
                Output.Add("Error: No modules found");
            }

            return Output;
        }

        [HttpPost("Add")]
        public string AddStudent(string UserId, string StudentNo)
        {
            try
            {
                TblUser newUser = new TblUser
                {
                    UserId = UserId,
                    UserName = "New User"
                };

                TblStudent newStudent = new TblStudent
                {
                    UserId = newUser.UserId,
                    StudentNo = StudentNo
                };

                context.TblUsers.Add(newUser);
                context.TblStudents.Add(newStudent);
                newUser.TblStudent = newStudent;
                context.SaveChanges();

                return "Success";
            } catch (Exception e)
            {
                return e.ToString();
            }
        }
    }
}

