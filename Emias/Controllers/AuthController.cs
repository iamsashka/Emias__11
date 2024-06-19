using Microsoft.AspNetCore.Mvc;
using EMIAS.Models;
using System.Linq;

namespace EMIAS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly EmiasContext _context;

        public AuthController(EmiasContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var doctor = _context.Doctors
                .FirstOrDefault(d => d.IdDoctor.ToString() == request.EmployeeNumber && d.EnterPassword == request.Password);

            if (doctor == null)
            {
                return Unauthorized(new { Message = "Неверный номер сотрудника или пароль" });
            }

            return Ok(new { Message = "Авторизация успешна", Role = "Doctor" });
        }
    }
}