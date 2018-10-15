using ERPManager.Entities.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPManager.Web.Models.Home
{
    public class DashboardModel : BaseModel
    {
        public DashboardModel()
        {
            Items = new List<DashboardDataModel>();
        }

        public List<DashboardDataModel> Items { get; set; }


    }

    public class DashboardDataModel
    {
        public DashboardDataModel()
        {
            Items = new List<DashboardDataItemModel>();
        }
        public string Name { get; set; }
        public string ZetoItemText { get; set; }
        public int Count { get; set; }
        public List<DashboardDataItemModel> Items { get; set; }
        public string ManageButton { get; set; }
        public bool UseAvatar { get; set; }
        public int ShowCount { get; set; }
        public string CssClass { get; set; }
        public string FaClass { get; set; }
    }

    public class DashboardDataItemModel
    {

        public string Images { get; set; }
        public string FirstDataIds { get; set; }
        public string SecondDataIds { get; set; }
        public string CreatedDate { get; set; }
    }
}
