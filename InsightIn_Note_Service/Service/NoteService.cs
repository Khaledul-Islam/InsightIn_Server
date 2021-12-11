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
    public class NoteService : INoteService
    {
        private readonly INoteRepository _repo;

        public NoteService(INoteRepository repo)
        {
            _repo = repo;
        }

        public bool CreateNote(Note notes,out string errMsg)
        {
            return _repo.CreateNote(notes,out errMsg);
        }

        public bool DeleteNote(int NoteID,out string errMsg)
        {
            return _repo.DeleteNote(NoteID,out errMsg);
        }

        public Note GetNotebyCategoryID(int catID, out string errMsg)
        {
            return _repo.GetNotebyCategoryID(catID, out errMsg);
        }

        public Note GetNotebyID(int NoteID,out string errMsg)
        {
            return _repo.GetNotebyID(NoteID,out errMsg);
        }

        public Note GetNotebyName(string NoteTitle,out string errMsg)
        {
            return _repo.GetNotebyName(NoteTitle,out errMsg);
        }

        public List<Note> GetNotes()
        {
            return _repo.GetNotes();
        }

        public bool IsExist(int NoteID,out string errMsg)
        {
            return _repo.IsExist(NoteID,out errMsg);
        }

        public bool UpdateNote(Note notes,out string errMsg)
        {
            return _repo.UpdateNote(notes,out errMsg);
        }
    }
}
