using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ERPManager.Web.Models.CoolUsers;
using ERPManager.Service.Abstract;
using ERPManager.Core.Settings;
using ERPManager.Web.Controllers.Base;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Rendering;
using ERPManager.Entities.Model;

namespace ERPManager.Web.Controllers
{
    [Authorize]
    public class CoolUserController : BaseController
    {
        private ICoolUserService _coolUserService;
        private ICoolComService _coolComService;
        private ILanguageService _languageService;
        private ErpManagerSettings _erpManagerSettings;
        private int pageSize = 15;
        public CoolUserController(ICoolUserService coolUserService, ICoolComService coolComService, ILanguageService languageService, ErpManagerSettings erpManagerSettings)
        {
            _coolUserService = coolUserService;
            _coolComService = coolComService;
            _languageService = languageService;
            _erpManagerSettings = erpManagerSettings;
        }


        public ActionResult Index( )
        {
            CoolUserListModel model = new CoolUserListModel();

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

            var coolUsers = _coolUserService.SearchCoolUser(out totalRecords, start, length, search, sortColumn, sortColumnDir == "asc"); //GetUserAddresses(user.Id, out totalRecords, start, length, search, sortColumn, sortColumnDir == "asc");
            CoolUserListModel model = new CoolUserListModel();
            model = PrepareCoolUserListModel(model, coolUsers);
            return Json(new { draw = draw, recordsFiltered = totalRecords, recordsTotal = totalRecords, data = model.Items });
        }


        public ActionResult Create()
        {
            CoolUserModel model = new CoolUserModel();
            
            model = PrepareCoolUserModel(model, new CoolUser(), true);
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CoolUserModel model)
        {

            CoolUser coolUser = _coolUserService.GetById(model.Id);

            if (coolUser != null)
            {
                model.Errors.Add(_languageService.GetLocaleString("CoolUser Already Exists"));
                return Json(model);
            }

            if( _coolUserService.GetAll().Where(k=> k.CoolUserId == model.CoolUserId && k.CoolComId == model.CompanyId).Count() >0)
            {
                model.Errors.Add(_languageService.GetLocaleString("CoolUser Already Exists"));
                return Json(model);
            }

            if (ModelState.IsValid)
            {
                coolUser = new CoolUser();
                coolUser.CoolComId = model.CompanyId;
                coolUser.CoolUserId = model.CoolUserId;
                coolUser.UserId = model.UserId;
                _coolUserService.Insert(coolUser);
                model.SuccessMessage = _languageService.GetLocaleString("CoolUser Created");
            }
            else
            {
                model.Errors.Add("Check fields for editing");
            }

            return Json(model);

        }


        public ActionResult Edit(string Id)
        {
            CoolUserModel model = new CoolUserModel();
            var coolUser = _coolUserService.GetById(Id);


            if (coolUser == null)
            {
                model.Errors.Add(_languageService.GetLocaleString("CoolApp not found"));
                return View(model);
            }

            model = PrepareCoolUserModel(model, coolUser, true);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CoolUserModel model)
        {
            var coolUser = _coolUserService.GetById(model.Id);
            if (coolUser == null)
            {
                model.Errors.Add(_languageService.GetLocaleString("coolUser not found"));
                return Json(model);
            }
            if (ModelState.IsValid)
            {
                coolUser.CoolComId = model.CompanyId;
                coolUser.CoolUserId = model.CoolUserId;
                coolUser.UserId = model.UserId;
                _coolUserService.Update(coolUser);
                model.SuccessMessage = _languageService.GetLocaleString("CoolUser Updated");
            }
            else
            {
                model.Errors.Add("Check fields for editing");
            }
            return Json(model);
        }


        public ActionResult Delete(string Id)
        {

            CoolUserModel model = new CoolUserModel();
            var coolUser = _coolUserService.GetById(Id);
            if (coolUser != null)
            {
                _coolUserService.Delete(Id);
            }
            else
            {
                model.Errors.Add(_languageService.GetLocaleString("CoolUser Couldn't found"));
            }

            model.SuccessMessage = _languageService.GetLocaleString("CoolUser Deleted successfully");
            return Json(model);
        }

        public CoolUserListModel PrepareCoolUserListModel(CoolUserListModel model, List<CoolUser> coolUsers)
        {
            foreach (var c in coolUsers)
            {
                CoolUserModel ccm = new CoolUserModel();
                ccm = PrepareCoolUserModel(ccm, c);
                model.Items.Add(ccm);
            }
            model.AvailableCompanies = _coolComService.GetAll().Select(k => new SelectListItem { Text = k.Id, Value = k.Id }).ToList();

            return model;


        }

        public CoolUserModel PrepareCoolUserModel(CoolUserModel model, CoolUser coolUser, bool fillAvailableCompanies = false)
        {
            model.Id = coolUser.Id;
            model.CompanyId = coolUser.CoolComId;
            model.CoolProfilePictureUrl = "<span class=\"avatar avatar-online\"><img src=\"" + _erpManagerSettings.UserProfilePath + coolUser.CoolUserId + "\"/></span>";
            model.CoolUserId = coolUser.CoolUserId;
            model.UserId = coolUser.UserId;
            model.CreatedDate = coolUser.CreatedDate;//.ToString("dd.MM.yyyy HH:mm:ss");
            if (fillAvailableCompanies)
            {
                model.AvailableCompanies =  _coolComService.GetAll().Select(k => new SelectListItem { Text = k.Id, Value = k.Id }).ToList();
            }
            return model;
        }

    }
}