using ERPManager.Web.Models.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPManager.Web.ViewComponents
{
    public class FooterViewComponent: ViewComponent
    {
        public ViewViewComponentResult Invoke()
        {
            FooterModel model = new FooterModel();
            model.Name = HttpContext.User.Identity.Name;
            return View(model);
        }
    }
}
