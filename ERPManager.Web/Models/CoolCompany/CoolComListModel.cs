using ERPManager.Entities.Model;
using ERPManager.Entities.Model.Base;
using ERPManager.Web.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPManager.Web.Models.CoolCompany
{
    public class CoolComListModel : PageAbleModel
    {
        public CoolComListModel()
        {
            Items = new List<CoolComModel>();
        }
        public List<CoolComModel> Items { get; set; }
    }
}
