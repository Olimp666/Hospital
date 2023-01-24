using Domain.Models;
using Domain.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Controllers
{
    [ApiController]
    [Route("doctor")]
    public class DoctorController : ControllerBase
    {
        private readonly DoctorService _service;
        public DoctorController(DoctorService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpPost("create")]
        public IActionResult CreateDoctor(string fullname, ulong? specialization_id)
        {
            Doctor doctor = new(0, fullname, specialization_id);
            var res = _service.CreateDoctor(doctor);

            if (res.IsFailure)
                return Problem(statusCode: 404, detail: res.Message);

            return Ok(res.Value);
        }

        [Authorize]
        [HttpDelete("delete")]
        public IActionResult DeleteDoctor(ulong? id)
        {
            var res = _service.DeleteDoctor(id);

            if (res.IsFailure)
                return Problem(statusCode: 404, detail: res.Message);

            return Ok(res.Value);
        }

        [HttpGet("get_all")]
        public IActionResult GetAllDoctors()
        {
            var res = _service.GetAllDoctors();

            if (res.IsFailure)
                return Problem(statusCode: 404, detail: res.Message);

            return Ok(res.Value);
        }

        [HttpGet("find")]
        public IActionResult FindDoctor(ulong? id)
        {
            var res = _service.FindDoctor(id);

            if (res.IsFailure)
                return Problem(statusCode: 404, detail: res.Message);

            return Ok(res.Value);
        }

        [HttpGet("get")]
        public IActionResult FindDoctors(ulong? specialization)
        {
            Specialization spec = new(specialization, "a");
            var res = _service.FindDoctors(spec);

            if (res.IsFailure)
                return Problem(statusCode: 404, detail: res.Message);

            return Ok(res.Value);
        }
    }
}
