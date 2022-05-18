

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

namespace DoItInCpp.Pages;

public class SearchModel : PageModel
{
    private readonly DoItInCppContext _context;

    public SearchModel(DoItInCppContext context)
    {
        _context = context;
    }

    public ICollection<Snippet> Snippets;
    public string CurrentSearch;

    public async Task OnGetAsync(string? searchString)
    {
        CurrentSearch = searchString;
        if(CurrentSearch != null) {
            Snippets = await _context.Snippets
                    .Where(d => d.Name.Contains(CurrentSearch)
                        || d.Description.Contains(CurrentSearch)
                        || d.Documentation.Contains(CurrentSearch)
                        || d.Code.Contains(CurrentSearch))
                    .Include(m => m.Version)
                    .Include(m => m.Includes)
                        .ThenInclude(i => i.Include)
                    .Include(m => m.AddOns)
                        .ThenInclude(a => a.AddOn)
                    .Include(m => m.Categories)
                        .ThenInclude(i => i.Category)
                    .ToListAsync();
        }

        /*IQueryable<Snippet> SnippetsIQ = from s in _context.Snippets
                                            select s;
        if (!String.IsNullOrEmpty(searchString))
        {
            SnippetsIQ = SnippetsIQ.Where(d => d.Name.Contains(CurrentSearch)
                                    || d.Description.Contains(CurrentSearch)
                                    || d.Documentation.Contains(CurrentSearch)
                                    || d.Code.Contains(CurrentSearch));
        }

        Snippets = SnippetsIQ.AsNoTracking();*/
    }
}
