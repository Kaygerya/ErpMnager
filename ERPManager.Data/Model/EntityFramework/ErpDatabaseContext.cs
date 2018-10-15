using ERPManager.Core.Settings;
using ERPManager.Entities.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace ERPManager.DataAccess.Model.EntityFramework
{
    public class ErpDatabaseContext : DbContext
    {


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            switch(ErpManagerSettings.ErpContextDbType)
            {
                case "MsSql":
                    optionsBuilder.UseSqlServer(ErpManagerSettings.ErpContexConnectionString);
                    break;

                case "SqLite":
                    optionsBuilder.UseSqlite(ErpManagerSettings.ErpContexConnectionString);
                    break;

                case "MySql":
                    optionsBuilder.UseMySql(ErpManagerSettings.ErpContexConnectionString);
                    break;
            }
        }

        public DbSet<CoolCom> CoolComs { get; set; }
        public DbSet<CoolUser> CoolUsers { get; set; }
        public DbSet<CoolApp> CoolApps { get; set; }
        public DbSet<CoolQuery> CoolQueries { get; set; }


        protected override void OnModelCreating(Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            //modelBuilder.Entity<CoolApp>().HasKey(k => k.CoolCompanyId);
            //modelBuilder.Entity<CoolApp>().HasOne(b => b.CoolCom).WithMany(c=> c.CoolApps).HasForeignKey(c => c.CompanyId).HasConstraintName("FK_CoolApps_CoolComs");

            //.HasOne(c => c.CoolCom).WithMany(b => b.CoolApps).HasForeignKey(c => c.CompanyId).HasConstraintName("FK_CoolApps_CoolComs");
            // modelBuilder.Entity<CoolCom>().HasMany(c=> c.CoolApps).WithOne(b=> b.CoolCom).HasForeignKey(c => c.CompanyId).HasConstraintName("FK_CoolApps_CoolComs");


            //       modelBuilder.Entity<CoolApp>()
            //.hason
            //.WithMany(g => g.CoolApps)
            //.HasForeignKey<string>(s => s.CompanyId);
        }





    }

}
