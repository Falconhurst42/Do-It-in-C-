#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DoItInCpp.Models;

namespace DoItInCpp.Pages.Includes
{
    public class DetailsModel : PageModel
    {
        private readonly DoItInCppContext _context;

        public DetailsModel(DoItInCppContext context)
        {
            _context = context;
        }

        public Include Include { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Include = await _context.Includes.FirstOrDefaultAsync(m => m.ID == id);

            if (Include == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
