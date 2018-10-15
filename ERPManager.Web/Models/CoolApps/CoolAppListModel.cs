using ERPManager.Entities.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPManager.Web.Models.CoolApps
{
    public class CoolAppListModel : BaseModel
    {
        public CoolAppListModel()
        {
            Items = new List<CoolAppModel>();
        }
        public List<CoolAppModel> Items { get; set; }
    }
}
