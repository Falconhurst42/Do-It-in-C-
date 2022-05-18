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

namespace DoItInCpp.Pages.IncludeInSnippets
{
    public class EditModel : PageModel
    {
        private readonly DoItInCppContext _context;

        public EditModel(DoItInCppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public IncludeInSnippet IncludeInSnippet { get; set; }

        public async Task<IActionResult> OnGetAsync(int? snid, int? inid)
        {
            if (snid == null || inid == null)
            {
                return NotFound();
            }

            IncludeInSnippet = await _context.IncludeInSnippets
                .Include(i => i.Include)
                .Include(i => i.Snippet).FirstOrDefaultAsync(m => m.SnippetID == snid && m.IncludeID == inid);

            if (IncludeInSnippet == null)
            {
                return NotFound();
            }
           ViewData["IncludeID"] = new SelectList(_context.Includes, "ID", "Name");
           ViewData["SnippetID"] = new SelectList(_context.Snippets, "ID", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? snid, int? inid)
        {
            var IncludeInSnippetToUpdate = await _context.IncludeInSnippets.FirstOrDefaultAsync(m => m.SnippetID == snid && m.IncludeID == inid);

            if (IncludeInSnippetToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<IncludeInSnippet>(
                IncludeInSnippetToUpdate,
                "IncludeInSnippet",
                d => d.SnippetID, 
                d => d.IncludeID))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
