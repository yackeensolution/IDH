using System;

namespace IDH.Sales.Model.Models.Users
{
    public class LoginResultModel
    {
        public LoginResultModel(Guid _UserID, Guid _SecurityToken, string _FullName, string _ImageURL,Guid _RoleID,string _RoleName,int Louck)
        {
            this.UserID = _UserID;
            this.SecurityToken = _SecurityToken;
            this.FullName = _FullName;
            this.ImageURL = _ImageURL;
            this.RoleID = _RoleID;
            this.RoleName = _RoleName;
            this.Louck = Louck;
        }
        public Guid UserID { get; set; }

        public Guid SecurityToken { get; set; }

        public string FullName { get; set; }

        public string ImageURL { get; set; }

        public Guid RoleID { get; set; }

        public string RoleName { get; set; }
        public int Louck { get; set; }
    }
}
