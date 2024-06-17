using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace testApi_sqlServer.Dtos.Account
{
    public class RegisterDto
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$")]
        public string? Username { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }

    }
}