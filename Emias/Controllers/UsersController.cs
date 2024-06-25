using Microsoft.AspNetCore.Mvc;
using EMIAS.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace EMIAS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly EmiasContext _context;

        public UsersController(EmiasContext context)
        {
            _context = context;
        }

        // Получение всех пользователей
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        // Добавление нового пользователя
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            if (user == null || string.IsNullOrEmpty(user.EmployeeNumber) || string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.Role))
            {
                return BadRequest(new { Message = "Invalid user data" });
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }

        // Обновление существующего пользователя
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (existingUser == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            existingUser.EmployeeNumber = user.EmployeeNumber;
            existingUser.Password = user.Password;
            existingUser.Role = user.Role;
            await _context.SaveChangesAsync();

            return Ok(existingUser);
        }

        // Удаление пользователя
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            // Получение идентификатора текущего авторизованного пользователя
            var loggedInUserId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value);
            if (user.Id == loggedInUserId)
            {
                return BadRequest(new { Message = "Cannot delete the logged-in user" });
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "User deleted" });
        }
    }
}
