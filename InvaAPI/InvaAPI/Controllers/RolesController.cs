using InvaAPI.Models;
using InvaAPI.Models.ProjectModels;
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
    [Authorize]
    [RoutePrefix("api/roles")]
    public class RolesController : ApiController
    {

        [HttpGet]
        // GET: /api/roles
        public IHttpActionResult GetRoles()
        {
            var roleContext = new ApplicationDbContext();

            var roles = roleContext.Roles
                        .Select(r => new
                        {
                            Id = r.Id,
                            Name = r.Name
                        });
            return Ok(roles);
        }

        [Route("GetUserRoles")]
        // GET: /api/roles
        public IHttpActionResult GetUserRoles()
        {
            var identityDbContext = new IdentityDbContext();

            var identityRole = identityDbContext.Users.Select(r => new{ 
                                                        Id = r.Id,
                                                        Roles = r.Roles,
                                                        Email = r.Email
                                                    });
            return Ok(identityRole);
        }

        // make a user admin
        [HttpPost]
        // POST: /api/roles
        public IHttpActionResult PostRoles(UserRoleClass userRole)
        {
            var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var manager = new UserManager<ApplicationUser>(userStore);
            if (userRole.Role == "Admin")
            {
                manager.AddToRole(userRole.Id, userRole.Role);
            }
            else if(userRole.Role == "User")
            {
                manager.RemoveFromRole(userRole.Id, "Admin");
            }
            return Ok();
        }




    }
}
