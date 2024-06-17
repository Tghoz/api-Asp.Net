using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace testApi_sqlServer.Dtos.Account
{
    public class LoginDto
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$")]
        public string User { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}