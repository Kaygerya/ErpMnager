using ERPManager.Entities.Model.Language;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPManager.Service.Abstract
{
    public interface ILanguageService
    {
        void Delete(Language language);
        Language GetById(string id);
        void Insert(Language language);
        void Update(Language language);
        List<Language> GetAll();
        List<LanguageItem> GetAllLocaleStrings(string languageId);
        string GetLocaleString(string key, object[] parameters);
        void InsertLocaleString(LanguageItem item);
        string GetLocaleString( string key);

    }
}
