﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Labb2LINQ.Data;
using Labb2LINQ.Models;
using System.Collections.Generic;

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
        // Get all Students and their teachers
        public async Task<IActionResult> GetAllStudentsTeachers()
        {
              List<SchoolViewModel> list = new List<SchoolViewModel>();

            var items = await (from cl in _context.Classes
                               join s in _context.Students on cl.ClassId equals s.FK_ClassId
                               join sc in _context.StudentCourse on s.StudentId equals sc.FK_StudentId
                               join c in _context.Courses on sc.FK_CourseId equals c.CourseId
                               join tc in _context.TeacherCourse on c.CourseId equals tc.FK_CourseId
                               join t in _context.Teachers on tc.FK_TeacherId equals t.TeacherId
                               where t.TeacherId == tc.FK_TeacherId && cl.ClassId == s.FK_ClassId
                               select new
                               {
                                   StudentFName = s.FirstMidName,
                                   StudentLName = s.LastName,
                                   ClassName = cl.ClassName,
                                   CourseName = c.Title,
                                   TeacherFName = t.FirstMidName,
                                   TeacherLName = t.LastName,
                               }).ToListAsync();
            foreach (var item in items)
            {
                SchoolViewModel listitem = new SchoolViewModel();
                listitem.StudentFName = item.StudentFName;
                listitem.StudentLName = item.StudentLName;
                listitem.ClassName = item.ClassName;
                listitem.CourseName = item.CourseName;
                listitem.TeacherFName = item.TeacherFName;
                listitem.TeacherLName = item.TeacherLName;
                list.Add(listitem);
            }
            return View(list);
        }

        //Get all teachers currently teaching the course "programmering1"
        public async Task<IActionResult> GetTeachers()
        {
            List<SchoolViewModel> list = new List<SchoolViewModel>();

            var items = await (from t in _context.Teachers
                               join tc in _context.TeacherCourse on t.TeacherId equals tc.FK_TeacherId
                               join co in _context.Courses on tc.FK_CourseId equals co.CourseId
                               where co.Title == "programmering1"
                               select new
                               {
                                   CourseName = co.Title,
                                   TeacherFName = t.FirstMidName,
                                   TeacherLName = t.LastName,
                               }).ToListAsync();
            foreach (var item in items)
            {
                SchoolViewModel listitem = new SchoolViewModel();
                listitem.CourseName = item.CourseName;
                listitem.TeacherFName = item.TeacherFName;
                listitem.TeacherLName = item.TeacherLName;
                list.Add(listitem);
            }

            return View(list);
        }
        //Get all students currently learning the course "programmering1"
        public async Task<IActionResult> GetStudents()
        {
            List<SchoolViewModel> list = new List<SchoolViewModel>();

            var items = await (from s in _context.Students
                               join sc in _context.StudentCourse on s.StudentId equals sc.FK_StudentId
                               join co in _context.Courses on sc.FK_CourseId equals co.CourseId
                               join tc in _context.TeacherCourse on co.CourseId equals tc.FK_CourseId
                               join t in _context.Teachers on tc.FK_TeacherId equals t.TeacherId
                               where co.Title == "programmering1"
                               select new
                               {
                                   CourseName = co.Title,
                                   StudentFName = s.FirstMidName,
                                   StudentLName = s.LastName,
                                   TeacherFName = t.FirstMidName,
                                   TeacherLName = t.LastName,
                               }).ToListAsync();
            foreach (var item in items)
            {
                SchoolViewModel listitem = new SchoolViewModel();
                listitem.CourseName = item.CourseName;
                listitem.StudentFName = item.StudentFName;
                listitem.StudentLName = item.StudentLName;
                listitem.TeacherFName = item.TeacherFName;
                listitem.TeacherLName = item.TeacherLName;
                list.Add(listitem);
            }

            return View(list);
        }

        //POST set CourseName to something else
        public IActionResult CourseName() { return View(); }    
        public async Task<IActionResult>SetCourseName(string CurrentCourseName, string NewCourseName)
        {
            var course = (from c in _context.Courses
                          where c.Title == CurrentCourseName
                          select c).FirstOrDefault();
            if (course==null)
            {
                return NotFound("Course name not found. Please try again");
            }

            course.Title = NewCourseName;
            await _context.SaveChangesAsync();

            return View();

        }

        //POST set new teacher
        public IActionResult UpdateTeacher()
        {
            return View();
        }

        public async Task<IActionResult> SetTeacher(string CourseName, string TeacherLName)
        {
            var currentTeacher = (from c in _context.Courses
                                   join tc in _context.TeacherCourse on c.CourseId equals tc.FK_CourseId
                                   join t in _context.Teachers on tc.FK_TeacherId equals t.TeacherId
                                   where t.TeacherId == tc.FK_TeacherId && c.CourseId == tc.FK_CourseId && c.Title == CourseName
                                   select t).FirstOrDefault();

            var newTeacher = (from t in _context.Teachers
                              where t.LastName == TeacherLName
                              select t).FirstOrDefault();

            var updateTeacher = (from tc in _context.TeacherCourse
                                 where tc.FK_TeacherId == currentTeacher.TeacherId
                                 select tc).FirstOrDefault();

            updateTeacher.FK_TeacherId = newTeacher.TeacherId;
            await _context.SaveChangesAsync();

            return View();
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
