using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AutoMapper;

namespace Flight.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }

        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? role { get; set; }

    }
}
