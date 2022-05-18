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

namespace DoItInCpp.Pages.AddOnInSnippets
{
    public class EditModel : PageModel
    {
        private readonly DoItInCppContext _context;

        public EditModel(DoItInCppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AddOnInSnippet AddOnInSnippet { get; set; }

        public async Task<IActionResult> OnGetAsync(int? snid, int? aoid)
        {
            if (snid == null || aoid == null)
            {
                return NotFound();
            }

            AddOnInSnippet = await _context.AddOnInSnippets
                .Include(a => a.AddOn)
                .Include(a => a.Snippet).FirstOrDefaultAsync(m => m.SnippetID == snid && m.AddOnID == aoid);

            if (AddOnInSnippet == null)
            {
                return NotFound();
            }
            ViewData["AddOnID"] = new SelectList(_context.AddOns, "ID", "Name");
            ViewData["SnippetID"] = new SelectList(_context.Snippets, "ID", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? snid, int? aoid)
        {
            var AddOnInSnippetToUpdate = await _context.AddOnInSnippets.FirstOrDefaultAsync(m => m.SnippetID == snid && m.AddOnID == aoid);

            if (AddOnInSnippetToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<AddOnInSnippet>(
                AddOnInSnippetToUpdate,
                "AddOnInSnippet",
                d => d.SnippetID, 
                d => d.AddOnID))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
