using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JD.SchoolRazor.Models;

namespace JD.SchoolRazor.Pages.Departments
{
    public class IndexModel : PageModel
    {
        private readonly JD.SchoolRazor.Models.JDSchoolRazorContext _context;

        public IndexModel(JD.SchoolRazor.Models.JDSchoolRazorContext context)
        {
            _context = context;
        }

        public IList<Department> Department { get;set; }

        public async Task OnGetAsync()
        {
            Department = await _context.Departments
                .Include(d => d.Administrator).ToListAsync();
        }
    }
}
