using ERPManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPManager.Entities.Model.Language
{
    public class Language: Entity, IEntity
    {
        public Language()
        {
            if(string.IsNullOrEmpty(Id))
            {
                Id = Guid.NewGuid().ToString();
            }
        }

        public string Name { get; set; }
    }
}
