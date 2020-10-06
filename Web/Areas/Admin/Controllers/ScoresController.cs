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
    public class ScoresController : Controller
    {
        private readonly AssessmentContext _context;

        public ScoresController(AssessmentContext context)
        {
            _context = context;
        }

        // GET: Admin/Scoress
        public async Task<IActionResult> Index()
        {
            var assessmentContext = _context.Scores.Include(s => s.Artifact).Include(s => s.Rubric).Include(s => s.School).Include(s => s.User);
            return View(await assessmentContext.ToListAsync());
        }

        // GET: Admin/Scoress/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var score = await _context.Scores
                .Include(s => s.Artifact)
                .Include(s => s.Rubric)
                .Include(s => s.School)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (score == null)
            {
                return NotFound();
            }

            return View(score);
        }

        // GET: Admin/Scoress/Create
        public IActionResult Create()
        {
            ViewData["ArtifactId"] = new SelectList(_context.Artifacts, "Id", "Id");
            ViewData["RubricId"] = new SelectList(_context.Rubrics, "Id", "Id");
            ViewData["SchoolId"] = new SelectList(_context.Schools, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Admin/Scoress/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,SchoolId,RubricId,ArtifactId,Score01,Score02,Score03,Score04,Score05,Score06,Score07,Score08,Score09,Score10")] Score score)
        {
            if (ModelState.IsValid)
            {
                _context.Add(score);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtifactId"] = new SelectList(_context.Artifacts, "Id", "Id", score.ArtifactId);
            ViewData["RubricId"] = new SelectList(_context.Rubrics, "Id", "Id", score.RubricId);
            ViewData["SchoolId"] = new SelectList(_context.Schools, "Id", "Id", score.SchoolId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", score.UserId);
            return View(score);
        }

        // GET: Admin/Scoress/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var score = await _context.Scores.FindAsync(id);
            if (score == null)
            {
                return NotFound();
            }
            ViewData["ArtifactId"] = new SelectList(_context.Artifacts, "Id", "Id", score.ArtifactId);
            ViewData["RubricId"] = new SelectList(_context.Rubrics, "Id", "Id", score.RubricId);
            ViewData["SchoolId"] = new SelectList(_context.Schools, "Id", "Id", score.SchoolId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", score.UserId);
            return View(score);
        }

        // POST: Admin/Scoress/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,SchoolId,RubricId,ArtifactId,Score01,Score02,Score03,Score04,Score05,Score06,Score07,Score08,Score09,Score10")] Score score)
        {
            if (id != score.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(score);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScoreExists(score.Id))
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
            ViewData["ArtifactId"] = new SelectList(_context.Artifacts, "Id", "Id", score.ArtifactId);
            ViewData["RubricId"] = new SelectList(_context.Rubrics, "Id", "Id", score.RubricId);
            ViewData["SchoolId"] = new SelectList(_context.Schools, "Id", "Id", score.SchoolId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", score.UserId);
            return View(score);
        }

        // GET: Admin/Scoress/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var score = await _context.Scores
                .Include(s => s.Artifact)
                .Include(s => s.Rubric)
                .Include(s => s.School)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (score == null)
            {
                return NotFound();
            }

            return View(score);
        }

        // POST: Admin/Scoress/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var score = await _context.Scores.FindAsync(id);
            _context.Scores.Remove(score);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScoreExists(int id)
        {
            return _context.Scores.Any(e => e.Id == id);
        }
    }
}
