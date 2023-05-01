using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Labb2LINQ.Data;
using Labb2LINQ.Models;

namespace Labb2LINQ.Controllers
{
    public class SchoolViewModelsController : Controller
    {
        private readonly SchoolDbContext _context;

        public SchoolViewModelsController(SchoolDbContext context)
        {
            _context = context;
        }

        // GET: SchoolViewModels
        public async Task<IActionResult> Index()
        {
              return _context.SchoolViewModel != null ? 
                          View(await _context.SchoolViewModel.ToListAsync()) :
                          Problem("Entity set 'SchoolDbContext.SchoolViewModel'  is null.");
        }

        // GET: SchoolViewModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SchoolViewModel == null)
            {
                return NotFound();
            }

            var schoolViewModel = await _context.SchoolViewModel
                .FirstOrDefaultAsync(m => m.SchoolViewModelId == id);
            if (schoolViewModel == null)
            {
                return NotFound();
            }

            return View(schoolViewModel);
        }

        // GET: SchoolViewModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SchoolViewModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SchoolViewModelId,StudentFName,StudentLName,ClassName,CourseName,TeacherId,TeacherFName,TeacherLName")] SchoolViewModel schoolViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(schoolViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(schoolViewModel);
        }

        // GET: SchoolViewModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SchoolViewModel == null)
            {
                return NotFound();
            }

            var schoolViewModel = await _context.SchoolViewModel.FindAsync(id);
            if (schoolViewModel == null)
            {
                return NotFound();
            }
            return View(schoolViewModel);
        }

        // POST: SchoolViewModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SchoolViewModelId,StudentFName,StudentLName,ClassName,CourseName,TeacherId,TeacherFName,TeacherLName")] SchoolViewModel schoolViewModel)
        {
            if (id != schoolViewModel.SchoolViewModelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schoolViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SchoolViewModelExists(schoolViewModel.SchoolViewModelId))
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
            return View(schoolViewModel);
        }

        // GET: SchoolViewModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SchoolViewModel == null)
            {
                return NotFound();
            }

            var schoolViewModel = await _context.SchoolViewModel
                .FirstOrDefaultAsync(m => m.SchoolViewModelId == id);
            if (schoolViewModel == null)
            {
                return NotFound();
            }

            return View(schoolViewModel);
        }

        // POST: SchoolViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SchoolViewModel == null)
            {
                return Problem("Entity set 'SchoolDbContext.SchoolViewModel'  is null.");
            }
            var schoolViewModel = await _context.SchoolViewModel.FindAsync(id);
            if (schoolViewModel != null)
            {
                _context.SchoolViewModel.Remove(schoolViewModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SchoolViewModelExists(int id)
        {
          return (_context.SchoolViewModel?.Any(e => e.SchoolViewModelId == id)).GetValueOrDefault();
        }
    }
}
