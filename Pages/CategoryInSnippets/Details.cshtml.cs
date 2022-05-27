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

namespace DoItInCpp.Pages.CategoryInSnippets
{
    [AllowAnonymous]
    public class DetailsModel : PageModel
    {
        private readonly DoItInCppContext _context;

        public DetailsModel(DoItInCppContext context)
        {
            _context = context;
        }

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
            return Page();
        }
    }
}
