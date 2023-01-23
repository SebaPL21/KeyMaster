using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KeyMaster.Models
{
    public class UserModel
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Password is requried!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is requried!")]
        public string Email { get; set; }
        public string? Nickname { get; set; }
    }
    public class Jwt
    {
        public string key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }
    }
}
