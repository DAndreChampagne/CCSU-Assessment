using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assessment.Data.Contexts;
using Assessment.Models;

namespace Assessment.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RubricCriteriaController : Controller
    {
        private readonly AssessmentContext _context;

        public RubricCriteriaController(AssessmentContext context)
        {
            _context = context;
        }

        // GET: Admin/RubricCriteria
        public async Task<IActionResult> Index()
        {
            var assessmentContext = _context.RubricCriteria.Include(r => r.Rubric);
            return View(await assessmentContext.ToListAsync());
        }

        // GET: Admin/RubricCriteria/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rubricCriteria = await _context.RubricCriteria
                .Include(r => r.Rubric)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rubricCriteria == null)
            {
                return NotFound();
            }

            return View(rubricCriteria);
        }

        // GET: Admin/RubricCriteria/Create
        public IActionResult Create()
        {
            ViewData["RubricId"] = new SelectList(_context.Rubrics, "Id", "Id");
            return View();
        }

        // POST: Admin/RubricCriteria/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RubricId,CriteriaText")] RubricCriteria rubricCriteria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rubricCriteria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RubricId"] = new SelectList(_context.Rubrics, "Id", "Id", rubricCriteria.RubricId);
            return View(rubricCriteria);
        }

        // GET: Admin/RubricCriteria/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rubricCriteria = await _context.RubricCriteria.FindAsync(id);
            if (rubricCriteria == null)
            {
                return NotFound();
            }
            ViewData["RubricId"] = new SelectList(_context.Rubrics, "Id", "Id", rubricCriteria.RubricId);
            return View(rubricCriteria);
        }

        // POST: Admin/RubricCriteria/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RubricId,CriteriaText")] RubricCriteria rubricCriteria)
        {
            if (id != rubricCriteria.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rubricCriteria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RubricCriteriaExists(rubricCriteria.Id))
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
            ViewData["RubricId"] = new SelectList(_context.Rubrics, "Id", "Id", rubricCriteria.RubricId);
            return View(rubricCriteria);
        }

        // GET: Admin/RubricCriteria/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rubricCriteria = await _context.RubricCriteria
                .Include(r => r.Rubric)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rubricCriteria == null)
            {
                return NotFound();
            }

            return View(rubricCriteria);
        }

        // POST: Admin/RubricCriteria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rubricCriteria = await _context.RubricCriteria.FindAsync(id);
            _context.RubricCriteria.Remove(rubricCriteria);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RubricCriteriaExists(int id)
        {
            return _context.RubricCriteria.Any(e => e.Id == id);
        }
    }
}
