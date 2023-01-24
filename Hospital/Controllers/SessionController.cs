using Domain.Models;
using Domain.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Controllers
{
    [ApiController]
    [Route("session")]
    public class SessionController : ControllerBase
    {
        private readonly SessionService _service;
        private readonly ScheduleService _serviceSched;
        public SessionController(SessionService service, ScheduleService scheduleService)
        {
            _service = service;
            _serviceSched = scheduleService;
        }

        [Authorize]
        [HttpPost("save")]
        public IActionResult SaveSession(ulong? patient_id, ulong? doctor_id, DateTime start_time, DateTime end_time, ulong? schedule_id)
        {
            Session session = new(0, start_time, end_time, patient_id, doctor_id);
            var schedule = _serviceSched.GetSchedule(schedule_id);
            if (schedule.IsFailure)
                return Problem(statusCode: 404, detail: schedule.Message);

            var res = _service.SaveSession(session, schedule.Value);

            if (res.IsFailure)
                return Problem(statusCode: 404, detail: res.Message);

            return Ok(res.Value);
        }

        [HttpGet("get/existing")]
        public IActionResult GetExistingSessions(ulong? specialization_id)
        {
            Specialization spec = new(specialization_id, "");
            var res = _service.GetExistingSessions(spec);

            if (res.IsFailure)
                return Problem(statusCode: 404, detail: res.Message);

            return Ok(res.Value);
        }

        [HttpGet("get/free")]
        public IActionResult GetFreeSessions(ulong? specialization_id, ulong? schedule_id)
        {
            var schedule = _serviceSched.GetSchedule(schedule_id);
            if (schedule.IsFailure)
                return Problem(statusCode: 404, detail: schedule.Message);
            var res = _service.GetFreeSessions(new Specialization(specialization_id, ""), schedule.Value);

            if (res.IsFailure)
                return Problem(statusCode: 404, detail: res.Message);

            return Ok(res.Value);
        }
    }
}
