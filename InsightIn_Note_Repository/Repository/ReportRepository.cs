using Dapper;
using InsightIn_Note_DBContext.DBContext_EF;
using InsightIn_Note_Model.Note;
using InsightIn_Note_Repository.Contracts;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsightIn_Note_Repository.Repository
{
    public class ReportRepository : IReportRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IDbConnection _dapper;

        public ReportRepository(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            _dapper = new SqlConnection(configuration.GetConnectionString("DefaultConncetion"));
        }

        public List<Note> MonthlyNotes()
        {
            string query = @"SELECT NoteID,NoteCategoryID,NoteTitle,NoteDescription,Reminder,CreateDate,DueDate,IsCompleted ,nc.Category_Name
               FROM Notes LEFT JOIN NoteCategories nc ON  Notes.NoteCategoryID=nc.ID
             WHERE IsCompleted='0' AND (CONVERT(DATE,Reminder) between DATEADD(day,8,GETDATE()) and DATEADD(day,30,GETDATE())
             OR CONVERT(DATE,DueDate) between DATEADD(day,8,GETDATE()) and DATEADD(day,30,GETDATE()))";
            var response = _dapper.Query<Note>(query).ToList();
            return response;
        }

        public List<Note> TodaysNotes()
        {
            string query = @"SELECT NoteID,NoteCategoryID,NoteTitle,NoteDescription,Reminder,CreateDate,DueDate,IsCompleted ,nc.Category_Name 
                        FROM Notes LEFT JOIN NoteCategories nc ON  Notes.NoteCategoryID=nc.ID
                        WHERE IsCompleted='0' AND (CONVERT(DATE,Reminder)=CONVERT(Date,GETDATE()) 
                        OR CONVERT(DATE,DueDate)=CONVERT(Date,GETDATE()))";
            var response = _dapper.Query<Note>(query).ToList();
            return response;
        }

        public List<Note> WeeklyNotes()
        {
            string query = @"SELECT NoteID,NoteCategoryID,NoteTitle,NoteDescription,Reminder,CreateDate,DueDate,IsCompleted ,nc.Category_Name
                    FROM Notes LEFT JOIN NoteCategories nc ON  Notes.NoteCategoryID=nc.ID
                             WHERE IsCompleted='0' AND (CONVERT(DATE,Reminder) 
                    between DATEADD(day,1,GETDATE()) 
                        and DATEADD(day,7,GETDATE())
                    OR CONVERT(DATE,DueDate) 
                    between DATEADD(day,1,GETDATE()) 
                        and DATEADD(day,7,GETDATE()))";

            var response = _dapper.Query<Note>(query).ToList();
            return response;
        }
    }
}
