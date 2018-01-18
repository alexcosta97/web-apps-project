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
    public class Favourites : Controller
    {
        private readonly ApplicationDbContext _context;

        public Favourites(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Favourites
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Favourites.Include(f => f.Line).Include(f => f.route);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Favourites/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favourite = await _context.Favourites
                .Include(f => f.Line)
                .Include(f => f.route)
                .SingleOrDefaultAsync(m => m.FavouriteID == id);
            if (favourite == null)
            {
                return NotFound();
            }

            return View(favourite);
        }

        // GET: Favourites/Create
        public IActionResult Create()
        {
            ViewData["LineID"] = new SelectList(_context.Lines, "LineID", "LineID");
            ViewData["RouteID"] = new SelectList(_context.Routes, "RouteID", "RouteID");
            return View();
        }

        // POST: Favourites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FavouriteID,Name,RouteID,LineID")] Favourite favourite)
        {
            if (ModelState.IsValid)
            {
                _context.Add(favourite);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LineID"] = new SelectList(_context.Lines, "LineID", "LineID", favourite.LineID);
            ViewData["RouteID"] = new SelectList(_context.Routes, "RouteID", "RouteID", favourite.RouteID);
            return View(favourite);
        }

        // GET: Favourites/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favourite = await _context.Favourites.SingleOrDefaultAsync(m => m.FavouriteID == id);
            if (favourite == null)
            {
                return NotFound();
            }
            ViewData["LineID"] = new SelectList(_context.Lines, "LineID", "LineID", favourite.LineID);
            ViewData["RouteID"] = new SelectList(_context.Routes, "RouteID", "RouteID", favourite.RouteID);
            return View(favourite);
        }

        // POST: Favourites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FavouriteID,Name,RouteID,LineID")] Favourite favourite)
        {
            if (id != favourite.FavouriteID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(favourite);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FavouriteExists(favourite.FavouriteID))
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
            ViewData["LineID"] = new SelectList(_context.Lines, "LineID", "LineID", favourite.LineID);
            ViewData["RouteID"] = new SelectList(_context.Routes, "RouteID", "RouteID", favourite.RouteID);
            return View(favourite);
        }

        // GET: Favourites/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favourite = await _context.Favourites
                .Include(f => f.Line)
                .Include(f => f.route)
                .SingleOrDefaultAsync(m => m.FavouriteID == id);
            if (favourite == null)
            {
                return NotFound();
            }

            return View(favourite);
        }

        // POST: Favourites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var favourite = await _context.Favourites.SingleOrDefaultAsync(m => m.FavouriteID == id);
            _context.Favourites.Remove(favourite);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FavouriteExists(int id)
        {
            return _context.Favourites.Any(e => e.FavouriteID == id);
        }
    }
}
