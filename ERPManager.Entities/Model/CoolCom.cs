using ERPManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ERPManager.Entities.Model
{
    public class CoolCom : Entity,  IEntity
    {
        private ICollection<CoolApp> _coolApps;


        public string CoolCompanyId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }


        public virtual ICollection<CoolApp> CoolApps
        {
            get { return _coolApps ?? (_coolApps = new List<CoolApp>()); }
            protected set { _coolApps = value; }
        }


    }
}
