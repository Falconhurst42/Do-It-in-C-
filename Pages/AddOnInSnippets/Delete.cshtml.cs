#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DoItInCpp.Models;

namespace DoItInCpp.Pages.AddOnInSnippets
{
    public class DeleteModel : PageModel
    {
        private readonly DoItInCppContext _context;

        public DeleteModel(DoItInCppContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? snid, int? aoid)
        {
            if (snid == null || aoid == null)
            {
                return NotFound();
            }

            AddOnInSnippet = await _context.AddOnInSnippets.FirstOrDefaultAsync(m => m.SnippetID == snid && m.AddOnID == aoid);

            if (AddOnInSnippet != null)
            {
                _context.AddOnInSnippets.Remove(AddOnInSnippet);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
