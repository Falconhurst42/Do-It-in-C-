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

namespace DoItInCpp.Pages.IncludeEquivalences
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly DoItInCppContext _context;

        public IndexModel(DoItInCppContext context)
        {
            _context = context;
        }

        public IList<IncludeEquivalence> IncludeEquivalence { get;set; }

        public async Task OnGetAsync()
        {
            IncludeEquivalence = await _context.IncludeEquivalences.ToListAsync();
        }
    }
}
