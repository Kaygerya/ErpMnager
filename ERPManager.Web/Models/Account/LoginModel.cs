using ERPManager.Entities.Model.Base;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERPManager.Web.Models.Account
{
    public class LoginModel : BaseModel
    {
        public LoginModel()
        {
            AvailableLanguages = new List<SelectListItem>();
        }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
        public string LanguageId { get; set; }
        public List<SelectListItem> AvailableLanguages { get; set; }
    }
}
