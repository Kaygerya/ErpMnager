using ERPManager.Core.DataAccess.EntityFramework;
using ERPManager.DataAccess.Abstract;
using ERPManager.Entities.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPManager.DataAccess.Model.EntityFramework
{
    public class EfCoolComData: EfEntityRepositoryBase<CoolCom,ErpDatabaseContext>, ICoolComData
    {
        //public IConfiguration _configuration { get; set; }

        //public EfCoolComData(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}
    }
}
