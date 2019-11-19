using InvaAPI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InvaAPI.Controllers
{
    public class RolesController : ApiController
    {

        [HttpGet]
        // GET: /api/roles
        public IHttpActionResult GetRoles()
        {
            var roleStore = new RoleStore<IdentityRole>(new ApplicationDbContext());
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var roles = roleManager.Roles
                        .Select(r => new 
                            { 
                                Id = r.Id,
                                Name = r.Name
                            } ) ;
            return Ok(roles);
        }

        //[HttpGet]
        //[Route("api/userroles/{id}")]
        //// GET: /api/roles
        //public IHttpActionResult GetUserRoles(Guid Id)
        //{
        //    var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
        //    var userManager = new UserManager<ApplicationUser>(userStore);

        //    var userroles = userManager.GetRoles(Id);
        //    return Ok(userroles);
        //}


    }
}
