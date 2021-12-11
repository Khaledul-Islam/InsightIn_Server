using InsightIn_Note_DBContext.DBContext_EF;
using InsightIn_Note_Model.Note;
using InsightIn_Note_Repository.Contracts;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsightIn_Note_Repository.Repository
{
    public class NoteRepository : INoteRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IEmailSender _emailSender;

        public NoteRepository(ApplicationDbContext db, IEmailSender emailSender)
        {
            _db = db;
            _emailSender = emailSender;
        }

        public bool CreateNote(Note notes, out string errMsg)
        {
            errMsg = string.Empty;
            if (string.IsNullOrEmpty(notes.NoteTitle))
            {
                errMsg = "Invalid Request";
            }
            notes.CreateDate = DateTime.Now;
            _db.Notes.Add(notes);
            if (notes.Reminder > DateTime.Now)
            {
                System.Text.StringBuilder sb = new();
                sb.Append("<h3>MR. " + notes.UserName + ",</h3>");
                sb.Append("<div>");
                sb.Append("<p> This notification is for your Note Management Task.");
                sb.Append("You just have created a scheduled task Title:</p>");
                sb.Append("<h2>" + notes.NoteTitle + "</h2>");
                sb.Append("<p>" + "Thank you," + "</p>");
                sb.Append("<p>" + "InsightIn Technology Dev Team" + "</p>");
                var sbbody = sb.ToString();
                _emailSender.SendEmailAsync(notes.Email, "Reminder....!!!!", sbbody);
            }

            _db.SaveChanges();
            return true;
        }

        public bool DeleteNote(int NoteID, out string errMsg)
        {
            errMsg = string.Empty;
            var response = _db.Notes.FirstOrDefault(a => a.NoteID == NoteID);
            if (response == null)
            {
                errMsg = "No Item to delete.";
                return false;
            }
            _db.Notes.Remove(response);
            _db.SaveChanges();
            return true;
        }

        public Note GetNotebyCategoryID(int catID, out string errMsg)
        {
            errMsg = string.Empty;
            var response = _db.Notes.FirstOrDefault(a => a.NoteCategoryID == catID);
            if (response == null)
            {
                errMsg = "Nothing found";
                return new Note();
            }
            return response;
        }

        public Note GetNotebyID(int NoteID, out string errMsg)
        {
            errMsg = string.Empty;
            var response = _db.Notes.FirstOrDefault(a => a.NoteID == NoteID);
            if (response == null)
            {
                errMsg = "Nothing found";
                return new Note();
            }
            return response;
        }

        public Note GetNotebyName(string NoteTitle, out string errMsg)
        {
            errMsg = string.Empty;
            var response = _db.Notes.FirstOrDefault(a => a.NoteTitle == NoteTitle);
            if (response == null)
            {
                errMsg = "Nothing found";
                return new Note();
            }
            return response;
        }

        public List<Note> GetNotes()
        {
            var response = _db.Notes.Include(a => a.NoteCategory).ToList();
            return response;
        }

        public bool IsExist(int NoteID, out string errMsg)
        {
            errMsg = string.Empty;
            var response = _db.Notes.Any(a => a.NoteID == NoteID);
            if (response == true)
            {
                errMsg = "Existing Note Available";
                return true;
            }
            return response;
        }

        public bool UpdateNote(Note notes, out string errMsg)
        {
            errMsg = string.Empty;
            bool exist = IsExist(notes.NoteID, out errMsg);
            if (exist)
            {
                _db.Notes.Update(notes);
                _db.SaveChanges();
                return true;
            }
            return exist;
        }
    }
}
