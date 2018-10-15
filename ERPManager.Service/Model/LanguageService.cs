using ERPManager.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using ERPManager.Entities.Model.Language;
using ERPManager.DataAccess.Abstract;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.ComponentModel;

namespace ERPManager.Service.Model
{
    public class LanguageService : ILanguageService
    {
        private ILanguageData _languageData { get; set; }
        private ILanguageItemData _languageItemData { get; set; }
        private IHttpContextAccessor  _httpContext { get; set; }
        private ICacheService _cacheService { get; set; }
        private string allLanguagesKey;
        public LanguageService(ILanguageData languageData, ILanguageItemData languageItemData,ICacheService cacheService, IHttpContextAccessor httpContext)
        {
            _languageData = languageData;
            _languageItemData = languageItemData;
            _cacheService = cacheService;
            _httpContext = httpContext;

            allLanguagesKey = "AllLanguages";

        }

        public void Delete(Language language)
        {
            _languageData.Delete(language);
        }

        public Language GetById(string id)
        {
            return  _languageData.Get(k => k.Id == id);
        }

        public void Insert(Language language)
        {
            _languageData.Add(language);
        }

        public void Update(Language language)
        {
            _languageData.Update(language);
        }
        public List<Language> GetAll()
        {
            List <Language> allLanguages = (List<Language>)_cacheService.GetFromCache(allLanguagesKey);
            if(allLanguages == null)
            {
                allLanguages = new List<Language>();
                allLanguages = _languageData.GetList();
                _cacheService.InsertToCache(allLanguagesKey, allLanguages);
            }

            return allLanguages;
        }

        public void InsertLocaleString(LanguageItem item)
        {
            _languageItemData.Add(item);
        }

        public List<LanguageItem> GetAllLocaleStrings(string languageId)
        {
            return _languageItemData.GetList(k => k.LanguageId == languageId);
        }

        public string GetLocaleString( string key)
        {
            string languageId = "tr-TR";
            var languageCookie =  _httpContext.HttpContext.Request.Cookies["culture"];
            if(languageCookie != null)
            {
                languageId = languageCookie;
            }

            var localeData = (List<LanguageItem>)_cacheService.GetFromCache(languageId);
            if (localeData == null)
            {
                localeData = new List<LanguageItem>();
                var localeStrings = _languageItemData.GetList(k => k.LanguageId == languageId);
                localeData = localeStrings;
                _cacheService.InsertToCache(languageId, localeStrings);
            }


            var item = localeData.Where(k=> k.Key == key).FirstOrDefault();
            if(item == null)
            {
                return key;
            }
            else
            {
                return item.Value;
            }
        }

        public string GetLocaleString(string key, object[] parameters)
        {
            string languageId = "tr-TR";
            var languageCookie = _httpContext.HttpContext.Request.Cookies["culture"];
            if (languageCookie != null)
            {
                languageId = languageCookie;
            }

            var localeData = (List<LanguageItem>)_cacheService.GetFromCache(languageId);
            if (localeData == null)
            {
                localeData = new List<LanguageItem>();
                var localeStrings = _languageItemData.GetList(k => k.LanguageId == languageId);
                localeData = localeStrings;
                _cacheService.InsertToCache(languageId, localeStrings);
            }


            var item = localeData.Where(k => k.Key == key).FirstOrDefault();
            if (item == null)
            {
                return key;
            }
            else
            {
                 return string.Format(item.Value, parameters);
            }
        }

    }


}
