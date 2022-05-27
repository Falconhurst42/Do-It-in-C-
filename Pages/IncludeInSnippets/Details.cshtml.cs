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

namespace DoItInCpp.Pages.IncludeInSnippets
{
    [AllowAnonymous]
    public class DetailsModel : PageModel
    {
        private readonly DoItInCppContext _context;

        public DetailsModel(DoItInCppContext context)
        {
            _context = context;
        }

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
            return Page();
        }
    }
}
