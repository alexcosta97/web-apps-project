using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.Models;

namespace src.Controllers
{
    [AllowAnonymous]
    public class Lines : Controller
    {
        private readonly ApplicationDbContext _context;

        public Lines(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Lines
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["StartSortParam"] = sortOrder == "Start" ? "start_desc" : "Start";
            ViewData["EndSortParam"] = sortOrder == "End" ? "end_desc" : "End";
            ViewData["CurrentFilter"] = searchString;

            var lines = from s in _context.Lines
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                lines = lines.Where(s => s.Name.ToLower().Contains(searchString.ToLower()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    lines = lines.OrderByDescending(s => s.Name);
                    break;
                case "Start":
                    lines = lines.OrderBy(s => s.Start);
                    break;
                case "start_desc":
                    lines = lines.OrderByDescending(s => s.Start);
                    break;
                case "End":
                    lines = lines.OrderBy(s => s.End);
                    break;
                case "end_desc":
                    lines = lines.OrderByDescending(s => s.End);
                    break;
                default:
                    lines = lines.OrderBy(s => s.Name);
                    break;
            }
            return View(await lines.AsNoTracking().ToListAsync());
        }

        // GET: Lines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var line = await _context.Lines
                .SingleOrDefaultAsync(m => m.LineID == id);
            if (line == null)
            {
                return NotFound();
            }

            return View(line);
        }

        // GET: Lines/Create
        [Authorize(Roles = "Manager, Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager, Admin")]
        public async Task<IActionResult> Create([Bind("LineID,Name,Start,End")] Line line)
        {
            if (ModelState.IsValid)
            {
                _context.Add(line);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(line);
        }

        // GET: Lines/Edit/5
        [Authorize(Roles="Manager, Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var line = await _context.Lines.SingleOrDefaultAsync(m => m.LineID == id);
            if (line == null)
            {
                return NotFound();
            }
            return View(line);
        }

        // POST: Lines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="Manager, Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("LineID,Name,Start,End")] Line line)
        {
            if (id != line.LineID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(line);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LineExists(line.LineID))
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
            return View(line);
        }

        // GET: Lines/Delete/5
        [Authorize(Roles="Manager, Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var line = await _context.Lines
                .SingleOrDefaultAsync(m => m.LineID == id);
            if (line == null)
            {
                return NotFound();
            }

            return View(line);
        }

        // POST: Lines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager, Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var line = await _context.Lines.SingleOrDefaultAsync(m => m.LineID == id);
            _context.Lines.Remove(line);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LineExists(int id)
        {
            return _context.Lines.Any(e => e.LineID == id);
        }
    }
}
