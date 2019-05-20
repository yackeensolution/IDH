using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IDH.Sales.Model.Models.UserModels
{
    public class UpdateDeviceTokenRequest : BaseRequest
    {
        [Required]
        public Guid SecurityToken { get; set; }

        [Required]
        public string DeviceToken { get; set; }
    }
}