using System.ComponentModel.DataAnnotations;

namespace EPharma.Application.Requests.Identity
{
    public class TokenRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}