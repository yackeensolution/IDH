using System;
using System.ComponentModel.DataAnnotations;

namespace IDH.Sales.Model.Models.Users
{
    public class ResetPasswordRequest:BaseRequest
    {
        [Required]
        public string UserID { get; set; }
        public string NewPassword { get; set; }
        public DateTime CreationDate { get; set; }
    }
}