using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.Models;

namespace src.Controllers
{
    [Authorize(Roles = "Manager, Admin")]
    public class RouteStops : Controller
    {
        private readonly ApplicationDbContext _context;

        public RouteStops(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RouteStops
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RouteStops.Include(r => r.Route).Include(r => r.Stop);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RouteStops/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routeStop = await _context.RouteStops
                .Include(r => r.Route)
                .Include(r => r.Stop)
                .SingleOrDefaultAsync(m => m.RouteStopID == id);
            if (routeStop == null)
            {
                return NotFound();
            }

            return View(routeStop);
        }

        // GET: RouteStops/Create
        public IActionResult Create()
        {
            ViewData["RouteID"] = new SelectList(_context.Routes, "Name", "Name");
            ViewData["StopID"] = new SelectList(_context.Stops, "Name", "Name");
            return View();
        }

        // POST: RouteStops/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RouteStopID")] RouteStop routeStop, string RouteID, string StopID)
        {
            routeStop.RouteID = _context.Routes.Where(n => n.Name == RouteID).SingleOrDefault().RouteID;
            routeStop.StopID = _context.Stops.Where(n => n.Name == StopID).SingleOrDefault().StopID;

            if (ModelState.IsValid)
            {
                _context.Add(routeStop);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RouteID"] = new SelectList(_context.Routes, "Name", "Name", RouteID);
            ViewData["StopID"] = new SelectList(_context.Stops, "Name", "Name", StopID);
            return View(routeStop);
        }

        // GET: RouteStops/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routeStop = await _context.RouteStops.SingleOrDefaultAsync(m => m.RouteStopID == id);
            if (routeStop == null)
            {
                return NotFound();
            }
            ViewData["RouteID"] = new SelectList(_context.Routes, "Name", "Name", _context.Routes.Where(i => i.RouteID == routeStop.RouteID).SingleOrDefault().Name);
            ViewData["StopID"] = new SelectList(_context.Stops, "Name", "Name", _context.Stops.Where(i => i.StopID == routeStop.StopID).SingleOrDefault().Name);
            return View(routeStop);
        }

        // POST: RouteStops/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RouteStopID")] RouteStop routeStop, string RouteID, string StopID)
        {
            if (id != routeStop.RouteStopID)
            {
                return NotFound();
            }

            routeStop.RouteID = _context.Routes.Where(n => n.Name == RouteID).SingleOrDefault().RouteID;
            routeStop.StopID = _context.Stops.Where(n => n.Name == StopID).SingleOrDefault().StopID;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(routeStop);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RouteStopExists(routeStop.RouteStopID))
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
            ViewData["RouteID"] = new SelectList(_context.Routes, "Name", "Name", RouteID);
            ViewData["StopID"] = new SelectList(_context.Stops, "Name", "Name", StopID);
            return View(routeStop);
        }

        // GET: RouteStops/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routeStop = await _context.RouteStops
                .Include(r => r.Route)
                .Include(r => r.Stop)
                .SingleOrDefaultAsync(m => m.RouteStopID == id);
            if (routeStop == null)
            {
                return NotFound();
            }

            return View(routeStop);
        }

        // POST: RouteStops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var routeStop = await _context.RouteStops.SingleOrDefaultAsync(m => m.RouteStopID == id);
            _context.RouteStops.Remove(routeStop);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RouteStopExists(int id)
        {
            return _context.RouteStops.Any(e => e.RouteStopID == id);
        }
    }
}
