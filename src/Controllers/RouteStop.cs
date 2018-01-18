using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.Models;

namespace src.Controllers
{
    public class RouteStop : Controller
    {
        private readonly ApplicationDbContext _context;

        public RouteStop(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RouteStop
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RouteStop.Include(r => r.Route).Include(r => r.Stop);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RouteStop/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routeStop = await _context.RouteStop
                .Include(r => r.Route)
                .Include(r => r.Stop)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (routeStop == null)
            {
                return NotFound();
            }

            return View(routeStop);
        }

        // GET: RouteStop/Create
        public IActionResult Create()
        {
            ViewData["RouteID"] = new SelectList(_context.Route, "Id", "Id");
            ViewData["StopID"] = new SelectList(_context.Stop, "Id", "Id");
            return View();
        }

        // POST: RouteStop/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RouteID,StopID")] RouteStop routeStop)
        {
            if (ModelState.IsValid)
            {
                _context.Add(routeStop);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RouteID"] = new SelectList(_context.Route, "Id", "Id", routeStop.RouteID);
            ViewData["StopID"] = new SelectList(_context.Stop, "Id", "Id", routeStop.StopID);
            return View(routeStop);
        }

        // GET: RouteStop/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routeStop = await _context.RouteStop.SingleOrDefaultAsync(m => m.Id == id);
            if (routeStop == null)
            {
                return NotFound();
            }
            ViewData["RouteID"] = new SelectList(_context.Route, "Id", "Id", routeStop.RouteID);
            ViewData["StopID"] = new SelectList(_context.Stop, "Id", "Id", routeStop.StopID);
            return View(routeStop);
        }

        // POST: RouteStop/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RouteID,StopID")] RouteStop routeStop)
        {
            if (id != routeStop.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(routeStop);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RouteStopExists(routeStop.Id))
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
            ViewData["RouteID"] = new SelectList(_context.Route, "Id", "Id", routeStop.RouteID);
            ViewData["StopID"] = new SelectList(_context.Stop, "Id", "Id", routeStop.StopID);
            return View(routeStop);
        }

        // GET: RouteStop/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routeStop = await _context.RouteStop
                .Include(r => r.Route)
                .Include(r => r.Stop)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (routeStop == null)
            {
                return NotFound();
            }

            return View(routeStop);
        }

        // POST: RouteStop/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var routeStop = await _context.RouteStop.SingleOrDefaultAsync(m => m.Id == id);
            _context.RouteStop.Remove(routeStop);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RouteStopExists(int id)
        {
            return _context.RouteStop.Any(e => e.Id == id);
        }
    }
}
