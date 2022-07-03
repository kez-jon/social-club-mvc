using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SocialClub.Web.Models;

namespace SocialClub.Web.Controllers
{
    public class ClubEventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClubEventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ClubEvents
        public async Task<IActionResult> Index()
        {
              return View(await _context.ClubEvent.ToListAsync());
        }

        // GET: ClubEvents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ClubEvent == null)
            {
                return NotFound();
            }

            var clubEvent = await _context.ClubEvent
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (clubEvent == null)
            {
                return NotFound();
            }

            return View(clubEvent);
        }

        // GET: ClubEvents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClubEvents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId,EventName,StartDate,EndDate,Venue,HostName,EventStatus,EventDescription")] ClubEvent clubEvent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clubEvent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clubEvent);
        }

        // GET: ClubEvents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ClubEvent == null)
            {
                return NotFound();
            }

            var clubEvent = await _context.ClubEvent.FindAsync(id);
            if (clubEvent == null)
            {
                return NotFound();
            }
            return View(clubEvent);
        }

        // POST: ClubEvents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventId,EventName,StartDate,EndDate,Venue,HostName,EventStatus,EventDescription")] ClubEvent clubEvent)
        {
            if (id != clubEvent.EventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clubEvent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClubEventExists(clubEvent.EventId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(clubEvent);
        }

        // GET: ClubEvents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ClubEvent == null)
            {
                return NotFound();
            }

            var clubEvent = await _context.ClubEvent
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (clubEvent == null)
            {
                return NotFound();
            }

            return View(clubEvent);
        }

        // POST: ClubEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ClubEvent == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ClubEvent'  is null.");
            }
            var clubEvent = await _context.ClubEvent.FindAsync(id);
            if (clubEvent != null)
            {
                _context.ClubEvent.Remove(clubEvent);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClubEventExists(int id)
        {
          return _context.ClubEvent.Any(e => e.EventId == id);
        }
    }
}
