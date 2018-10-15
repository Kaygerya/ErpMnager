using ERPManager.Core.DataAccess.EntityFramework;
using ERPManager.DataAccess.Abstract;
using ERPManager.Entities.Model.Language;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPManager.DataAccess.Model.EntityFramework
{
    public class EfLanguageItemData : EfEntityRepositoryBase<LanguageItem, LanguageDbContext>, ILanguageItemData
    {
      
        public DbSet<LanguageItem>  LocaleStrings { get; set; }
    }
}
