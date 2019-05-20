using System.ComponentModel.DataAnnotations;

namespace IDH.Sales.Model.Models.UserModels
{
    public class LoginModel : BaseRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        public string DeviceToken { get; set; }
        public string employeeID { get; set; }
        public int Louck { get; set; }
    }
}