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

namespace DoItInCpp.Pages.Snippets
{
    public class EditModel : PageModel
    {
        private readonly DoItInCppContext _context;

        public EditModel(DoItInCppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Snippet Snippet { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Snippet = await _context.Snippets.FirstOrDefaultAsync(m => m.ID == id);

            if (Snippet == null)
            {
                return NotFound();
            }
            var temp = new SelectList(_context.LanguageVersions, "Year_XX", "Year_XX");
            foreach(var item in temp) {
                item.Text = "C++" + item.Text;
            }
            ViewData["VersionID"] = temp;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var snippetToUpdate = await _context.Snippets.FindAsync(id);

            if (snippetToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Snippet>(
                snippetToUpdate,
                "Snippet",
                d => d.Name, 
                d => d.Description, 
                d => d.Code, 
                d => d.Documentation, 
                d => d.VersionID))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }

        private bool SnippetExists(int id)
        {
            return _context.Snippets.Any(e => e.ID == id);
        }
    }
}
