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
    public class UserGroupsController : ApiController
    {
        private HpmgCMSEntities db = new HpmgCMSEntities();

        // GET: api/UserGroups
        public IQueryable<UserGroup> GetUserGroups()
        {
            return db.UserGroups;
        }

        // GET: api/UserGroups/5
        [ResponseType(typeof(UserGroup))]
        public async Task<IHttpActionResult> GetUserGroup(string id)
        {
            UserGroup userGroup = await db.UserGroups.FindAsync(id);
            if (userGroup == null)
            {
                return NotFound();
            }

            return Ok(userGroup);
        }

        // PUT: api/UserGroups/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUserGroup(string id, UserGroup userGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userGroup.GroupId)
            {
                return BadRequest();
            }

            db.Entry(userGroup).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserGroupExists(id))
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

        // POST: api/UserGroups
        [ResponseType(typeof(UserGroup))]
        public async Task<IHttpActionResult> PostUserGroup(UserGroup userGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserGroups.Add(userGroup);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserGroupExists(userGroup.GroupId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = userGroup.GroupId }, userGroup);
        }

        // DELETE: api/UserGroups/5
        [ResponseType(typeof(UserGroup))]
        public async Task<IHttpActionResult> DeleteUserGroup(string id)
        {
            UserGroup userGroup = await db.UserGroups.FindAsync(id);
            if (userGroup == null)
            {
                return NotFound();
            }

            db.UserGroups.Remove(userGroup);
            await db.SaveChangesAsync();

            return Ok(userGroup);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserGroupExists(string id)
        {
            return db.UserGroups.Count(e => e.GroupId == id) > 0;
        }
    }
}