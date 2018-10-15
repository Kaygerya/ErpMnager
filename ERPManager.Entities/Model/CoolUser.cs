using ERPManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPManager.Entities.Model
{
    public class CoolUser: Entity,IEntity
    {
        public string CoolComId { get; set; }
        public string UserId { get; set; }
        public string CoolUserId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
