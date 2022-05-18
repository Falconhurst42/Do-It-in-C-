#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DoItInCpp.Models;

namespace DoItInCpp.Pages.Includes
{
    public class IndexModel : PageModel
    {
        private readonly DoItInCppContext _context;

        public IndexModel(DoItInCppContext context)
        {
            _context = context;
        }

        public IList<Include> Include { get;set; }

        public async Task OnGetAsync()
        {
            Include = await _context.Includes.ToListAsync();
        }
    }
}
