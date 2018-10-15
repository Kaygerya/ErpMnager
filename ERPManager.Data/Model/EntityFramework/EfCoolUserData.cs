using ERPManager.Core.DataAccess.EntityFramework;
using ERPManager.DataAccess.Abstract;
using ERPManager.Entities.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPManager.DataAccess.Model.EntityFramework
{
    public class EfCoolUserData : EfEntityRepositoryBase<CoolUser, ErpDatabaseContext>, ICoolUserData
    {
        //public IConfiguration _configuration { get; set; }

        //public EfCoolUserData(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}
    }
}
