#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoItInCpp.Models;

namespace DoItInCpp.Pages.Addons
{
    public class EditModel : PageModel
    {
        private readonly DoItInCppContext _context;

        public EditModel(DoItInCppContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var AddOnToUpdate = await _context.AddOns.FindAsync(id);

            if (AddOnToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<AddOn>(
                AddOnToUpdate,
                "AddOn",
                d => d.Name, 
                d => d.Tagline,
                d => d.Description))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
