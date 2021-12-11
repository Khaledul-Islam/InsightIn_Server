using InsightIn_Note_Model.Note;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsightIn_Note_Repository.Contracts
{
    public interface INoteRepository
    {
        bool CreateNote(Note notes,out string errMsg);
        bool DeleteNote(int NoteID,out string errMsg);
        bool UpdateNote(Note notes,out string errMsg);
        List<Note> GetNotes();
        Note GetNotebyID(int NoteID,out string errMsg);
        Note GetNotebyName(string NoteTitle,out string errMsg);
        Note GetNotebyCategoryID(int catID,out string errMsg);
        bool IsExist(int NoteID,out string errMsg);
    }
}
