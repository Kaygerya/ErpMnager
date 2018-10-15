using ERPManager.Core.DataAccess.EntityFramework;
using ERPManager.DataAccess.Abstract;
using ERPManager.Entities.Model.Language;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERPManager.DataAccess.Model.EntityFramework
{
    public class EfLanguageData : EfEntityRepositoryBase<Language, LanguageDbContext>, ILanguageData
    {
    }
}
