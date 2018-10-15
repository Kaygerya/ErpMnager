using ERPManager.Entities.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPManager.Service.Abstract
{
   public interface ICoolQueryService
    {
        List<CoolQuery> GetAll();
        List<CoolQuery> GetByCompanyId(string companyId);
        List<CoolQuery> SearchCoolQuery(out long totalRecord, int start, int pageSize, string q, string sortId, bool asc);
        CoolQuery GetById(string Id);
        void Insert(CoolQuery coolQuery);
        void Update(CoolQuery coolQuery);
        void Delete(string coolQueryId);
    }
}
