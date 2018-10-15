using ERPManager.Entities.Model.Base;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERPManager.Web.Models.CoolApps
{
    public class CoolAppModel:BaseModel
    {
        public CoolAppModel()
        {
            AvailableCompanies = new List<SelectListItem>();
        }

        public string Image { get; set; }
        [Required]
        public string CompanyId { get; set; }
        [Required]
        public string AppCode { get; set; }
        [Required]
        public string CoolCompanyId { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<SelectListItem> AvailableCompanies { get; set; }
    }
}
