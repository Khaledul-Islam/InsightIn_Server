using InsightIn_Note_DBContext.DBContext_EF;
using InsightIn_Note_Model.Note;
using InsightIn_Note_Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsightIn_Note_Repository.Repository
{
    public class NoteCategoryRepository : INoteCategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public NoteCategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<NoteCat> GetNoteCategories()
        {
            var response = _db.NoteCategories.ToList();
            _db.SaveChanges();
            return response;
        }

        public NoteCat GetNoteCategory(string NoteName)
        {
            var response = _db.NoteCategories.FirstOrDefault(a => a.Category_Name == NoteName);
            return response;
        }

        public bool isExist(int NoteID)
        {
            bool response = _db.NoteCategories.Any(a => a.ID == NoteID);
            _db.SaveChanges();
            return response;
        }

        public bool NoteCategoryDelete(int noteID)
        {
            var response = _db.NoteCategories.FirstOrDefault(a => a.ID == noteID);
            if(response != null)
            {
                _db.NoteCategories.Remove(response);
                _db.SaveChanges();
                return true;
            }
            return false;

        }

        public bool NoteCategorySave(NoteCat note)
        {
            _db.NoteCategories.Add(note);
            _db.SaveChanges();
            return true;
        }

        public bool NoteCategoryUpdate(NoteCat note)
        {
            _db.NoteCategories.Update(note);
            _db.SaveChanges();
            return true;
        }
    }
}
