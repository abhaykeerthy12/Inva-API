using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using InvaAPI.Models;
using InvaAPI.Models.ProjectModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace InvaAPI.Controllers
{
    [Authorize]
    public class RequestsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Requests
        public IHttpActionResult GetRequest()
        {
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();

            var request = db.Requests
                            .Select(r => new
                            {
                                RequestId = r.Id,
                                EmployeeId = r.EmployeeId,
                                CurrentUserId = userId,
                                ProductId = r.ProductId,
                                Quantity = r.Quantity,
                                Status = r.Status,
                                RequestedDate = r.RequestedDate
                            });

            return Ok(request);
        }

        // GET: api/Requests/5
        [ResponseType(typeof(Request))]
        public async Task<IHttpActionResult> GetRequest(Guid id)
        {
            Request request = await db.Requests.FindAsync(id).ConfigureAwait(false);
            if (request == null)
            {
                return NotFound();
            }

            return Ok(request);
        }

        // PUT: api/Requests/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRequest(Guid id, Request request)
        {

            if(request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (id != request.Id)
            {
                return BadRequest();
            }

            db.Entry(request).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Requests
        [ResponseType(typeof(Request))]
        public async Task<IHttpActionResult> PostRequest(Request request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            db.Requests.Add(request);
            await db.SaveChangesAsync().ConfigureAwait(false);

            // sent mail to admins

            return CreatedAtRoute("DefaultApi", new { id = request.Id }, request);
        }

        // DELETE: api/Requests/5
        [ResponseType(typeof(Request))]
        public async Task<IHttpActionResult> DeleteRequest(Guid id)
        {
            Request request = await db.Requests.FindAsync(id).ConfigureAwait(true);
            if (request == null)
            {
                return NotFound();
            }

            db.Requests.Remove(request);
            await db.SaveChangesAsync().ConfigureAwait(true);

            return Ok(request);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RequestExists(Guid id)
        {
            return db.Requests.Any(e => e.Id == id);
        }
    }
}