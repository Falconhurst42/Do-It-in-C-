#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DoItInCpp.Models;

namespace DoItInCpp.Pages.IncludeEquivalences
{
    public class CreateModel : PageModel
    {
        private readonly DoItInCppContext _context;

        public CreateModel(DoItInCppContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["IncludeID"] = new SelectList(_context.Includes, "ID", "Name");
            return Page();
        }

        [BindProperty]
        public IncludeEquivalence IncludeEquivalence { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var emptyIncludeEquivalence = new IncludeEquivalence();
            if (await TryUpdateModelAsync<IncludeEquivalence>(
                emptyIncludeEquivalence,
                "IncludeEquivalence",   // Prefix for form value.
                d => d.C_ID, 
                d => d.CPP_ID))
            {
                // avoid self-referencing or mirror equivalences
                if(emptyIncludeEquivalence.C_ID == emptyIncludeEquivalence.CPP_ID
                    || _context.IncludeEquivalences.FirstOrDefault(
                        m => m.C_ID == emptyIncludeEquivalence.CPP_ID
                            && m.CPP_ID == emptyIncludeEquivalence.C_ID) != null) {
                    return Page();
                }
                _context.IncludeEquivalences.Add(emptyIncludeEquivalence);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
