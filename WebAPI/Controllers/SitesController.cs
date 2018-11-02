using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.DbEntity;

namespace WebAPI.Controllers
{
    public class SitesController : ApiController
    {
        private HpmgCMSEntities db = new HpmgCMSEntities();

        // GET: api/Sites
        public IQueryable<Site> GetSites()
        {
            return db.Sites;
        }

        // GET: api/Sites/5
        [ResponseType(typeof(Site))]
        public async Task<IHttpActionResult> GetSite(string id)
        {
            Site site = await db.Sites.FindAsync(id);
            if (site == null)
            {
                return NotFound();
            }

            return Ok(site);
        }

        // PUT: api/Sites/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSite(string id, Site site)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != site.SiteId)
            {
                return BadRequest();
            }

            db.Entry(site).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SiteExists(id))
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

        // POST: api/Sites
        [ResponseType(typeof(Site))]
        public async Task<IHttpActionResult> PostSite(Site site)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            site.SiteId = System.Guid.NewGuid().ToString();

            db.Sites.Add(site);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SiteExists(site.SiteId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = site.SiteId }, site);
        }

        // DELETE: api/Sites/5
        [ResponseType(typeof(Site))]
        public async Task<IHttpActionResult> DeleteSite(string id)
        {
            Site site = await db.Sites.FindAsync(id);
            if (site == null)
            {
                return NotFound();
            }

            db.Sites.Remove(site);
            await db.SaveChangesAsync();

            return Ok(site);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SiteExists(string id)
        {
            return db.Sites.Count(e => e.SiteId == id) > 0;
        }
    }
}