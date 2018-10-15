using ERPManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPManager.Service.Abstract
{
    public interface ICacheService
    {
        void InsertToCache(string key, object data);
        object GetFromCache(string key);
    }
}
