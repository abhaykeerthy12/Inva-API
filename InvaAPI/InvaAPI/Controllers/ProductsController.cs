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

namespace InvaAPI.Controllers
{
    [Authorize]
    public class ProductsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Products
        public IHttpActionResult GetProducts()
        {
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();

            var product = db.Products
                            .Select(p => new
                            {
                                CurrentUserId = userId,
                                Id = p.Id,
                                Name = p.Name,
                                Type = p.Type,
                                Quantity = p.Quantity,
                                Price = p.Price
                            });

            return Ok(product);
        }

        // GET: api/Products/5
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> GetProduct(Guid id)
        {
            Product product = await db.Products.FindAsync(id).ConfigureAwait(false);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/Products/5
        [OverrideAuthentication]
        [Authorize(Roles = "Admin")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProduct(Guid id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            if (id != product.Id)
            {
                return BadRequest();
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/Products
        [OverrideAuthentication]
        [Authorize(Roles = "Admin")]
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            db.Products.Add(product);
            await db.SaveChangesAsync().ConfigureAwait(false);

            return CreatedAtRoute("DefaultApi", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [OverrideAuthentication]
        [Authorize(Roles = "Admin")]
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> DeleteProduct(Guid id)
        {
            Product product = await db.Products.FindAsync(id).ConfigureAwait(false);
            if (product == null)
            {
                return NotFound();
            }

            db.Products.Remove(product);
            await db.SaveChangesAsync().ConfigureAwait(false);

            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(Guid id)
        {
            return db.Products.Any(e => e.Id == id);
        }
    }
}