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
    public class Stops : Controller
    {
        private readonly ApplicationDbContext _context;

        public Stops(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Stops
        public async Task<IActionResult> Index()
        {
            return View(await _context.Stops.ToListAsync());
        }

        // GET: Stops/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stop = await _context.Stops
                .SingleOrDefaultAsync(m => m.StopID == id);
            if (stop == null)
            {
                return NotFound();
            }

            return View(stop);
        }

        // GET: Stops/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Stops/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StopID,Name,Longitude,Latitude")] Stop stop)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stop);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stop);
        }

        // GET: Stops/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stop = await _context.Stops.SingleOrDefaultAsync(m => m.StopID == id);
            if (stop == null)
            {
                return NotFound();
            }
            return View(stop);
        }

        // POST: Stops/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StopID,Name,Longitude,Latitude")] Stop stop)
        {
            if (id != stop.StopID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stop);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StopExists(stop.StopID))
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
            return View(stop);
        }

        // GET: Stops/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stop = await _context.Stops
                .SingleOrDefaultAsync(m => m.StopID == id);
            if (stop == null)
            {
                return NotFound();
            }

            return View(stop);
        }

        // POST: Stops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stop = await _context.Stops.SingleOrDefaultAsync(m => m.StopID == id);
            _context.Stops.Remove(stop);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StopExists(int id)
        {
            return _context.Stops.Any(e => e.StopID == id);
        }
    }
}
