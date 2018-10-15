using ERPManager.Core.Settings;
using ERPManager.Entities.Model.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPManager.DataAccess.Model.EntityFramework
{
    public class CustomIdentityDbContext : IdentityDbContext<CustomIdentityUser, CustomIdentityRole,string>
    { 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            switch(ErpManagerSettings.IdentityContextDbType)
            {

                case "MsSql":
                    optionsBuilder.UseSqlServer(ErpManagerSettings.IdentityContexConnectionString);
                    break;

                case "SqLite":
                    optionsBuilder.UseSqlite(ErpManagerSettings.IdentityContexConnectionString);
                    break;

                case "MySql":
                    optionsBuilder.UseMySql(ErpManagerSettings.IdentityContexConnectionString);
                    break;

            }

        }
        //public DbSet<CustomIdentityUser> IdentityUsers { get; set; }
        //public DbSet<CustomIdentityRole> IdentityRoles { get; set; }
    }
}
