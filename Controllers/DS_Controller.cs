using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DS.Models;

namespace DS.Controllers
{
    public class DS_Controller : Controller
    {
        private readonly ApplicationDbContext _context;

        public DS_Controller(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DS
        public ActionResult Index(string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var students = from s in _context.DS_book
                           select s;
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.Comp_num);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.Comp_num);
                    break;
                default:
                    students = students.OrderBy(s => s.Name);
                    break;
            }
            return View(students.ToList());
        }

        // GET: DS_/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dS_Model = await _context.DS_book
                .FirstOrDefaultAsync(m => m.id == id);
            if (dS_Model == null)
            {
                return NotFound();
            }

            return View(dS_Model);
        }

        // GET: DS_/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DS_/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Comp_num,Name")] DS_Model dS_Model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dS_Model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dS_Model);
        }

        // GET: DS_/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dS_Model = await _context.DS_book.FindAsync(id);
            if (dS_Model == null)
            {
                return NotFound();
            }
            return View(dS_Model);
        }

        // POST: DS_/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Comp_num,Name")] DS_Model dS_Model)
        {
            if (id != dS_Model.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dS_Model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DS_ModelExists(dS_Model.id))
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
            return View(dS_Model);
        }

        // GET: DS_/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dS_Model = await _context.DS_book
                .FirstOrDefaultAsync(m => m.id == id);
            if (dS_Model == null)
            {
                return NotFound();
            }

            return View(dS_Model);
        }

        // POST: DS_/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dS_Model = await _context.DS_book.FindAsync(id);
            _context.DS_book.Remove(dS_Model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DS_ModelExists(int id)
        {
            return _context.DS_book.Any(e => e.id == id);
        }
    }
}
