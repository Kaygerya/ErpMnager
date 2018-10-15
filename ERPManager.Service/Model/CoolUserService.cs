using ERPManager.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using ERPManager.Entities.Model;
using ERPManager.DataAccess.Abstract;
using System.Linq;

namespace ERPManager.Service.Model
{
    public class CoolUserService : ICoolUserService
    {
        private  ICoolUserData _coolUserData { get; set; }

        public CoolUserService(ICoolUserData coolUserData)
        {
            this._coolUserData = coolUserData;
        }


        public void Delete(string Id)
        {
            _coolUserData.Delete(new CoolUser { Id = Id });
        }

        public List<CoolUser> GetAll()
        {
            return _coolUserData.GetList();
        }


        public CoolUser GetById(string id)
        {
           return  _coolUserData.Get(k => k.Id == id);
                
        }

        public List<CoolUser> SearchCoolUser(out long totalRecord, int start, int pageSize, string q, string sortId, bool asc)
        {
            //_coolComData.Get()
            var query = _coolUserData.GetList().AsQueryable();
            if (!string.IsNullOrEmpty(q))
            {
                query = query.Where(k => k.CoolComId.Contains(q) || k.CoolUserId.Contains(q) ||  k.UserId.Contains(q));
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
                else if (sortId == "CoolUserId")
                {
                    if (asc)
                    {
                        query = query.OrderBy(k => k.CoolUserId);
                    }
                    else
                    {
                        query = query.OrderByDescending(k => k.CoolUserId);
                    }
                }

                else if (sortId == "UserId")
                {
                    if (asc)
                    {
                        query = query.OrderBy(k => k.UserId);
                    }
                    else
                    {
                        query = query.OrderByDescending(k => k.UserId);
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

        public List<CoolUser> GetByCompanyId(string companyId)
        {
            return _coolUserData.GetList(u => u.CoolComId == companyId);
        }

        public void Insert(CoolUser coolUser)
        {
            coolUser.CreatedDate = DateTime.Now;
            _coolUserData.Add(coolUser);
        }

        public void Update(CoolUser coolUser)
        {
            _coolUserData.Update(coolUser);
        }
    }
}
