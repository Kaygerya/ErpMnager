using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ERPManager.Core.Entities
{
    public class Entity
    {
        [Key]
        public string Id { get; set; }
    }
}
