using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assessment.Data.Contexts;
using Assessment.Models;
using Assessment.Web.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Assessment.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "System Administrator")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly AssessmentContext _db;
        private readonly UserManager<Assessment.Models.User> _userManager;
        private readonly RoleManager<Assessment.Models.Role> _roleManager;

        public UsersController(ApplicationDbContext context, AssessmentContext db, UserManager<User> userManager, RoleManager<Assessment.Models.Role> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }

        // GET: Admin/Users
        public async Task<IActionResult> Index()
        {
            var users = await _context.Users
                .OrderBy(x => x.Name)
                .ToListAsync();
            var allRoles = await _context.Roles.ToListAsync();

            foreach (var user in users) {
                var roles = await _userManager.GetRolesAsync(user);
                user.RoleName = String.Join(',', roles);
                user.RoleDescription = String.Join(',',
                    allRoles.Where(x => roles.Contains(x.Name))
                        .Select(x => x.Description)
                        .ToList()
                );
            }

            return View(users);
        }

        // GET: Admin/Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Admin/Users/Create
        public async Task<IActionResult> Create()
        {
            ViewData["Roles"] = new SelectList(_context.Roles, "Id", "Name");

            return View();
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                user.UserName = user.Email;
                user.NormalizedUserName = user.UserName.ToUpperInvariant();
                user.NormalizedEmail = user.Email.ToUpperInvariant();
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, "Abcdefg!1");

                var userResult = await _userManager.CreateAsync(user);
                
                foreach (var role in user.Roles) {
                    var r = await _roleManager.FindByIdAsync(role);
                    var roleResult = await _userManager.AddToRoleAsync(user, r.Name);
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["Roles"] = new SelectList(_context.Roles, "Id", "Name");

            return View(user);
        }

        // GET: Admin/Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            foreach (var item in await _roleManager.Roles.ToListAsync()) {
                if (await _userManager.IsInRoleAsync(user, item.Name)) {
                    user.Roles.Add(item.Id);
                }
            }

            ViewData["Roles"] = new SelectList(_context.Roles, "Id", "Name");
            return View(user);
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var u = await _userManager.FindByIdAsync(user.Id);
                    u.Name = user.Name;
                    u.Email = user.Email;
                    u.NormalizedEmail = user.Email.ToUpperInvariant();
                    u.EmailConfirmed = false;

                    await _userManager.UpdateAsync(u);

                    var oRoles = await _context.UserRoles
                        .Where(x => x.UserId == user.Id)
                        .Select(x => x.RoleId)
                        .ToListAsync();
                    var nRoles = user.Roles;
                    var originalList = await _context.Roles
                        .Where(x => oRoles.Contains(x.Id))
                        .Select(x => x.Name)
                        .ToListAsync();
                    var newList = await _context.Roles
                        .Where(x => nRoles.Contains(x.Id))
                        .Select(x => x.Name)
                        .ToListAsync();


                    var itemsToRemove = originalList.Except(newList);
                    var itemsToAdd = newList.Except(originalList);

                    await _userManager.RemoveFromRolesAsync(u, itemsToRemove);
                    await _userManager.AddToRolesAsync(u, itemsToAdd);

                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!UserExists(user.Id))
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

            foreach (var item in await _roleManager.Roles.ToListAsync()) {
                if (await _userManager.IsInRoleAsync(user, item.Name)) {
                    user.Roles.Add(item.Id);
                }
            }

            ViewData["Roles"] = new SelectList(_context.Roles, "Id", "Name");
            return View(user);
        }

        // GET: Admin/Users/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var u = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(u);

            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
