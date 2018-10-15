using ERPManager.Entities.Model.Base;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPManager.Web.Models.CoolUsers
{
    public class CoolUserModel :BaseModel
    {
        public CoolUserModel()
        {
            AvailableCompanies = new List<SelectListItem>();
        }

        public string CompanyId { get; set; }
        public string UserId { get; set; }
        public string CoolUserId { get; set; }
        public string CoolProfilePictureUrl { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<SelectListItem> AvailableCompanies { get; set; }
    }
}
