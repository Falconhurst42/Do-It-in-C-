#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DoItInCpp.Models;

namespace DoItInCpp.Pages.Snippets
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
            var temp = new SelectList(_context.LanguageVersions, "Year_XX", "Year_XX");
            foreach(var item in temp) {
                item.Text = "C++" + item.Text;
            }
            ViewData["VersionID"] = temp;

            return Page();
        }

        [BindProperty]
        public Snippet Snippet { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var emptySnippet = new Snippet();

            /*emptySnippet.Name = emptySnippet.Description = emptySnippet.Documentation = emptySnippet.Code = "Test";
            emptySnippet.VersionID = 98;
            _context.Snippets.Add(emptySnippet);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");*/


            // THIS ISN"T WORKING AND IA DON"T KNOW WHY AHAHAHAHAHAHAHAHAHAHA
            if (await TryUpdateModelAsync<Snippet>(
                emptySnippet,
                "Snippet",   // Prefix for form value.
                d => d.Name, 
                d => d.Description, 
                d => d.Code, 
                d => d.Documentation, 
                d => d.VersionID))
            {
                _context.Snippets.Add(emptySnippet);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
