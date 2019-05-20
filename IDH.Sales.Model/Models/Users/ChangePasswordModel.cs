using System;
using System.ComponentModel.DataAnnotations;

namespace IDH.Sales.Model.Models.UserModels
{
    public class ChangePasswordRequest:BaseRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string OldPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }
        public DateTime CreationDate { get; set; }
    }
}