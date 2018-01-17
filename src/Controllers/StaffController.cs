using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace src.Controllers
{
    public class StaffController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<ApplicationUser> _userManager;

        public StaffController(ApplicationDbContext context, IAuthorizationService authorizationService, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _authorizationService = authorizationService;
        }

        // GET: Staff
        public async Task<IActionResult> Index()
        {
            return View(await _context.Staff.ToListAsync());
        }

        // GET: Staff/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _context.Staff
                .SingleOrDefaultAsync(m => m.Id == Convert.ToInt32(id));
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        // GET: Staff/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Staff/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ownerID,hoursContracted,accountNumber,sortCode,nationalInsuranceNumber")] Staff staff)
        {
            if (!ModelState.IsValid)
            {
                return View(staff);
            }

            staff.ownerID = _userManager.GetUserId(User);

            _context.Add(staff);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Staff/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _context.Staff.SingleOrDefaultAsync(m => m.Id == Convert.ToInt32(id));
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        // POST: Staff/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ownerID,hoursContracted,accountNumber,sortCode,nationalInsuranceNumber")] Staff staff)
        {
            if (id != staff.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(staff);
            }
            
            var toEdit = await _context.Staff.SingleOrDefaultAsync(m => m.Id == id);
            if(toEdit == null)
            {
                return NotFound();
            }

            _context.Update(staff);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // GET: Staff/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _context.Staff
                .SingleOrDefaultAsync(m => m.Id == Convert.ToInt32(id));
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
            var staff = await _context.Staff.SingleOrDefaultAsync(m => m.Id == Convert.ToInt32(id));
            _context.Staff.Remove(staff);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaffExists(int id)
        {
            return _context.Staff.Any(e => e.Id == Convert.ToInt32(id));
        }
    }
}
