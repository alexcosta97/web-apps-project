using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.Models;
using System.Linq;
using System.Threading.Tasks;

namespace src.Controllers
{
	[Authorize]
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
			var applicationDbContext = _context.Favourites.Include(f => f.Line).Include(f => f.route).Include(a => a.ApplicationUser);
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
				.Include(a => a.ApplicationUser)
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
			ViewData["LineID"] = new SelectList(_context.Lines, "Name", "Name");
			ViewData["RouteID"] = new SelectList(_context.Routes, "Name", "Name");
			ViewData["ApplicationUserID"] = new SelectList(_context.Users, "UserName", "UserName");
			return View();
		}

		// POST: Favourites/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("FavouriteID,Name")] Favourite favourite, string RouteID, string LineID, string ApplicationUserID)
		{
			favourite.ApplicationUserID = _context.Users.Where(n => n.UserName == ApplicationUserID).SingleOrDefault().Id;
			favourite.RouteID = _context.Routes.Where(n => n.Name == RouteID).SingleOrDefault().RouteID;
			favourite.LineID = _context.Lines.Where(n => n.Name == LineID).SingleOrDefault().LineID;
			if (ModelState.IsValid)
			{
				_context.Add(favourite);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			ViewData["LineID"] = new SelectList(_context.Lines, "Name", "Name", LineID);
			ViewData["RouteID"] = new SelectList(_context.Routes, "Name", "Name", RouteID);
			ViewData["ApplicationUserID"] = new SelectList(_context.Users, "UserName", "UserName", ApplicationUserID);
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
			ViewData["LineID"] = new SelectList(_context.Lines, "Name", "Name", _context.Lines.Where(n => n.LineID == favourite.LineID).SingleOrDefault().Name);
			ViewData["RouteID"] = new SelectList(_context.Routes, "Name", "Name", _context.Routes.Where(n => n.RouteID == favourite.RouteID).SingleOrDefault().Name);
			ViewData["ApplicationUserID"] = new SelectList(_context.Users, "UserName", "UserName", _context.Users.Where(u => u.Id == favourite.ApplicationUserID).SingleOrDefault().UserName);
			return View(favourite);
		}

		// POST: Favourites/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("FavouriteID,Name")] Favourite favourite, string RouteID, string LineID, string ApplicationUserID)
		{
			if (id != favourite.FavouriteID)
			{
				return NotFound();
			}

			favourite.ApplicationUserID = _context.Users.Where(n => n.UserName == ApplicationUserID).SingleOrDefault().Id;
			favourite.RouteID = _context.Routes.Where(n => n.Name == RouteID).SingleOrDefault().RouteID;
			favourite.LineID = _context.Lines.Where(n => n.Name == LineID).SingleOrDefault().LineID;

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
			ViewData["LineID"] = new SelectList(_context.Lines, "Name", "Name", LineID);
			ViewData["RouteID"] = new SelectList(_context.Routes, "Name", "Name", RouteID);
			ViewData["ApplicationUserID"] = new SelectList(_context.Users, "UserName", "UserName", ApplicationUserID);
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
				.Include(a => a.ApplicationUser)
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
