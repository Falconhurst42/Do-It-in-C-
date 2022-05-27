#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DoItInCpp.Models;
using DoItInCpp.Utilities;

namespace DoItInCpp.Pages.Snippets
{
    [AllowAnonymous]
    public class DetailsModel : PageModel
    {
        private readonly DoItInCppContext _context;

        public DetailsModel(DoItInCppContext context)
        {
            _context = context;
        }

        public Snippet Snippet { get; set; }
        public ICollection<Snippet> MoreLike { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Snippet = await _context.Snippets
                .Include(m => m.Version)
                .Include(m => m.Includes)
                    .ThenInclude(i => i.Include)
                .Include(m => m.AddOns)
                    .ThenInclude(a => a.AddOn)
                .Include(m => m.Categories)
                    .ThenInclude(i => i.Category)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Snippet == null)
            {
                return NotFound();
            }

            /*var query = 
                from cis in _context.CategoryInSnippets
                where Snippet.Categories.Contains(cis)
                join cis2 in _context.CategoryInSnippets on cis.CategoryID equals cis2.CategoryID
                join snip in _context.Snippets on cis2.SnippetID equals snip.ID
                select snip;
            
            foreach(var snip in query) {
                Console.WriteLine(snip.Name);
            }*/

            // this can prob all be better LINQ queries, but I don't care
            var snipids = _context.CategoryInSnippets
                .Where(cis => cis.SnippetID != Snippet.ID && Snippet.Categories.Select(cis => cis.CategoryID).Contains(cis.CategoryID))
                .Select(cis => cis.SnippetID);
            
            MoreLike = _context.Snippets
                .Where(sn => snipids.Contains(sn.ID))
                .Include(m => m.Version)
                .Include(m => m.Includes)
                    .ThenInclude(i => i.Include)
                .Include(m => m.AddOns)
                    .ThenInclude(a => a.AddOn)
                .Include(m => m.Categories)
                    .ThenInclude(i => i.Category)
                .ToList();

            return Page();
        }
    }
}
