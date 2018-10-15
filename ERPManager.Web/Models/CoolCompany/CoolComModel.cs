using ERPManager.Entities.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERPManager.Web.Models.CoolCompany
{
    public class CoolComModel : BaseModel
    {
        public string Id { get; set; }

        public string CompanyImagePath { get; set; }
        [Required]
        public string CompanyId { get; set; }
        [Required]
        public string CoolCompanyId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
