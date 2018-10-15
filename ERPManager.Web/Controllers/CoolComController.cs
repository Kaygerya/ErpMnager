using ERPManager.Core.Settings;
using ERPManager.Entities.Model;
using ERPManager.Service.Abstract;
using ERPManager.Web.Models.CoolCompany;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPManager.Web.Controllers
{

    public class CoolComController : Controller
    {

        private ICoolUserService _coolUserService;
        private ICoolComService _coolComService;
        private ErpManagerSettings _erpManagerSettings;
        private ILanguageService _languageService;
        private IHttpContextAccessor _httpContextAccessor;
        private int pageSize = 15;
        public CoolComController(ICoolUserService coolUserService, ICoolComService coolComService, ErpManagerSettings erpManagerSettings, IHttpContextAccessor httpContextAccessor, ILanguageService languageService)
        {
            _coolUserService = coolUserService;
            _coolComService = coolComService;
            _erpManagerSettings = erpManagerSettings;
            _languageService = languageService;
            _httpContextAccessor = httpContextAccessor;
        }


        public ActionResult Index()
        {
            CoolComListModel model = new CoolComListModel();
           
            return View(model);
        }

        [HttpPost]
        [Produces("application/json")]
        public ActionResult LoadTable()
        {


            var draw =Request.Form["draw"].FirstOrDefault();
            var start = Convert.ToInt32(Request.Form["start"].FirstOrDefault());
            var length = Convert.ToInt32(Request.Form["length"].FirstOrDefault());
            string search = Request.Form["search[value]"][0];
            //Get Sort columns value
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDir = Request.Form["order[0][dir]"].FirstOrDefault();
            long totalRecords = 0;

            var coolComs = _coolComService.SearchCoolCom(out totalRecords, start, length, search, sortColumn, sortColumnDir == "asc"); //GetUserAddresses(user.Id, out totalRecords, start, length, search, sortColumn, sortColumnDir == "asc");
            CoolComListModel model = new CoolComListModel();
            PrepareCoolComListModel(model, coolComs);
            return Json(new { draw = draw, recordsFiltered = totalRecords, recordsTotal = totalRecords, data = model.Items });

        }



        public ActionResult Create()
        {
            CoolComModel model = new CoolComModel();

            model = PrepareCoolComModel(model, new CoolCom());
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CoolComModel model)
        {

            CoolCom coolCom = _coolComService.GetByCompanyId(model.CompanyId).FirstOrDefault();

            if (coolCom != null)
            {
                model.Errors.Add(_languageService.GetLocaleString("Coolcom Already Exists"));
                return Json(model);
            }

            if (ModelState.IsValid)
            {
                coolCom = new CoolCom();
                coolCom.Id = model.CompanyId;
                coolCom.CoolCompanyId = model.CoolCompanyId;
                _coolComService.Insert(coolCom);
                model.SuccessMessage = _languageService.GetLocaleString("CoolCom Created");
            }
            else
            {
                model.Errors.Add("Check fields for editing");
            }
            return Json(model);

        }


        public ActionResult Edit(string Id)
        {
            CoolComModel model = new CoolComModel();
            var coolCom = _coolComService.GetCoolComById(Id);

            if (coolCom == null)
            {
                model.Errors.Add(_languageService.GetLocaleString("CoolCom not found"));
                return View(model);
            }
            model = PrepareCoolComModel(model, coolCom);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CoolComModel model)
        {
            var coolCom = _coolComService.GetCoolComById(model.Id);
            if (coolCom == null)
            {
                model.Errors.Add(_languageService.GetLocaleString("CoolCom not found"));
                return Json(model);
            }
            if (ModelState.IsValid)
            {
                coolCom.Id = model.CompanyId;
                coolCom.CoolCompanyId = model.CoolCompanyId;
                _coolComService.Update(coolCom);
            //model = PrepareCoolComModel(model, coolCom);

            model.SuccessMessage = _languageService.GetLocaleString("CoolCom Updated");
            }
            else
            {
                model.Errors.Add("Check fields for editing");
            }
            return Json(model);
        }

        public CoolComListModel PrepareCoolComListModel(CoolComListModel model, List<CoolCom> coolCom)
        {
           foreach(var c in coolCom)
            {
                CoolComModel ccm = new CoolComModel();
                ccm = PrepareCoolComModel(ccm, c);
                model.Items.Add(ccm);
            }

            return model;
        }

        public CoolComModel PrepareCoolComModel(CoolComModel model, CoolCom coolCom)
        {
            //model.Id = coolCom.Id;
            model.CompanyId = coolCom.Id;
            model.CompanyImagePath = "<img src=\"" + _erpManagerSettings.CompanyImagePath + coolCom.CoolCompanyId + "\"/>";
            model.CoolCompanyId = coolCom.CoolCompanyId;
            model.CreatedDate = coolCom.CreatedDate;//.ToString("dd.MM.yyyy HH:mm:ss");
            return model;
        }


        public ActionResult Delete(string Id)
        {
            CoolComModel model = new CoolComModel();
            var coolCom = _coolComService.GetCoolComById(Id);
            if (coolCom != null)
            {
                _coolComService.Delete(Id);
            }
            else
            {
                model.Errors.Add(_languageService.GetLocaleString("CoolCom Couldn't found"));
            }

            model.SuccessMessage = _languageService.GetLocaleString("CoolCom Deleted successfully");
            return Json(model);
        }
    }
}
