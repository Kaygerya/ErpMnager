using System;
using System.Collections.Generic;
using System.Text;

namespace ERPManager.Core.Settings
{
    public  class ErpManagerSettings
    {
        public int PageSize { get; set; }
        public int DashboardDataItemCount { get; set; }

        public string CompanyImagePath { get; set; }
        public string UserProfilePath { get; set; }
        public string AppPicturePath { get; set; }



        //entegration settings
        public string CoolUser { get; set; }
        public string CoolPass { get; set; }
        public string CoolAtFormSecretKey { get; set; }

        public static string  LanguageContexConnectionString { get; set; }
        public static string LanguageContextDbType { get; set; }

        public static string IdentityContexConnectionString { get; set; }
        public static string IdentityContextDbType { get; set; }

        public static string ErpContexConnectionString { get; set; }
        public static string ErpContextDbType { get; set; }
    }
}
