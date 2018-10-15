
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPManager.Web.Controllers.Base
{
    public class BaseController : Controller
    {
        public int _pageIndex { get; set; }
        public int _pageSize { get; set; }
        //public PagingSettings _pagingSettings;

        //public BaseController(PagingSettings pagingSettings)
        //{
        //    _pagingSettings = pagingSettings;
        //}
    }
}
