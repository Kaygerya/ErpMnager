using ERPManager.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using ERPManager.Entities.Model;
using ERPManager.DataAccess.Abstract;
using System.Linq;

namespace ERPManager.Service.Model
{
    public class CoolComService : ICoolComService
    {
        private ICoolComData _coolComData { get; set; }

        public CoolComService(ICoolComData coolComData)
        {
            this._coolComData = coolComData;
        }

        public List<CoolCom> SearchCoolCom(out long totalRecord, int start, int pageSize, string q, string sortId, bool asc)
        {
            //_coolComData.Get()
            var query = _coolComData.GetList().AsQueryable();
            if(!string.IsNullOrEmpty(q) )
            {
                query = query.Where(k => k.Id.Contains(q) || k.CoolCompanyId.Contains(q));
            }

            if(!string.IsNullOrEmpty(sortId))
            {
                if(sortId == "CompanyId")
                {
                    if(asc)
                    {
                        query = query.OrderBy(k => k.Id);
                    }
                    else
                    {
                        query = query.OrderByDescending(k => k.Id);
                    }
                }
                else  if (sortId == "CoolCompanyId")
                {
                    if (asc)
                    {
                        query = query.OrderBy(k => k.CoolCompanyId);
                    }
                    else
                    {
                        query = query.OrderByDescending(k => k.CoolCompanyId);
                    }
                }
            }
            else
            {
                query = query.OrderByDescending(k => k.CreatedDate);
            }

            totalRecord = query.LongCount();
            query = query.Skip(start).Take(pageSize);



            return query.ToList();

        }

        public void Delete(string companyId)
        {
            //_coolComData.Delete(new CoolCom { CompanyId = companyId });
            var coolCom = GetByCompanyId(companyId).FirstOrDefault();
            coolCom.IsActive = false;
            _coolComData.Update(coolCom);
        }

        public List<CoolCom> GetAll()
        {
           return _coolComData.GetList();
        }

        public CoolCom GetCoolComById(string companyId)
        {
            return _coolComData.Get(u => u.Id == companyId);
        }

        public List<CoolCom> GetByCompanyId(string companyId)
        {
            return _coolComData.GetList(u => u.Id == companyId);
        }

        public void Insert(CoolCom coolCom)
        {
            coolCom.IsActive = true;
            coolCom.CreatedDate = DateTime.Now;
            _coolComData.Add(coolCom);
        }

        public void Update(CoolCom coolCom)
        {
            _coolComData.Update(coolCom);
        }
    }
}
