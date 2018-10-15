using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ERPManager.Web.Controllers.Base;
using ERPManager.Service.Abstract;
using ERPManager.Core.Settings;
using ERPManager.Web.Models.CoolApps;
using ERPManager.Entities.Model;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERPManager.Web.Controllers
{
    [Authorize]
    public class CoolAppController : BaseController
    {
        private ICoolAppService _coolAppService;
        private ICoolComService _coolComService;
        private ILanguageService _languageService;
        private ErpManagerSettings _erpManagerSettings;
        private int pageSize = 15;
        public CoolAppController(ICoolAppService coolAppService, ICoolComService coolComService, ILanguageService languageService, ErpManagerSettings erpManagerSettings)
        {
            _coolAppService = coolAppService;
            _coolComService = coolComService;
            _languageService = languageService;
            _erpManagerSettings = erpManagerSettings;
        }


        public ActionResult Index()
        {
            CoolAppListModel model = new CoolAppListModel();

            return View(model);
        }

        [HttpPost]
        [Produces("application/json")]
        public ActionResult LoadTable()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Convert.ToInt32(Request.Form["start"].FirstOrDefault());
            var length = Convert.ToInt32(Request.Form["length"].FirstOrDefault());
            string search = Request.Form["search[value]"][0];
            //Get Sort columns value
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDir = Request.Form["order[0][dir]"].FirstOrDefault();
            long totalRecords = 0;

            var coolApps = _coolAppService.SearchCoolApp(out totalRecords, start, length, search, sortColumn, sortColumnDir == "asc"); //GetUserAddresses(user.Id, out totalRecords, start, length, search, sortColumn, sortColumnDir == "asc");
            CoolAppListModel model = new CoolAppListModel();
            model = PrepareCoolAppListModel(model, coolApps);
            return Json(new { draw = draw, recordsFiltered = totalRecords, recordsTotal = totalRecords, data = model.Items });
        }


        public ActionResult Create()
        {
            CoolAppModel model = new CoolAppModel();

            model = PrepareCoolAppModel(model, new CoolApp(), true);
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CoolAppModel model)
        {

            CoolApp coolApp = _coolAppService.GetById(model.Id);

            if (coolApp != null)
            {
                model.Errors.Add(_languageService.GetLocaleString("CoolApp Already Exists"));
                return Json(model);
            }

            if (_coolAppService.GetAll().Where(k => k.Id == model.Id && k.CoolComId == model.CompanyId).Count() > 0)
            {
                model.Errors.Add(_languageService.GetLocaleString("CoolApp Already Exists"));
                return Json(model);
            }

            if (ModelState.IsValid)
            {
                coolApp = new CoolApp();
                coolApp.CoolComId = model.CompanyId;
                coolApp.CoolCompanyId = model.CoolCompanyId;
                coolApp.AppCode = model.AppCode;
                _coolAppService.Insert(coolApp);
                model.SuccessMessage = _languageService.GetLocaleString("CoolApp Created");
            }
            else
            {
                model.Errors.Add("Check fields for editing");
            }
            return Json(model);

        }

        public ActionResult Edit(string Id)
        {
            CoolAppModel model = new CoolAppModel();
            var coolUser = _coolAppService.GetById(Id);


            if (coolUser == null)
            {
                model.Errors.Add(_languageService.GetLocaleString("CoolApp not found"));
                return View(model);
            }

            model = PrepareCoolAppModel(model, coolUser, true);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CoolAppModel model)
        {
            var coolApp = _coolAppService.GetById(model.Id);
            if (coolApp == null)
            {
                model.Errors.Add(_languageService.GetLocaleString("CoolApp not found"));
                return Json(model);
            }
            if (ModelState.IsValid)
            {
                coolApp.CoolComId = model.CompanyId;
                coolApp.CoolCompanyId = model.CoolCompanyId;
                coolApp.AppCode = model.AppCode;
                _coolAppService.Update(coolApp);
            
            model.SuccessMessage = _languageService.GetLocaleString("CoolApp Updated");
            }
            else
            {
                model.Errors.Add("Check fields for editing");
            }
            return Json(model);
        }


        public ActionResult Delete(string Id)
        {

            CoolAppModel model = new CoolAppModel();
            var coolUser = _coolAppService.GetById(Id);
            if (coolUser != null)
            {
                _coolAppService.Delete(Id);
            }
            else
            {
                model.Errors.Add(_languageService.GetLocaleString("CoolApp Couldn't found"));
            }

            model.SuccessMessage = _languageService.GetLocaleString("CoolApp Deleted successfully");
            return Json(model);
        }

        private CoolAppListModel PrepareCoolAppListModel(CoolAppListModel model, List<CoolApp> coolApps)
        {
            foreach (var c in coolApps)
            {
                CoolAppModel ccm = new CoolAppModel();
                ccm = PrepareCoolAppModel(ccm, c);
                model.Items.Add(ccm);
            }

            return model;

        }

        private CoolAppModel PrepareCoolAppModel(CoolAppModel model, CoolApp coolApp, bool fillAvailableCompanies =false)
        {
            model.Id = coolApp.Id;
            model.AppCode = coolApp.AppCode;
            model.CompanyId = coolApp.CoolComId;
            model.CoolCompanyId = coolApp.CoolCompanyId;
            model.Image = "<span class=\"avatar avatar-online\"><img src=\"" + _erpManagerSettings.AppPicturePath + coolApp.Id + "\"/></span>";
            if (fillAvailableCompanies)
            {
                model.AvailableCompanies = _coolComService.GetAll().Select(k => new SelectListItem { Text = k.Id, Value = k.Id }).ToList();
            }

            return model;
        }
    }
}