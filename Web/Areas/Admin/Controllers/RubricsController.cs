using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assessment.Data.Contexts;
using Assessment.Models;
using Microsoft.AspNetCore.Authorization;

namespace Assessment.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "System Administrator")]
    public class RubricsController : Controller
    {
        private readonly AssessmentContext _context;

        public RubricsController(AssessmentContext context)
        {
            _context = context;
        }

        // GET: Admin/Rubrics
        public async Task<IActionResult> Index()
        {

            var assessmentContext = _context.Rubrics.Include(r => r.School).Include(r => r.RubricCriteria);
            return View(await assessmentContext.ToListAsync());
        }

        // GET: Admin/Rubrics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rubric = await _context.Rubrics
                .Include(r => r.School)
                .Include(r => r.RubricCriteria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rubric == null)
            {
                return NotFound();
            }

            return View(rubric);
        }

        // GET: Admin/Rubrics/Create
        public IActionResult Create()
        {
            ViewData["SchoolId"] = new SelectList(_context.Schools, "Id", "Id");
            return View();
        }

        // POST: Admin/Rubrics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SchoolId,Code,Name,Criterion01,Criterion02,Criterion03,Criterion04,Criterion05,Criterion06,Criterion07,Criterion08,Criterion09,Criterion10,Data,File")] Rubric rubric)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rubric);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SchoolId"] = new SelectList(_context.Schools, "Id", "Id", rubric.SchoolId);
            return View(rubric);
        }

        // GET: Admin/Rubrics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rubric = await _context.Rubrics.FindAsync(id);
            if (rubric == null)
            {
                return NotFound();
            }
            ViewData["SchoolId"] = new SelectList(_context.Schools, "Id", "Id", rubric.SchoolId);
            return View(rubric);
        }

        // POST: Admin/Rubrics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SchoolId,Code,Name,Criterion01,Criterion02,Criterion03,Criterion04,Criterion05,Criterion06,Criterion07,Criterion08,Criterion09,Criterion10,Data,File")] Rubric rubric)
        {
            if (id != rubric.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rubric);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RubricExists(rubric.Id))
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
            ViewData["SchoolId"] = new SelectList(_context.Schools, "Id", "Id", rubric.SchoolId);
            return View(rubric);
        }

        // GET: Admin/Rubrics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rubric = await _context.Rubrics
                .Include(r => r.School)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rubric == null)
            {
                return NotFound();
            }

            return View(rubric);
        }

        // POST: Admin/Rubrics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rubric = await _context.Rubrics.FindAsync(id);
            _context.Rubrics.Remove(rubric);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RubricExists(int id)
        {
            return _context.Rubrics.Any(e => e.Id == id);
        }
    }
}
