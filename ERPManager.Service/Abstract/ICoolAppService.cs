using ERPManager.Entities.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPManager.Service.Abstract
{
    public interface ICoolAppService
    {
        List<CoolApp> GetAll();
        List<CoolApp> GetByCompanyId(string companyId);
        List<CoolApp> SearchCoolApp(out long totalRecord, int start, int pageSize, string q, string sortId, bool asc);
        CoolApp GetById(string Id);
        void Insert(CoolApp coolApp);
        void Update(CoolApp coolApp);
        void Delete(string coolAppId);
    }
}
