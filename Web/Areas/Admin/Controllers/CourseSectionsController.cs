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
    public class CourseSectionsController : Controller
    {
        private readonly AssessmentContext _context;

        public CourseSectionsController(AssessmentContext context)
        {
            _context = context;
        }

        // GET: Admin/CourseSections
        public async Task<IActionResult> Index()
        {
            var assessmentContext = _context.CourseSections.Include(c => c.Faculty);
            return View(await assessmentContext.ToListAsync());
        }

        // GET: Admin/CourseSections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseSection = await _context.CourseSections
                .Include(c => c.Faculty)
                .FirstOrDefaultAsync(m => m.CRN == id);
            if (courseSection == null)
            {
                return NotFound();
            }

            return View(courseSection);
        }

        // GET: Admin/CourseSections/Create
        public IActionResult Create()
        {
            ViewData["FacultyId"] = new SelectList(_context.Faculty, "Id", "Id");
            return View();
        }

        // POST: Admin/CourseSections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CRN,FacultyId,Name")] CourseSection courseSection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courseSection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FacultyId"] = new SelectList(_context.Faculty, "Id", "Id", courseSection.FacultyId);
            return View(courseSection);
        }

        // GET: Admin/CourseSections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseSection = await _context.CourseSections.FindAsync(id);
            if (courseSection == null)
            {
                return NotFound();
            }
            ViewData["FacultyId"] = new SelectList(_context.Faculty, "Id", "Id", courseSection.FacultyId);
            return View(courseSection);
        }

        // POST: Admin/CourseSections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CRN,FacultyId,Name")] CourseSection courseSection)
        {
            if (id != courseSection.CRN)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseSection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseSectionExists(courseSection.CRN))
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
            ViewData["FacultyId"] = new SelectList(_context.Faculty, "Id", "Id", courseSection.FacultyId);
            return View(courseSection);
        }

        // GET: Admin/CourseSections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseSection = await _context.CourseSections
                .Include(c => c.Faculty)
                .FirstOrDefaultAsync(m => m.CRN == id);
            if (courseSection == null)
            {
                return NotFound();
            }

            return View(courseSection);
        }

        // POST: Admin/CourseSections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courseSection = await _context.CourseSections.FindAsync(id);
            _context.CourseSections.Remove(courseSection);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseSectionExists(int id)
        {
            return _context.CourseSections.Any(e => e.CRN == id);
        }
    }
}
