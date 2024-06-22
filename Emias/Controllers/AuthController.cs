using Microsoft.AspNetCore.Mvc;
using EMIAS.Models;
using System.Linq;
using WebApplication1.Models;

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
        [HttpPost("patientlogin")]
        public IActionResult PatientLogin([FromBody] PatientLoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Polis))
            {
                return BadRequest(new { Message = "Polis cannot be empty" });
            }

            // Проверка, что введены только цифры и их длина составляет 16
            if (request.Polis.Length != 16 || !request.Polis.All(char.IsDigit))
            {
                return BadRequest(new { Message = "Polis number must be 16 digits" });
            }

            try
            {
                if (!long.TryParse(request.Polis, out long polis))
                {
                    return BadRequest(new { Message = "Invalid Polis format" });
                }

                var patient = _context.Patients
                    .FirstOrDefault(p => p.Polis == polis);

                if (patient == null)
                {
                    return Unauthorized(new { Message = "Invalid Polis number" });
                }

                return Ok(new { Message = "Authorization successful", Role = "Patient" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Server error: {ex.Message}" });
            }
        }
    }
}