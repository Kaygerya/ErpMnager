using ERPManager.Entities.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPManager.Service.Abstract
{
    public interface ICoolComService
    {

        List<CoolCom> GetAll();
        List<CoolCom> GetByCompanyId(string companyId);
        List<CoolCom> SearchCoolCom(out long totalRecord, int start, int pageSize, string q, string sortId, bool asc);
        CoolCom GetCoolComById(string Id);
        void Insert(CoolCom coolCom);
        void Update(CoolCom coolCom);
        void Delete(string coolComId);
    }
}
