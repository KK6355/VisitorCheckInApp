using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VisitorCheckInApp.Data;
using VisitorCheckInApp.Models;

namespace VisitorCheckInApp.Controllers
{
    public class VisitorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VisitorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Visitors
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Visitors.Include(v => v.Staff);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Visitors/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Visitors == null)
            {
                return NotFound();
            }

            var visitors = await _context.Visitors
                .Include(v => v.Staff)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visitors == null)
            {
                return NotFound();
            }

            return View(visitors);
        }

        // GET: Visitors/Create
        public IActionResult Create()
        {
            ViewData["StaffId"] = new SelectList(_context.Staff, "Id", "StaffName");
            Visitors visitor = new Visitors();
            visitor.CheckIn = DateTime.Now;
            return View(visitor);
        }

        // POST: Visitors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Business,CheckIn,CheckOut,StaffId")] Visitors visitors)
        {
            if (ModelState.IsValid)
            {
                visitors.Id = Guid.NewGuid();
                _context.Add(visitors);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StaffId"] = new SelectList(_context.Staff, "Id", "Id", visitors.StaffId);
            return View(visitors);
        }

        // GET: Visitors/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Visitors == null)
            {
                return NotFound();
            }

            var visitors = await _context.Visitors.FindAsync(id);
            if (visitors == null)
            {
                return NotFound();
            }
            ViewData["StaffId"] = new SelectList(_context.Staff, "Id", "StaffName", visitors.StaffId);
            return View(visitors);
        }

        // POST: Visitors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,FirstName,LastName,Business,CheckIn,CheckOut,StaffId")] Visitors visitors)
        {
            if (id != visitors.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(visitors);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisitorsExists(visitors.Id))
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
            ViewData["StaffId"] = new SelectList(_context.Staff, "Id", "Id", visitors.StaffId);
            return View(visitors);
        }

        // GET: Visitors/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Visitors == null)
            {
                return NotFound();
            }

            var visitors = await _context.Visitors
                .Include(v => v.Staff)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visitors == null)
            {
                return NotFound();
            }

            return View(visitors);
        }

        // POST: Visitors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Visitors == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Visitors'  is null.");
            }
            var visitors = await _context.Visitors.FindAsync(id);
            if (visitors != null)
            {
                _context.Visitors.Remove(visitors);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VisitorsExists(Guid id)
        {
          return _context.Visitors.Any(e => e.Id == id);
        }
    }
}
