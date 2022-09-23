using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using VisitorCheckInApp.Data;
using VisitorCheckInApp.Models;

namespace VisitorCheckInApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            ViewData["StaffId"] = new SelectList(_context.Staff, "Id", "StaffName");
            Visitors visitor = new Visitors();
            visitor.CheckIn = DateTime.Now;
            return View(visitor);
           
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
        //public IActionResult Create()
        //{
        //    ViewData["StaffId"] = new SelectList(_context.Staff, "Id", "StaffName");
        //    Visitors visitor = new Visitors();
        //    visitor.CheckIn = DateTime.Now;
        //    return View(visitor);
        //}

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
    }
}