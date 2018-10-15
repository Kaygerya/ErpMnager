using ERPManager.Entities.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPManager.Service.Abstract
{
    public interface ICoolUserService
    {
        List<CoolUser> GetAll();
        List<CoolUser> GetByCompanyId(string companyId);
        CoolUser GetById(string id);
        void Insert(CoolUser coolUser);
        void Update(CoolUser coolUser);
        void Delete(string Id);
        List<CoolUser> SearchCoolUser(out long totalRecord, int start, int length, string q, string sortId, bool asc);
    }
}
