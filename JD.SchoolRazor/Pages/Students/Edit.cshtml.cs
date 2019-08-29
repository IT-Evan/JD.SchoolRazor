using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JD.SchoolRazor.Models;

namespace JD.SchoolRazor.Pages.Students
{
    public class EditModel : PageModel
    {
        private readonly JDSchoolRazorContext _context;

        public EditModel(JDSchoolRazorContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Student Student { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Student = await _context.Student.FirstOrDefaultAsync(m => m.ID == id);
            Student = await _context.Student.FindAsync(id);

            if (Student == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //_context.Attach(Student).State = EntityState.Modified;
            var studentToUpdate = await _context.Student.FindAsync(id);

            if (await TryUpdateModelAsync<Student>(
                studentToUpdate,
                "student",   // Prefix for form value.
                s => s.FirstMidName, s => s.LastName, s => s.EnrollmentDate))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }

        private bool StudentExists(int id)
        {
            return _context.Student.Any(e => e.ID == id);
        }
    }
}
