using ERPManager.Entities.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPManager.Web.Models.CoolQueries
{
    public class CoolQueryListModel : BaseModel
    {
        public CoolQueryListModel()
        {
            Items = new List<CoolQueryModel>();
        }

        public List<CoolQueryModel> Items { get; set; }

    }
}
