using Domain.Models;
using Domain.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Controllers
{
    [ApiController]
    [Route("schedule")]
    public class ScheduleController : ControllerBase
    {
        private readonly ScheduleService _service;
        private readonly DoctorService _serviceDoc;
        public ScheduleController(ScheduleService service, DoctorService doctorService)
        {
            _service = service;
            _serviceDoc = doctorService;
        }

        [HttpGet("get")]
        public IActionResult GetSchedule(ulong? doctor_id)
        {
            var doc = _serviceDoc.FindDoctor(doctor_id);
            if (doc.IsFailure)
                return Problem(statusCode: 404, detail: doc.Message);

            var res = _service.GetSchedule(doc.Value);
            if (res.IsFailure)
                return Problem(statusCode: 404, detail: res.Message);

            return Ok(res.Value);
        }

        [Authorize]
        [HttpPost("register")]
        public IActionResult AddSchedule(ulong? doctor_id, DateTime start_time, DateTime end_time)
        {
            Schedule schedule = new(0, doctor_id, start_time, end_time);

            var res = _service.AddSchedule(schedule);

            if (res.IsFailure)
                return Problem(statusCode: 404, detail: res.Message);

            return Ok();
        }

        [Authorize]
        [HttpPost("update")]
        public IActionResult UpdateSchedule(ulong? schedule_id, ulong? doctor_id, DateTime? start_time, DateTime? end_time)
        {
            var res = _service.GetSchedule(schedule_id);
            if (res.IsFailure)
                return Problem(statusCode: 404, detail: res.Message);

            var schedule = res.Value;

            if (doctor_id != null)
                schedule.DoctorID = doctor_id;
            if (start_time != null && end_time != null)
            {
                schedule.StartTime = (DateTime)start_time;
                schedule.EndTime = (DateTime)end_time;
            }

            var res1 = _service.UpdateSchedule(schedule);

            if (res1.IsFailure)
                return Problem(statusCode: 404, detail: res1.Message);

            return Ok();
        }
    }
}
