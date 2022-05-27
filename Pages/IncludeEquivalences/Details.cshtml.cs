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
    public class DetailsModel : PageModel
    {
        private readonly DoItInCppContext _context;

        public DetailsModel(DoItInCppContext context)
        {
            _context = context;
        }

        public IncludeEquivalence IncludeEquivalence { get; set; }
        public Include CInc { get; set; }
        public Include CPPInc { get; set; }

        public async Task<IActionResult> OnGetAsync(int? cid, int? cppid)
        {
            if (cid == null || cppid == null)
            {
                return NotFound();
            }

            IncludeEquivalence = await _context.IncludeEquivalences.FirstOrDefaultAsync(m => m.C_ID == cid && m.CPP_ID == cppid);
            CInc = await _context.Includes.FirstOrDefaultAsync(m => m.ID == cid);
            CPPInc = await _context.Includes.FirstOrDefaultAsync(m => m.ID == cppid);

            if (IncludeEquivalence == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
