#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DoItInCpp.Models;

namespace DoItInCpp.Pages.AddOnInSnippets
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
        ViewData["AddOnID"] = new SelectList(_context.AddOns, "ID", "Name");
        ViewData["SnippetID"] = new SelectList(_context.Snippets, "ID", "Name");
            return Page();
        }

        [BindProperty]
        public AddOnInSnippet AddOnInSnippet { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var emptyAddOnInSnippet = new AddOnInSnippet();
            if (await TryUpdateModelAsync<AddOnInSnippet>(
                emptyAddOnInSnippet,
                "AddOnInSnippet",   // Prefix for form value.
                d => d.SnippetID, 
                d => d.AddOnID))
            {
                _context.AddOnInSnippets.Add(emptyAddOnInSnippet);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
