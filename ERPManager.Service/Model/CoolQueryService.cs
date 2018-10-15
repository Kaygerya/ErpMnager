using ERPManager.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using ERPManager.Entities.Model;
using ERPManager.DataAccess.Abstract;
using System.Linq;

namespace ERPManager.Service.Model
{
    public class CoolQueryService : ICoolQueryService
    {
        private ICoolQueryData _coolQueryData { get; set; }

        public CoolQueryService(ICoolQueryData coolQueryData)
        {
            this._coolQueryData = coolQueryData;
        }

        public void Delete(string coolQueryId)
        {
            _coolQueryData.Delete(new CoolQuery { Id = coolQueryId });
        }

        public List<CoolQuery> GetAll()
        {
            return _coolQueryData.GetList();
        }

        public List<CoolQuery> GetByCompanyId(string companyId)
        {
            return _coolQueryData.GetList(u => u.CoolComId == companyId);
        }

        public CoolQuery GetById(string Id)
        {
            return _coolQueryData.Get(u => u.Id == Id);
        }

        public void Insert(CoolQuery coolQuery)
        {
            coolQuery.CreatedDate = DateTime.Now;
            _coolQueryData.Add(coolQuery);
        }

        public List<CoolQuery> SearchCoolQuery(out long totalRecord, int start, int pageSize, string q, string sortId, bool asc)
        {
            //_coolComData.Get()
            var query = _coolQueryData.GetList().AsQueryable();
            if (!string.IsNullOrEmpty(q))
            {
                query = query.Where(k => k.CoolComId.Contains(q) || k.Description.Contains(q) || k.QueryCode.Contains(q));
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
                else if (sortId == "Description")
                {
                    if (asc)
                    {
                        query = query.OrderBy(k => k.Description);
                    }
                    else
                    {
                        query = query.OrderByDescending(k => k.Description);
                    }
                }

                else if (sortId == "QueryCode")
                {
                    if (asc)
                    {
                        query = query.OrderBy(k => k.QueryCode);
                    }
                    else
                    {
                        query = query.OrderByDescending(k => k.QueryCode);
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

        public void Update(CoolQuery coolQuery)
        {
            _coolQueryData.Update(coolQuery);
        }
    }
}
