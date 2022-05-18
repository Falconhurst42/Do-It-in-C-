#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DoItInCpp.Models;

namespace DoItInCpp.Pages.CategoryInSnippets
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
        ViewData["CategoryID"] = new SelectList(_context.Categories, "ID", "Name");
        ViewData["SnippetID"] = new SelectList(_context.Snippets, "ID", "Name");
            return Page();
        }

        [BindProperty]
        public CategoryInSnippet CategoryInSnippet { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var emptyCategoryInSnippet = new CategoryInSnippet();
            if (await TryUpdateModelAsync<CategoryInSnippet>(
                emptyCategoryInSnippet,
                "CategoryInSnippet",   // Prefix for form value.
                d => d.SnippetID, 
                d => d.CategoryID))
            {
                _context.CategoryInSnippets.Add(emptyCategoryInSnippet);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
