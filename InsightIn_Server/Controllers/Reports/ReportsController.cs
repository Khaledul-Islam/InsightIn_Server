using InsightIn_Note_Service.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsightIn_Server.Controllers.Reports
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _service;

        public ReportsController(IReportService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult TodaysEvent()
        {
            var response = _service.TodaysNotes();
            if (response == null || response.Count == 0)
            {
                return NoContent();
            }
            return Ok(response);
        }
        [HttpGet]
        public IActionResult WeeklyEvent()
        {
            var response = _service.WeeklyNotes();
            if (response == null || response.Count == 0)
            {
                return NoContent();
            }
            return Ok(response);
        }
        
        [HttpGet]
        public IActionResult MonthlyEvent()
        {
            var response = _service.MonthlyNotes();
            if (response == null || response.Count == 0)
            {
                return NoContent();
            }
            return Ok(response);
        }
    }
}
