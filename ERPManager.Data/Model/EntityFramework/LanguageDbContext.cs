
using ERPManager.Core.Settings;
using ERPManager.Entities.Model.Language;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPManager.DataAccess.Model.EntityFramework
{
    public class LanguageDbContext : DbContext
    {
   
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            switch (ErpManagerSettings.LanguageContextDbType)
            {

                case "MsSql":
                    optionsBuilder.UseSqlServer(ErpManagerSettings.LanguageContexConnectionString);
                    break;

                case "SqLite":
                    optionsBuilder.UseSqlite(ErpManagerSettings.LanguageContexConnectionString);
                    break;

                case "MySql":
                    optionsBuilder.UseMySql(ErpManagerSettings.LanguageContexConnectionString);
                    break;

            }

        }

        public DbSet<Language> Languages { get; set; }
        public DbSet<LanguageItem> LocaleStrings { get; set; }
    }
}
