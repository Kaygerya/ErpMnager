﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ERPManager.Web.Controllers
{
    public class InstallController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}