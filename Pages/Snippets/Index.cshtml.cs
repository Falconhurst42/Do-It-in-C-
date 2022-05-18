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

namespace DoItInCpp.Pages.Snippets
{
    public class IndexModel : PageModel
    {
        private readonly DoItInCppContext _context;

        public IndexModel(DoItInCppContext context)
        {
            _context = context;
        }

        public string NameSort { get; set; }
        public string CreatedSort { get; set; }
        public string UpdatedSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<Snippet> Snippets { get;set; }

        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;

            UpdatedSort = String.IsNullOrEmpty(sortOrder) ? "updated" : "";
            NameSort = (sortOrder == "name") ? "name_desc" : "name";
            CreatedSort = (sortOrder == "created") ? "created_desc" : "created";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            IQueryable<Snippet> SnippetsIQ = from s in _context.Snippets
                                             select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                SnippetsIQ = SnippetsIQ.Where(d => d.Name.Contains(searchString)
                                       || d.Description.Contains(searchString));
            }

            SnippetsIQ = sortOrder switch
            {
                "name" => SnippetsIQ.OrderBy(d => d.Name),
                "name_desc" => SnippetsIQ.OrderByDescending(d => d.Name),
                "updated" => SnippetsIQ.OrderBy(d => d.LastUpdated),
                "created_desc" => SnippetsIQ.OrderByDescending(d => d.Created),
                "created" => SnippetsIQ.OrderBy(d => d.Created),
                _ => SnippetsIQ.OrderByDescending(d => d.LastUpdated),
            };
            int pageSize = 7;
            Snippets = await PaginatedList<Snippet>.CreateAsync(
                SnippetsIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
