using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ERPManager.Entities.Model.Language;
using ERPManager.Service.Abstract;
using ERPManager.Entities.Model.Base;
using ERPManager.Core.Settings;
using ERPManager.Web.Models.Home;
using Microsoft.AspNetCore.Mvc.Routing;

namespace ERPManager.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ICoolUserService _coolUserService;
        private ICoolQueryService _coolQueryService;
        private ICoolComService _coolComService;
        private ICoolAppService _coolAppService;
        private ILanguageService _languageService;
        private ErpManagerSettings _erpManagerSettings;
        public HomeController(ICoolUserService coolUserService, ICoolComService coolComService, ILanguageService languageService, ErpManagerSettings erpManagerSettings, ICoolQueryService coolQueryService, ICoolAppService coolAppService)
        {
            _coolUserService = coolUserService;
            _coolComService = coolComService;
            _languageService = languageService;
            _erpManagerSettings = erpManagerSettings;
            _coolQueryService = coolQueryService;
            _coolAppService = coolAppService;
        }


        public ActionResult Index()
        {
            DashboardModel model = new DashboardModel();
            model = PrepareDashboardModel(model);
            return View(model);
        }

        public DashboardModel PrepareDashboardModel(DashboardModel model)
        {
            long totalCount = 0;
            //coolComs
            DashboardDataModel coolComItem = new DashboardDataModel();
            var coolComData = _coolComService.SearchCoolCom(out totalCount, 0, _erpManagerSettings.DashboardDataItemCount, "", "", true);
            coolComItem.Count = Convert.ToInt32(totalCount);
            coolComItem.ManageButton= "/CoolCom/Index";
            coolComItem.Name = _languageService.GetLocaleString("CoolCom Main");
            coolComItem.ZetoItemText = _languageService.GetLocaleString("CoolCom0");
            coolComItem.UseAvatar = false;
            coolComItem.ShowCount = _erpManagerSettings.DashboardDataItemCount;
            coolComItem.CssClass = "btn-danger";
            coolComItem.FaClass = "fa fa-building-o";
            foreach (var coolCom in coolComData)
            {
                DashboardDataItemModel subItem = new DashboardDataItemModel();
                subItem.Images=_erpManagerSettings.CompanyImagePath + coolCom.CoolCompanyId;
                subItem.FirstDataIds= _languageService.GetLocaleString("CompanyId")+ ": " + coolCom.Id;
                subItem.SecondDataIds= _languageService.GetLocaleString("CoolCompanyId") + ": " +  coolCom.CoolCompanyId;
                coolComItem.Items.Add(subItem);
            }
            model.Items.Add(coolComItem);

            DashboardDataModel coolUserItem = new DashboardDataModel();
            var coolUserData = _coolUserService.SearchCoolUser(out totalCount, 0, _erpManagerSettings.DashboardDataItemCount, "", "", true);
            coolUserItem.Count = Convert.ToInt32(totalCount);
            coolUserItem.ManageButton = "/CoolUser/Index";
            coolUserItem.Name = _languageService.GetLocaleString("CoolUser Main");
            coolUserItem.ZetoItemText = _languageService.GetLocaleString("CoolUser0");
            coolUserItem.UseAvatar = true;
            coolUserItem.ShowCount = _erpManagerSettings.DashboardDataItemCount;
            coolUserItem.CssClass = "btn-info";
            coolUserItem.FaClass = "fa fa-user-o";
            foreach (var coolUser in coolUserData)
            {
                DashboardDataItemModel subItem = new DashboardDataItemModel();
                subItem.Images = _erpManagerSettings.UserProfilePath + coolUser.CoolUserId;
                subItem.FirstDataIds = _languageService.GetLocaleString("CompanyId") + ": " + coolUser.CoolComId;
                subItem.SecondDataIds = _languageService.GetLocaleString("CoolUserId") + ": " +  coolUser.CoolUserId;
                coolUserItem.Items.Add(subItem);
            }
            model.Items.Add(coolUserItem);

            DashboardDataModel coolQueryItem = new DashboardDataModel();
            var coolQueryData =_coolQueryService.SearchCoolQuery(out totalCount, 0, _erpManagerSettings.DashboardDataItemCount, "", "", true);
            coolQueryItem.Count = Convert.ToInt32(totalCount);
            coolQueryItem.ManageButton = "/CoolQuery/Index";
            coolQueryItem.Name = _languageService.GetLocaleString("CoolQuery Main");
            coolQueryItem.ZetoItemText = _languageService.GetLocaleString("CoolQuery0");
            coolQueryItem.ShowCount = _erpManagerSettings.DashboardDataItemCount;
            coolQueryItem.CssClass = "btn-purple";
            coolQueryItem.FaClass = "fa fa-database";
            foreach (var coolQuery in coolQueryData)
            {
                DashboardDataItemModel subItem = new DashboardDataItemModel();
                subItem.Images = "/assets/images/messages/soldan-bakan.png";
                subItem.FirstDataIds = _languageService.GetLocaleString("CompanyId") + ": " + coolQuery.CoolComId;
                subItem.SecondDataIds = _languageService.GetLocaleString("QueryCode") + ": " + coolQuery.QueryCode;
                coolQueryItem.Items.Add(subItem);
            }
            model.Items.Add(coolQueryItem);


            DashboardDataModel  coolAppItem = new DashboardDataModel();
            var coolAppData = _coolAppService.SearchCoolApp(out totalCount, 0, _erpManagerSettings.DashboardDataItemCount, "", "", true);
            coolAppItem.Count = Convert.ToInt32(totalCount);
            coolAppItem.ManageButton = "/CoolApp/Index";
            coolAppItem.Name = _languageService.GetLocaleString("CoolApp Main");
            coolAppItem.ZetoItemText = _languageService.GetLocaleString("coolApp0");
            coolAppItem.ShowCount = _erpManagerSettings.DashboardDataItemCount;
            coolAppItem.CssClass = "btn-warning";
            coolAppItem.FaClass = "fa fa-window-maximize";
            foreach (var coolApp in coolAppData)
            {
                DashboardDataItemModel subItem = new DashboardDataItemModel();
                subItem.Images = "/assets/images/messages/mutlu.png";
                subItem.FirstDataIds = _languageService.GetLocaleString("CompanyId") + ": " + coolApp.CoolComId;
                subItem.SecondDataIds = _languageService.GetLocaleString("CoolCompanyId") + ": " + coolApp.CoolCompanyId;
                coolAppItem.Items.Add(subItem);
            }
            model.Items.Add(coolAppItem);

            return model;
        }
    }
}