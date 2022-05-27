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

namespace DoItInCpp.Pages.Addons
{
    [AllowAnonymous]
    public class DetailsModel : PageModel
    {
        private readonly DoItInCppContext _context;

        public DetailsModel(DoItInCppContext context)
        {
            _context = context;
        }

        public AddOn AddOn { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AddOn = await _context.AddOns.FirstOrDefaultAsync(m => m.ID == id);

            if (AddOn == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
