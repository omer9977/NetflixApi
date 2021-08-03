using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Concrete
{
    public class UserInfo : CommonProperties
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")] // Bunu incele
        public string Email { get; set; }


        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [NotMapped]
        public string ConfirmPassword { get; set; }

        public bool Status { get; set; }

    }
}
