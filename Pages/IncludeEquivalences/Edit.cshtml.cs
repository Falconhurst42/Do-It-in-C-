#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoItInCpp.Models;

namespace DoItInCpp.Pages.IncludeEquivalences
{
    public class EditModel : PageModel
    {
        private readonly DoItInCppContext _context;

        public EditModel(DoItInCppContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? cid, int? cppid)
        {
            var IncludeEquivalenceToUpdate = await _context.IncludeEquivalences.FirstOrDefaultAsync(m => m.C_ID == cid && m.CPP_ID == cppid);

            if (IncludeEquivalenceToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<IncludeEquivalence>(
                IncludeEquivalenceToUpdate,
                "IncludeEquivalence",
                d => d.C_ID, 
                d => d.CPP_ID))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
