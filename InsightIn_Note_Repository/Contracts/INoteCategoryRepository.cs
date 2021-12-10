using InsightIn_Note_Model.Note;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsightIn_Note_Repository.Contracts
{
   public interface INoteCategoryRepository
    {
        bool NoteCategorySave(NoteCat note);
        bool NoteCategoryUpdate(NoteCat note);
        bool NoteCategoryDelete(int noteID);
        List<NoteCat> GetNoteCategories();
        NoteCat GetNoteCategory(string NoteName);
        bool isExist(int NoteID);
    }
}
