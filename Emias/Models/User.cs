namespace EMIAS.Models
{
    public class User
    {
        public int Id { get; set; }
        public string EmployeeNumber { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // "Doctor", "Patient", "Admin"
    }

}
