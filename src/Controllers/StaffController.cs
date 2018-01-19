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
	[Authorize(Roles = "Manager, Admin")]
	public class StaffController : Controller
	{
		private readonly ApplicationDbContext _context;

		public StaffController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: Staff
		public async Task<IActionResult> Index()
		{
			var applicationDbContext = _context.Staff.Include(s => s.ApplicationUser);
			return View(await applicationDbContext.ToListAsync());
		}

		// GET: Staff/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var staff = await _context.Staff
				.Include(s => s.ApplicationUser)
				.SingleOrDefaultAsync(m => m.StaffID == id);
			if (staff == null)
			{
				return NotFound();
			}

			return View(staff);
		}

		// GET: Staff/Create
		public IActionResult Create()
		{
			ViewData["ApplicationUserID"] = new SelectList(_context.Users, "UserName", "UserName");
			return View();
		}

		// POST: Staff/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("StaffID,hoursContracted,accountNumber,sortCode,nationalInsuranceNumber")] Staff staff, string ApplicationUserID)
		{
			staff.ApplicationUserID = _context.Users.Where(n => n.UserName.Equals(ApplicationUserID)).SingleOrDefault().Id;

			if (ModelState.IsValid)
			{
				_context.Add(staff);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			ViewData["ApplicationUserID"] = new SelectList(_context.Users, "UserName", "UserName", ApplicationUserID);
			return View(staff);
		}

		// GET: Staff/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var staff = await _context.Staff.SingleOrDefaultAsync(m => m.StaffID == id);
			if (staff == null)
			{
				return NotFound();
			}
			ViewData["ApplicationUserID"] = new SelectList(_context.Users, "UserName", "UserName", _context.Users.Where(i => i.Id.Equals(staff.ApplicationUserID)).SingleOrDefault().UserName);
			return View(staff);
		}

		// POST: Staff/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("StaffID,hoursContracted,accountNumber,sortCode,nationalInsuranceNumber")] Staff staff, string ApplicationUserID)
		{
			if (id != staff.StaffID)
			{
				return NotFound();
			}

			staff.ApplicationUserID = _context.Users.Where(n => n.NormalizedUserName.Equals(ApplicationUserID)).SingleOrDefault().Id;

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(staff);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!StaffExists(staff.StaffID))
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
			ViewData["ApplicationUserID"] = new SelectList(_context.Users, "UserName", "UserName", ApplicationUserID);
			return View(staff);
		}

		// GET: Staff/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var staff = await _context.Staff
				.Include(s => s.ApplicationUser)
				.SingleOrDefaultAsync(m => m.StaffID == id);
			if (staff == null)
			{
				return NotFound();
			}

			return View(staff);
		}

		// POST: Staff/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var staff = await _context.Staff.SingleOrDefaultAsync(m => m.StaffID == id);
			_context.Staff.Remove(staff);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool StaffExists(int id)
		{
			return _context.Staff.Any(e => e.StaffID == id);
		}
	}
}
