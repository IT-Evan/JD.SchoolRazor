using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JD.SchoolRazor.Models;

namespace JD.SchoolRazor.Pages.Students
{
    public class DetailsModel : PageModel
    {
        private readonly JD.SchoolRazor.Models.JDSchoolRazorContext _context;

        public DetailsModel(JD.SchoolRazor.Models.JDSchoolRazorContext context)
        {
            _context = context;
        }

        public Student Student { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Student = await _context.Student.FirstOrDefaultAsync(m => m.ID == id);

            Student = await _context.Student
                        .Include(s => s.Enrollments)
                            .ThenInclude(e => e.Course)
                        .AsNoTracking()
                        .FirstOrDefaultAsync(m => m.ID == id);

            if (Student == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
