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
    public class StudentCoursesController : Controller
    {
        private readonly SchoolDbContext _context;

        public StudentCoursesController(SchoolDbContext context)
        {
            _context = context;
        }

        // GET: StudentCourses
        public async Task<IActionResult> Index()
        {
            var schoolDbContext = _context.StudentCourse.Include(s => s.Courses).Include(s => s.Students);
            return View(await schoolDbContext.ToListAsync());
        }

        // GET: StudentCourses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StudentCourse == null)
            {
                return NotFound();
            }

            var studentCourse = await _context.StudentCourse
                .Include(s => s.Courses)
                .Include(s => s.Students)
                .FirstOrDefaultAsync(m => m.StudentCourseId == id);
            if (studentCourse == null)
            {
                return NotFound();
            }

            return View(studentCourse);
        }

        // GET: StudentCourses/Create
        public IActionResult Create()
        {
            ViewData["FK_CourseId"] = new SelectList(_context.Courses, "CourseId", "Title");
            ViewData["FK_StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId");
            return View();
        }

        // POST: StudentCourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentCourseId,FK_StudentId,FK_CourseId")] StudentCourse studentCourse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentCourse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FK_CourseId"] = new SelectList(_context.Courses, "CourseId", "Title", studentCourse.FK_CourseId);
            ViewData["FK_StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", studentCourse.FK_StudentId);
            return View(studentCourse);
        }

        // GET: StudentCourses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StudentCourse == null)
            {
                return NotFound();
            }

            var studentCourse = await _context.StudentCourse.FindAsync(id);
            if (studentCourse == null)
            {
                return NotFound();
            }
            ViewData["FK_CourseId"] = new SelectList(_context.Courses, "CourseId", "Title", studentCourse.FK_CourseId);
            ViewData["FK_StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", studentCourse.FK_StudentId);
            return View(studentCourse);
        }

        // POST: StudentCourses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentCourseId,FK_StudentId,FK_CourseId")] StudentCourse studentCourse)
        {
            if (id != studentCourse.StudentCourseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentCourse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentCourseExists(studentCourse.StudentCourseId))
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
            ViewData["FK_CourseId"] = new SelectList(_context.Courses, "CourseId", "Title", studentCourse.FK_CourseId);
            ViewData["FK_StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", studentCourse.FK_StudentId);
            return View(studentCourse);
        }

        // GET: StudentCourses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StudentCourse == null)
            {
                return NotFound();
            }

            var studentCourse = await _context.StudentCourse
                .Include(s => s.Courses)
                .Include(s => s.Students)
                .FirstOrDefaultAsync(m => m.StudentCourseId == id);
            if (studentCourse == null)
            {
                return NotFound();
            }

            return View(studentCourse);
        }

        // POST: StudentCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StudentCourse == null)
            {
                return Problem("Entity set 'SchoolDbContext.StudentCourse'  is null.");
            }
            var studentCourse = await _context.StudentCourse.FindAsync(id);
            if (studentCourse != null)
            {
                _context.StudentCourse.Remove(studentCourse);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentCourseExists(int id)
        {
          return (_context.StudentCourse?.Any(e => e.StudentCourseId == id)).GetValueOrDefault();
        }
    }
}
