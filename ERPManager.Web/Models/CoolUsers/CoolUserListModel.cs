using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERPManager.Entities.Model;
using ERPManager.Web.Models.Base;
using ERPManager.Web.Models.CoolUsers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERPManager.Web.Models.CoolUsers
{
    public class CoolUserListModel : PageAbleModel
    {
        public CoolUserListModel()
        {
            Items = new List<CoolUserModel>();
            AvailableCompanies = new List<SelectListItem>();
        }

        public List<CoolUserModel> Items { get; set; }
        public string CompanyId { get; set; }
        public List<SelectListItem> AvailableCompanies { get; set; }
    }
}
