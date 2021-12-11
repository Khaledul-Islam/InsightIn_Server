using InsightIn_Note_Model.Note;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsightIn_Note_Service.Contracts
{
    public interface IReportService
    {
        List<Note> TodaysNotes();
        List<Note> WeeklyNotes();
        List<Note> MonthlyNotes();
    }
}
