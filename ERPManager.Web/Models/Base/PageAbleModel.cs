using ERPManager.Entities.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPManager.Web.Models.Base
{
    public class PageAbleModel : BaseModel
    {
        public int PageIndex { get; set; }
        public int PageCount { get; set; }
    }
}
