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
    public class ReportService : IReportService
    {
        private readonly IReportRepository _repo;

        public ReportService(IReportRepository repo)
        {
            _repo = repo;
        }

        public List<Note> MonthlyNotes()
        {
            return _repo.MonthlyNotes();
        }

        public List<Note> TodaysNotes()
        {
            return _repo.TodaysNotes();
        }

        public List<Note> WeeklyNotes()
        {
            return _repo.WeeklyNotes();
        }
    }
}
