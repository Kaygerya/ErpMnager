using ERPManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ERPManager.Entities.Model
{
    public class CoolQuery : Entity, IEntity
    {
        public string QueryCode { get; set; }


        public int Direction { get; set; }

        public string CoolComId { get; set; }

        public string Description { get; set; }

        public string Query { get; set; }

        public DateTime CreatedDate { get; set; }

        [NotMapped]
        public CoolQueryDirection CoolQueryDirection {
            get
            {
                return (CoolQueryDirection)this.Direction;
            }
            set
            {
                this.Direction = (int)value;
            }
        }
}

    
}

    public enum CoolQueryDirection
{
    incoming = 0,
    outgoing = 1
}