using ERPManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPManager.Entities.Model.Language
{
    public class LanguageItem : IEntity
    {

        public int Id { get; set; }
        public string LanguageId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
