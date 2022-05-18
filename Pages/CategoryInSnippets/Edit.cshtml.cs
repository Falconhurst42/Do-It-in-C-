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

namespace DoItInCpp.Pages.CategoryInSnippets
{
    public class EditModel : PageModel
    {
        private readonly DoItInCppContext _context;

        public EditModel(DoItInCppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CategoryInSnippet CategoryInSnippet { get; set; }

        public async Task<IActionResult> OnGetAsync(int? snid, int? cid)
        {
            if (snid == null || cid == null)
            {
                return NotFound();
            }

            CategoryInSnippet = await _context.CategoryInSnippets
                .Include(c => c.Category)
                .Include(c => c.Snippet).FirstOrDefaultAsync(m => m.SnippetID == snid && m.CategoryID == cid);

            if (CategoryInSnippet == null)
            {
                return NotFound();
            }
           ViewData["CategoryID"] = new SelectList(_context.Categories, "ID", "Name");
           ViewData["SnippetID"] = new SelectList(_context.Snippets, "ID", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? snid, int? cid)
        {
            var CategoryInSnippetToUpdate = await _context.CategoryInSnippets.FirstOrDefaultAsync(m => m.SnippetID == snid && m.CategoryID == cid);

            if (CategoryInSnippetToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<CategoryInSnippet>(
                CategoryInSnippetToUpdate,
                "CategoryInSnippet",
                d => d.SnippetID, 
                d => d.CategoryID))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
