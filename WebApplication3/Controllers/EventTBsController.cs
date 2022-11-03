using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class EventTBsController : ApiController
    {
        private MVCAPPEntities1 db = new MVCAPPEntities1();

        // GET: api/EventTBs
        public IQueryable<EventTB> GetEventTBs()
        {
            return db.EventTBs;
        }

        // GET: api/EventTBs/5
        [ResponseType(typeof(EventTB))]
        public IHttpActionResult GetEventTB(int id)
        {
            EventTB eventTB = db.EventTBs.Find(id);
            if (eventTB == null)
            {
                return NotFound();
            }

            return Ok(eventTB);
        }

        // PUT: api/EventTBs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEventTB(int id, EventTB eventTB)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eventTB.EventID)
            {
                return BadRequest();
            }

            db.Entry(eventTB).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventTBExists(id))
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

        // POST: api/EventTBs
        [ResponseType(typeof(EventTB))]
        public IHttpActionResult PostEventTB(EventTB eventTB)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EventTBs.Add(eventTB);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EventTBExists(eventTB.EventID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = eventTB.EventID }, eventTB);
        }

        // DELETE: api/EventTBs/5
        [ResponseType(typeof(EventTB))]
        public IHttpActionResult DeleteEventTB(int id)
        {
            EventTB eventTB = db.EventTBs.Find(id);
            if (eventTB == null)
            {
                return NotFound();
            }

            db.EventTBs.Remove(eventTB);
            db.SaveChanges();

            return Ok(eventTB);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EventTBExists(int id)
        {
            return db.EventTBs.Count(e => e.EventID == id) > 0;
        }
    }
}