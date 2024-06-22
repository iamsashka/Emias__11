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
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.EmployeeNumber) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new { Message = "EmployeeNumber and Password cannot be empty" });
            }

            try
            {
                // Преобразуем EmployeeNumber в int для поиска по IdDoctor
                if (!int.TryParse(request.EmployeeNumber, out int idDoctor))
                {
                    return BadRequest(new { Message = "Invalid EmployeeNumber format" });
                }

                var doctor = _context.Doctors
                    .FirstOrDefault(d => d.IdDoctor == idDoctor && d.EnterPassword == request.Password);

                if (doctor == null)
                {
                    return Unauthorized(new { Message = "Неверный номер сотрудника или пароль" });
                }

                return Ok(new { Message = "Авторизация успешна", Role = "Doctor" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Ошибка сервера: {ex.Message}" });
            }
        }


        [HttpPost("adminlogin")]
        public IActionResult AdminLogin([FromBody] LoginRequestt request)
        {
            if (request == null || string.IsNullOrEmpty(request.EmployeeNumberr) || string.IsNullOrEmpty(request.Passwordd))
    {
                return BadRequest(new { Message = "EmployeeNumber and Password cannot be empty" });
            }

            try
            {
                if (!int.TryParse(request.EmployeeNumberr, out int idAdmin))
                {
                    return BadRequest(new { Message = "Invalid EmployeeNumber format" });
                }

                var admin = _context.Admins
                    .FirstOrDefault(a => a.IdAdmin == idAdmin && a.EnterPassword == request.Passwordd);

                if (admin == null)
                {
                    return Unauthorized(new { Message = "Неверный номер сотрудника или пароль" });
                }

                return Ok(new { Message = "Авторизация успешна", Role = "Admin" });
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                return StatusCode(500, new { Message = $"Ошибка сервера: {ex.Message}" });
            }
        }
    }
}