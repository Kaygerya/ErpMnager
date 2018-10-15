using ERPManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ERPManager.Entities.Model
{
    public class CoolApp : Entity, IEntity
    {


        public string CoolComId { get; set; }

        public string AppCode { get; set; }
        [NotMapped]
        public  string CoolCompanyId { get; set; }
        public DateTime CreatedDate { get; set; }


        public virtual CoolCom CoolCom { get; set; }

    //public virtual ICollection<CoolCom> CoolComs
    //    {
    //        get { return _coolCom ?? (_coolCom = new List<CoolCom>()); }
    //        protected set { _coolCom = value; }
    //    }

    //    //public List<UserAppPermission> UserAppPermissions { get; set; }


    //    //public virtual ICollection<CoolQuery> CoolQuery { get; set; }
    }

    public enum UserAppPermission
    {
        Creator = 1,
        Approver = 2,
        Viewer = 3,
        Editor = 4
    }
}
