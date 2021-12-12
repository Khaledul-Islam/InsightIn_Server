using InsightIn_Note_Model.Note;
using InsightIn_Note_Service.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsightIn_Server.Controllers.Notes
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _service;

        public NotesController(INoteService service)
        {
            _service = service;
        }
        [HttpPost]
        public IActionResult CreateNote(Note notes)
        {
            if (notes == null)
            {
                return BadRequest("Invalid Request");
            }
            var response = _service.CreateNote(notes, out string errMsg);
            if (!string.IsNullOrEmpty(errMsg))
            {
                return BadRequest(errMsg);
            }
            return Ok(response);
            //return CreatedAtRoute(notes, response);
        }

        [HttpPost]
        public IActionResult UpdateNote(Note notes)
        {
            if (notes == null)
            {
                return BadRequest("Invalid Request");
            }
            var response = _service.UpdateNote(notes, out string errMsg);
            return NoContent();
            //return CreatedAtRoute(notes, response);
        }
        [HttpDelete]
        public IActionResult DeleteNote(int NoteID)
        {
            var response = _service.DeleteNote(NoteID, out string errMsg);
            if (!string.IsNullOrEmpty(errMsg))
            {
                return BadRequest(errMsg);
            }
            return Ok(response);
        }

        [HttpGet]
        public IActionResult GetNotebyCategoryID(int catID)
        {
            var response = _service.GetNotebyCategoryID(catID, out string errMsg);
            if (response == null)
            {
                return NotFound();
            }
            if (!string.IsNullOrEmpty(errMsg))
            {
                return BadRequest(errMsg);
            }
            return Ok(response);
        }
        
        [HttpGet]
        public IActionResult GetNotebyID(int NoteID)
        {
            var response = _service.GetNotebyID(NoteID, out string errMsg);
            if (response == null)
            {
                return NotFound();
            }
            if (!string.IsNullOrEmpty(errMsg))
            {
                return BadRequest(errMsg);
            }
            return Ok(response);
        }
        
        [HttpGet]
        public IActionResult GetNotebyName(string NoteTitle)
        {
            var response = _service.GetNotebyName(NoteTitle, out string errMsg);
            if (response == null)
            {
                return NotFound();
            }
            if (!string.IsNullOrEmpty(errMsg))
            {
                return BadRequest(errMsg);
            }
            return Ok(response);
        }
        [HttpGet]
        public IActionResult GetNotes()
        {
            var response = _service.GetNotes();
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }
    }
}
