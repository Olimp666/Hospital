using Domain.Models;
using Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Controllers
{
    [ApiController]
    [Route("specialization")]
    public class SpecializationController : ControllerBase
    {
        private readonly ISpecializationRepository _rep;
        public SpecializationController(ISpecializationRepository rep)
        {
            _rep = rep;
        }

        [Authorize]
        [HttpPost("add")]
        public IActionResult AddSpecialization(string name)
        {
            Specialization specialization = new(0, name);
            var res = specialization.IsValid();
            if (res.IsFailure)
                return Problem(statusCode: 404, detail: res.Message);

            if (_rep.Create(specialization))
            {
                _rep.Save();
                return Ok(_rep.GetByName(name));
            }
            return Problem(statusCode: 404, detail: "Creating failure");
        }

        [Authorize]
        [HttpDelete("delete")]
        public IActionResult DeleteSpecialization(ulong? id)
        {
            if (_rep.Delete(id))
            {
                _rep.Save();
                return Ok();
            }
            return Problem(statusCode: 404, detail: "Deleting failure");

        }

        [HttpGet("get_all")]
        public IActionResult GetAll()
        {
            return Ok(_rep.GetAll());
        }
    }
}
