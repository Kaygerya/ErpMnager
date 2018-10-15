using ERPManager.Entities.Model.Base;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERPManager.Web.Models.CoolQueries
{
    public class CoolQueryModel : BaseModel
    {
        public CoolQueryModel()
        {
            AvailableCompanies = new List<SelectListItem>();
            AvailableDirections = new List<SelectListItem>();
        }
        [Required]
        public string QueryCode { get; set; }

        public CoolQueryDirection CoolQueryDirection { get; set; }
        [Required]
        public string CompanyId { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Query { get; set; }

        public List<SelectListItem> AvailableCompanies { get; set; }

        public List<SelectListItem> AvailableDirections { get; set; }

        public DateTime CreatedDate { get; set; }


    }
}
