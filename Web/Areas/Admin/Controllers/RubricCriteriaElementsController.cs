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
    public class RubricCriteriaElementsController : Controller
    {
        private readonly AssessmentContext _context;

        public RubricCriteriaElementsController(AssessmentContext context)
        {
            _context = context;
        }

        // GET: Admin/RubricCriteriaElements
        public async Task<IActionResult> Index()
        {
            return View(await _context.RubricCriteriaElements.ToListAsync());
        }

        // GET: Admin/RubricCriteriaElements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rubricCriteriaElement = await _context.RubricCriteriaElements
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rubricCriteriaElement == null)
            {
                return NotFound();
            }

            return View(rubricCriteriaElement);
        }

        // GET: Admin/RubricCriteriaElements/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/RubricCriteriaElements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RubricCriteriaId,CriteriaText,ScoreValue")] RubricCriteriaElement rubricCriteriaElement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rubricCriteriaElement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rubricCriteriaElement);
        }

        // GET: Admin/RubricCriteriaElements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rubricCriteriaElement = await _context.RubricCriteriaElements.FindAsync(id);
            if (rubricCriteriaElement == null)
            {
                return NotFound();
            }
            return View(rubricCriteriaElement);
        }

        // POST: Admin/RubricCriteriaElements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RubricCriteriaId,CriteriaText,ScoreValue")] RubricCriteriaElement rubricCriteriaElement)
        {
            if (id != rubricCriteriaElement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rubricCriteriaElement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RubricCriteriaElementExists(rubricCriteriaElement.Id))
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
            return View(rubricCriteriaElement);
        }

        // GET: Admin/RubricCriteriaElements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rubricCriteriaElement = await _context.RubricCriteriaElements
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rubricCriteriaElement == null)
            {
                return NotFound();
            }

            return View(rubricCriteriaElement);
        }

        // POST: Admin/RubricCriteriaElements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rubricCriteriaElement = await _context.RubricCriteriaElements.FindAsync(id);
            _context.RubricCriteriaElements.Remove(rubricCriteriaElement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RubricCriteriaElementExists(int id)
        {
            return _context.RubricCriteriaElements.Any(e => e.Id == id);
        }
    }
}
