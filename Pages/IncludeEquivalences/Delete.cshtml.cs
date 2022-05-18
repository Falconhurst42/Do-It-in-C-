#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DoItInCpp.Models;

namespace DoItInCpp.Pages.IncludeEquivalences
{
    public class DeleteModel : PageModel
    {
        private readonly DoItInCppContext _context;

        public DeleteModel(DoItInCppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public IncludeEquivalence IncludeEquivalence { get; set; }

        public async Task<IActionResult> OnGetAsync(int? cid, int? cppid)
        {
            if (cid == null || cppid == null)
            {
                return NotFound();
            }

            IncludeEquivalence = await _context.IncludeEquivalences.FirstOrDefaultAsync(m => m.C_ID == cid && m.CPP_ID == cppid);

            if (IncludeEquivalence == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? cid, int? cppid)
        {
            if (cid == null || cppid == null)
            {
                return NotFound();
            }

            IncludeEquivalence = await _context.IncludeEquivalences.FirstOrDefaultAsync(m => m.C_ID == cid && m.CPP_ID == cppid);

            if (IncludeEquivalence != null)
            {
                _context.IncludeEquivalences.Remove(IncludeEquivalence);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
