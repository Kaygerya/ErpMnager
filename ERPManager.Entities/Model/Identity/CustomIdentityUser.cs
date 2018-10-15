using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPManager.Entities.Model.Identity
{
    public class CustomIdentityUser: IdentityUser
    {

        //private ICollection<CustomIdentityUserRole> _AspNetUserRoles;

        //public virtual ICollection<CustomIdentityUserRole> AspNetUserRoles
        //{
        //    get { return _AspNetUserRoles ?? (_AspNetUserRoles = new List<CustomIdentityUserRole>()); }
        //    protected set { _AspNetUserRoles = value; }
        //}
    }
}
