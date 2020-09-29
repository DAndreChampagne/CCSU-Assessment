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
    public class ArtifactsController : Controller
    {
        private readonly AssessmentContext _context;

        public ArtifactsController(AssessmentContext context)
        {
            _context = context;
        }

        // GET: Admin/Artifacts
        public async Task<IActionResult> Index()
        {
            var assessmentContext = _context.Artifacts.Include(a => a.Rubric).Include(a => a.School).Include(a => a.User);
            return View(await assessmentContext.ToListAsync());
        }

        public async Task<IActionResult> ViewFile(int? id) {
            if (id == null)
                return NotFound();

            var file = await _context.Artifacts
                .Where(x => x.Id == id)
                .Select(x => x.File)
                .FirstOrDefaultAsync();

            return File(file, System.Net.Mime.MediaTypeNames.Application.Pdf);
        }

        // GET: Admin/Artifacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artifact = await _context.Artifacts
                .Include(a => a.Rubric)
                .Include(a => a.School)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artifact == null)
            {
                return NotFound();
            }

            return View(artifact);
        }

        // GET: Admin/Artifacts/Create
        public IActionResult Create()
        {
            ViewData["RubricId"] = new SelectList(_context.Rubrics, "Id", "Id");
            ViewData["SchoolId"] = new SelectList(_context.Schools, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Admin/Artifacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SchoolId,RubricId,Name,Term,StudentId,UserId,LearningObjective,Level,CRN,FilePath,File")] Artifact artifact)
        {
            if (ModelState.IsValid)
            {
                _context.Add(artifact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RubricId"] = new SelectList(_context.Rubrics, "Id", "Id", artifact.RubricId);
            ViewData["SchoolId"] = new SelectList(_context.Schools, "Id", "Id", artifact.SchoolId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", artifact.UserId);
            return View(artifact);
        }

        // GET: Admin/Artifacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artifact = await _context.Artifacts.FindAsync(id);
            if (artifact == null)
            {
                return NotFound();
            }
            ViewData["RubricId"] = new SelectList(_context.Rubrics, "Id", "Id", artifact.RubricId);
            ViewData["SchoolId"] = new SelectList(_context.Schools, "Id", "Id", artifact.SchoolId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", artifact.UserId);
            return View(artifact);
        }

        // POST: Admin/Artifacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SchoolId,RubricId,Name,Term,StudentId,UserId,LearningObjective,Level,CRN,FilePath,File")] Artifact artifact)
        {
            if (id != artifact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artifact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtifactExists(artifact.Id))
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
            ViewData["RubricId"] = new SelectList(_context.Rubrics, "Id", "Id", artifact.RubricId);
            ViewData["SchoolId"] = new SelectList(_context.Schools, "Id", "Id", artifact.SchoolId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", artifact.UserId);
            return View(artifact);
        }

        // GET: Admin/Artifacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artifact = await _context.Artifacts
                .Include(a => a.Rubric)
                .Include(a => a.School)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artifact == null)
            {
                return NotFound();
            }

            return View(artifact);
        }

        // POST: Admin/Artifacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artifact = await _context.Artifacts.FindAsync(id);
            _context.Artifacts.Remove(artifact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtifactExists(int id)
        {
            return _context.Artifacts.Any(e => e.Id == id);
        }
    }
}
