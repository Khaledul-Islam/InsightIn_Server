using InsightIn_Note_Model.Note;
using InsightIn_Note_Repository.Contracts;
using InsightIn_Note_Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsightIn_Note_Service.Service
{
    public class NoteCategoryService : INoteCategoryService
    {
        private readonly INoteCategoryRepository _repo;

        public NoteCategoryService(INoteCategoryRepository repo)
        {
            _repo = repo;
        }

        public List<NoteCat> GetNoteCategories()
        {
            return _repo.GetNoteCategories();
        }

        public NoteCat GetNoteCategory(string NoteName)
        {
            return _repo.GetNoteCategory(NoteName);
        }

        public bool isExist(int NoteID)
        {
            return _repo.isExist(NoteID);
        }

        public bool NoteCategoryDelete(int noteID)
        {
            return _repo.NoteCategoryDelete(noteID);
        }

        public bool NoteCategorySave(NoteCat note)
        {
            if (note == null || string.IsNullOrEmpty(note.Category_Name))
            {
                return false;
            }
            var exist = _repo.GetNoteCategory(note.Category_Name);
            if (exist!=null)
            {
                return false;
            }
            return _repo.NoteCategorySave(note);
        }

        public bool NoteCategoryUpdate(NoteCat note)
        {
            if (note == null || string.IsNullOrEmpty(note.Category_Name))
            {
                return false;
            }
            var exist = _repo.isExist(note.ID);
            if (exist == false)
            {
                return false;
            }
            return _repo.NoteCategoryUpdate(note);
        }
    }
}
