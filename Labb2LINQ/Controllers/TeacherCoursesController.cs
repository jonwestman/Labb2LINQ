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
    public class TeacherCoursesController : Controller
    {
        private readonly SchoolDbContext _context;

        public TeacherCoursesController(SchoolDbContext context)
        {
            _context = context;
        }

        // GET: TeacherCourses
        public async Task<IActionResult> Index()
        {
            var schoolDbContext = _context.TeacherCourse.Include(t => t.Courses).Include(t => t.Teachers);
            return View(await schoolDbContext.ToListAsync());
        }

        // GET: TeacherCourses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TeacherCourse == null)
            {
                return NotFound();
            }

            var teacherCourse = await _context.TeacherCourse
                .Include(t => t.Courses)
                .Include(t => t.Teachers)
                .FirstOrDefaultAsync(m => m.TeacherCourseId == id);
            if (teacherCourse == null)
            {
                return NotFound();
            }

            return View(teacherCourse);
        }

        // GET: TeacherCourses/Create
        public IActionResult Create()
        {
            ViewData["FK_CourseId"] = new SelectList(_context.Courses, "CourseId", "Title");
            ViewData["FK_TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "FirstMidName");
            return View();
        }

        // POST: TeacherCourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeacherCourseId,FK_TeacherId,FK_CourseId")] TeacherCourse teacherCourse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teacherCourse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FK_CourseId"] = new SelectList(_context.Courses, "CourseId", "Title", teacherCourse.FK_CourseId);
            ViewData["FK_TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "FirstMidName", teacherCourse.FK_TeacherId);
            return View(teacherCourse);
        }

        // GET: TeacherCourses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TeacherCourse == null)
            {
                return NotFound();
            }

            var teacherCourse = await _context.TeacherCourse.FindAsync(id);
            if (teacherCourse == null)
            {
                return NotFound();
            }
            ViewData["FK_CourseId"] = new SelectList(_context.Courses, "CourseId", "Title", teacherCourse.FK_CourseId);
            ViewData["FK_TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "FirstMidName", teacherCourse.FK_TeacherId);
            return View(teacherCourse);
        }

        // POST: TeacherCourses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeacherCourseId,FK_TeacherId,FK_CourseId")] TeacherCourse teacherCourse)
        {
            if (id != teacherCourse.TeacherCourseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacherCourse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherCourseExists(teacherCourse.TeacherCourseId))
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
            ViewData["FK_CourseId"] = new SelectList(_context.Courses, "CourseId", "Title", teacherCourse.FK_CourseId);
            ViewData["FK_TeacherId"] = new SelectList(_context.Teachers, "TeacherId", "FirstMidName", teacherCourse.FK_TeacherId);
            return View(teacherCourse);
        }

        // GET: TeacherCourses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TeacherCourse == null)
            {
                return NotFound();
            }

            var teacherCourse = await _context.TeacherCourse
                .Include(t => t.Courses)
                .Include(t => t.Teachers)
                .FirstOrDefaultAsync(m => m.TeacherCourseId == id);
            if (teacherCourse == null)
            {
                return NotFound();
            }

            return View(teacherCourse);
        }

        // POST: TeacherCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TeacherCourse == null)
            {
                return Problem("Entity set 'SchoolDbContext.TeacherCourse'  is null.");
            }
            var teacherCourse = await _context.TeacherCourse.FindAsync(id);
            if (teacherCourse != null)
            {
                _context.TeacherCourse.Remove(teacherCourse);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherCourseExists(int id)
        {
          return (_context.TeacherCourse?.Any(e => e.TeacherCourseId == id)).GetValueOrDefault();
        }
    }
}
