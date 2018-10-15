using ERPManager.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using ERPManager.Entities.Model;
using ERPManager.DataAccess.Abstract;
using System.Linq;

namespace ERPManager.Service.Model
{
    public class CoolAppService : ICoolAppService
    {
        private ICoolAppData _coolAppData { get; set; }

        public CoolAppService(ICoolAppData coolAppData)
        {
            this._coolAppData = coolAppData;
        }

        public void Delete(string Id)
        {

            _coolAppData.Delete(new CoolApp { Id = Id });
        }

        public List<CoolApp> GetAll()
        {
            return _coolAppData.GetList();
        }

        public List<CoolApp> GetByCompanyId(string companyId)
        {
            return _coolAppData.GetList(u => u.CoolComId == companyId);
        }

        public void Insert(CoolApp coolApp)
        {
            coolApp.CreatedDate = DateTime.Now;
            _coolAppData.Add(coolApp);
        }

        public List<CoolApp> SearchCoolApp(out long totalRecord, int start, int pageSize, string q, string sortId, bool asc)
        {
            //_coolComData.Get()
            var query = _coolAppData.GetList().AsQueryable();
            if (!string.IsNullOrEmpty(q))
            {
                query = query.Where(k => k.CoolComId.Contains(q) || k.CoolCompanyId.Contains(q) || k.AppCode.Contains(q) || k.Id.Contains(q));
            }

            if (!string.IsNullOrEmpty(sortId))
            {
                if (sortId == "CompanyId")
                {
                    if (asc)
                    {
                        query = query.OrderBy(k => k.CoolComId);
                    }
                    else
                    {
                        query = query.OrderByDescending(k => k.CoolComId);
                    }
                }
                else if (sortId == "CoolCompanyId")
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
                else if(sortId == "AppCode")
                {
                    if (asc)
                    {
                        query = query.OrderBy(k => k.AppCode);
                    }
                    else
                    {
                        query = query.OrderByDescending(k => k.AppCode);
                    }
                }
                else if(sortId == "Id")
                {
                    if (asc)
                    {
                        query = query.OrderBy(k => k.Id);
                    }
                    else
                    {
                        query = query.OrderByDescending(k => k.Id);
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

        public void Update(CoolApp coolApp)
        {
            _coolAppData.Update(coolApp);
        }

        public CoolApp GetById(string Id)
        {
            return _coolAppData.Get(k => k.Id == Id);
        }
    }
}
