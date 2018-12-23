using System.ComponentModel.DataAnnotations;

namespace TutorsOfMogilev_NetCore.Models.Account
{
    public class LoginModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}