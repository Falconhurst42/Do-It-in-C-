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
    public class IndexModel : PageModel
    {
        private readonly DoItInCppContext _context;

        public IndexModel(DoItInCppContext context)
        {
            _context = context;
        }

        public IList<CategoryInSnippet> CategoryInSnippet { get;set; }

        public async Task OnGetAsync()
        {
            CategoryInSnippet = await _context.CategoryInSnippets
                .Include(c => c.Category)
                .Include(c => c.Snippet).ToListAsync();
        }
    }
}
