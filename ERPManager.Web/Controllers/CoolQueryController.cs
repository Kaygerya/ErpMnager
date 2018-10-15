using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ERPManager.Service.Abstract;
using ERPManager.Core.Settings;
using Microsoft.AspNetCore.Http;
using ERPManager.Web.Models.CoolQueries;
using ERPManager.Entities.Model;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ERPManager.Web.Controllers
{
    [Authorize]
    public class CoolQueryController : Controller
    {
        private ICoolComService _coolComService;
        private ICoolQueryService _coolQueryService;

        private ErpManagerSettings _erpManagerSettings;
        private ILanguageService _languageService;
        private IHttpContextAccessor _httpContextAccessor;
        private int pageSize = 15;
        public CoolQueryController(ICoolQueryService coolQueryService, ICoolComService coolComService, ErpManagerSettings erpManagerSettings, IHttpContextAccessor httpContextAccessor, ILanguageService languageService)
        {
            _coolComService = coolComService;
            _erpManagerSettings = erpManagerSettings;
            _languageService = languageService;
            _httpContextAccessor = httpContextAccessor;
            _coolQueryService = coolQueryService;
        }

        public ActionResult Index()
        {
            CoolQueryListModel model = new CoolQueryListModel();

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

            var coolQueries = _coolQueryService.SearchCoolQuery(out totalRecords, start, length, search, sortColumn, sortColumnDir == "asc"); //GetUserAddresses(user.Id, out totalRecords, start, length, search, sortColumn, sortColumnDir == "asc");
            CoolQueryListModel model = new CoolQueryListModel();
            PrepareCoolQueryListModel(model, coolQueries);
            return Json(new { draw = draw, recordsFiltered = totalRecords, recordsTotal = totalRecords, data = model.Items });

        }



        public ActionResult Create()
        {
            CoolQueryModel model = new CoolQueryModel();

            model = PrepareCoolQueryModel(model, new CoolQuery(), true, true);
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CoolQueryModel model)
        {

            CoolQuery coolQuery = _coolQueryService.GetById(model.Id);

            if (coolQuery != null)
            {
                model.Errors.Add(_languageService.GetLocaleString("CoolQuery Already Exists"));
                return Json(model);
            }

            if (ModelState.IsValid)
            {
                coolQuery = new CoolQuery();
                coolQuery.CoolComId = model.CompanyId;
                coolQuery.Description = model.Description;
                coolQuery.QueryCode = model.QueryCode;
                coolQuery.CoolQueryDirection = model.CoolQueryDirection;
                coolQuery.Query = model.Query;
                _coolQueryService.Insert(coolQuery);
                model.SuccessMessage = _languageService.GetLocaleString("CoolQuery Created");
            }

            return Json(model);

        }


        public ActionResult Edit(string Id)
        {
            CoolQueryModel model = new CoolQueryModel();
            var coolQuery = _coolQueryService.GetById(Id);

            if (coolQuery == null)
            {
                model.Errors.Add(_languageService.GetLocaleString("CoolQuery not found"));
                return View(model);
            }
            model = PrepareCoolQueryModel(model, coolQuery, true, true);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CoolQueryModel model)
        {
            var coolQuery = _coolQueryService.GetById(model.Id);
            if (coolQuery == null)
            {
                model.Errors.Add(_languageService.GetLocaleString("CoolQuery not found"));
                return Json(model);
            }

            if (ModelState.IsValid)
            {
                coolQuery.CoolComId = model.CompanyId;
                coolQuery.Description = model.Description;
                coolQuery.QueryCode = model.QueryCode;
                coolQuery.Query = model.Query;
                coolQuery.CoolQueryDirection = model.CoolQueryDirection;

                _coolQueryService.Update(coolQuery);
                model.SuccessMessage = _languageService.GetLocaleString("CoolQuery Updated");
            }
            else
            {
                model.Errors.Add("Check fields for editing");
            }

            //model = PrepareCoolQueryModel(model, coolCom);

         
            return Json(model);
        }

        public CoolQueryListModel PrepareCoolQueryListModel(CoolQueryListModel model, List<CoolQuery> coolQuery)
        {
            foreach (var c in coolQuery)
            {
                CoolQueryModel cqm = new CoolQueryModel();
                cqm = PrepareCoolQueryModel(cqm, c);
                model.Items.Add(cqm);
            }

            return model;
        }

        public CoolQueryModel PrepareCoolQueryModel(CoolQueryModel model, CoolQuery coolQuery, bool fillAvailableCompanies = false, bool fillAvailableDirections = false)
        {
            model.Id = coolQuery.Id;
            model.CompanyId = coolQuery.CoolComId;
            model.Description = coolQuery.Description;
            model.QueryCode = coolQuery.QueryCode;
            model.CoolQueryDirection = coolQuery.CoolQueryDirection;
            model.Query = coolQuery.Query;
            model.CreatedDate = coolQuery.CreatedDate;

            if (fillAvailableCompanies)
            {
                model.AvailableCompanies = _coolComService.GetAll().Select(k => new SelectListItem { Text = k.Id, Value = k.Id }).ToList();
            }

            if(fillAvailableDirections)
            {
                model.AvailableDirections =  Enum.GetValues(typeof(CoolQueryDirection)).Cast<CoolQueryDirection>().Select(v => new SelectListItem
                {
                    Text = v.ToString(),
                    Value = ((int)v).ToString()
                }).ToList();
            }

            return model;
        }


        public ActionResult Delete(string Id)
        {
            CoolQueryModel model = new CoolQueryModel();
            var coolCom = _coolQueryService.GetById(Id);
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