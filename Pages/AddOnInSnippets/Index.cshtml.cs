#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DoItInCpp.Models;

namespace DoItInCpp.Pages.AddOnInSnippets
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly DoItInCppContext _context;

        public IndexModel(DoItInCppContext context)
        {
            _context = context;
        }

        public IList<AddOnInSnippet> AddOnInSnippet { get;set; }

        public async Task OnGetAsync()
        {
            AddOnInSnippet = await _context.AddOnInSnippets
                .Include(a => a.AddOn)
                .Include(a => a.Snippet).ToListAsync();
        }
    }
}
