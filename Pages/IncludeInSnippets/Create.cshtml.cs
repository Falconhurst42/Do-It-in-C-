#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DoItInCpp.Models;

namespace DoItInCpp.Pages.IncludeInSnippets
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
        ViewData["SnippetID"] = new SelectList(_context.Snippets, "ID", "Name");
            return Page();
        }

        [BindProperty]
        public IncludeInSnippet IncludeInSnippet { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var emptyIncludeInSnippet = new IncludeInSnippet();
            if (await TryUpdateModelAsync<IncludeInSnippet>(
                emptyIncludeInSnippet,
                "IncludeInSnippet",   // Prefix for form value.
                d => d.SnippetID, 
                d => d.IncludeID))
            {
                _context.IncludeInSnippets.Add(emptyIncludeInSnippet);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
