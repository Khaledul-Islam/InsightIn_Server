using InsightIn_Note_Model.Note;
using InsightIn_Note_Service.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsightIn_Server.Controllers.NoteCategory
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class NoteCategoryController : ControllerBase
    {
        private readonly INoteCategoryService _service;

        public NoteCategoryController(INoteCategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetNoteCategories()
        {
            var response = _service.GetNoteCategories();
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpGet]
        public IActionResult GetNoteCategory(string NoteName)
        {
            if (string.IsNullOrEmpty(NoteName))
            {
                return BadRequest();
            }
            var response = _service.GetNoteCategory(NoteName);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpDelete]
        public IActionResult NoteCategoryDelete(int noteID)
        {
            var response = _service.NoteCategoryDelete(noteID);
            if (response == false)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [HttpPost]
        public IActionResult NoteCategorySave(NoteCat note)
        {
            if (note == null)
            {
                return BadRequest();
            }
            var response = _service.NoteCategorySave(note);
            if (response == false)
            {
                return BadRequest();
            }
            return Ok(note);
        }
        [HttpPost]
        public IActionResult NoteCategoryUpdate(NoteCat note)
        {

            if (note == null)
            {
                return BadRequest();
            }
            var response = _service.NoteCategoryUpdate(note);
            if (response == false)
            {
                return BadRequest();
            }
            return Ok(response);
        }
    }
}
