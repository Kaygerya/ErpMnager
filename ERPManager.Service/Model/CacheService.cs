using ERPManager.Core.Entities;
using ERPManager.Service.Abstract;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPManager.Service.Model
{
    public class CacheService:  ICacheService
    {
        private IMemoryCache _cache;
        public CacheService(IMemoryCache cache)
        {
            _cache = cache;

        }

        public object GetFromCache(string key)
        {
           return   _cache.Get(key);
        }

        public void InsertToCache(string key, object data)
        {
            _cache.Set(key, data);
        }



    }
}
