using ERPManager.Core.DataAccess.EntityFramework;
using ERPManager.DataAccess.Abstract;
using ERPManager.Entities.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPManager.DataAccess.Model.EntityFramework
{
    public class EfCoolAppData  : EfEntityRepositoryBase<CoolApp, ErpDatabaseContext>, ICoolAppData
    {
      
    }
}
