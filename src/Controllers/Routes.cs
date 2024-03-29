using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace src.Controllers
{
	[Authorize(Roles = "Manager, Admin")]
	public class Routes : Controller
	{
		private readonly ApplicationDbContext _context;

		public Routes(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: Routes
		[AllowAnonymous]
		public async Task<IActionResult> Index(string sortOrder, string searchString)
		{
			ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
			ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
			ViewData["CurrentFilter"] = searchString;

			var routes = from s in _context.Routes select s;
			if (!String.IsNullOrEmpty(searchString))
			{
				routes = routes.Where(s => s.Name.ToLower().Contains(searchString.ToLower()) || s.Note.ToLower().Contains(searchString.ToLower()));
			}
			switch (sortOrder)
			{
				case "name_desc":
					routes = routes.OrderByDescending(s => s.Name);
					break;

				case "Date":
					routes = routes.OrderBy(s => s.Departure);
					break;

				case "date_desc":
					routes = routes.OrderByDescending(s => s.Departure);
					break;

				default:
					routes = routes.OrderBy(s => s.Name);
					break;
			}
			return View(await routes.AsNoTracking().ToListAsync());
		}

		// GET: Routes/Details/5
		[AllowAnonymous]
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var route = await _context.Routes
				.SingleOrDefaultAsync(m => m.RouteID == id);
			if (route == null)
			{
				return NotFound();
			}

			return View(route);
		}

		// GET: Routes/Create
		public IActionResult Create()
		{
			ViewData["DriverID"] = new SelectList(_context.Staff, "StaffID", "StaffID");
			ViewData["LineID"] = new SelectList(_context.Lines, "LineID", "LineID");
			return View();
		}

		// POST: Routes/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("RouteID,DriverID,LineID,Name,Note,Departure,Arrival,Direction")] Route route)
		{
			if (ModelState.IsValid)
			{
				_context.Add(route);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			ViewData["DriverID"] = new SelectList(_context.Staff, "StaffID", "DriverID", route.DriverID);
			ViewData["LineID"] = new SelectList(_context.Lines, "LineID", "LineID", route.LineID);
			return View(route);
		}

		// GET: Routes/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var route = await _context.Routes.SingleOrDefaultAsync(m => m.RouteID == id);
			if (route == null)
			{
				return NotFound();
			}
			ViewData["DriverID"] = new SelectList(_context.Staff, "StaffID", "StaffID", route.DriverID);
			ViewData["LineID"] = new SelectList(_context.Lines, "LineID", "LineID", route.LineID);
			return View(route);
		}

		// POST: Routes/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("RouteID,DriverID,LineID,Name,Note,Departure,Arrival,Direction")] Route route)
		{
			if (id != route.RouteID)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(route);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!RouteExists(route.RouteID))
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
			return View(route);
		}

		// GET: Routes/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var route = await _context.Routes
				.SingleOrDefaultAsync(m => m.RouteID == id);
			if (route == null)
			{
				return NotFound();
			}

			return View(route);
		}

		// POST: Routes/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var route = await _context.Routes.SingleOrDefaultAsync(m => m.RouteID == id);
			_context.Routes.Remove(route);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool RouteExists(int id)
		{
			return _context.Routes.Any(e => e.RouteID == id);
		}
	}
}
