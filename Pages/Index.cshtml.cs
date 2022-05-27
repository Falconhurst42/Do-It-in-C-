

#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoItInCpp.Models;

namespace DoItInCpp.Pages;

[AllowAnonymous]
public class SplashModel : PageModel
{
    private readonly DoItInCppContext _context;

    public SplashModel(DoItInCppContext context)
    {
        _context = context;
    }

    public ICollection<Snippet> Snippets;

    public async Task OnGetAsync()
    {
        Snippets = await _context.Snippets
                .Include(m => m.Version)
                .Include(m => m.Includes)
                    .ThenInclude(i => i.Include)
                .Include(m => m.AddOns)
                    .ThenInclude(a => a.AddOn)
                .Include(m => m.Categories)
                    .ThenInclude(i => i.Category)
                .ToListAsync();
    }
}
