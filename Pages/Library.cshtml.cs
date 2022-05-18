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

namespace DoItInCpp.Pages
{

    public class LibraryModel : PageModel
    {
        private readonly DoItInCppContext _context;

        public LibraryModel(DoItInCppContext context)
        {
            _context = context;
        }

        public List<KeyValuePair<String, List<Snippet>>> SnippetGroups;
        public string SizeSort { get; set; }
        public string NameSort { get; set; }
        public string UpdatedSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public string GroupBy { get; set; }

        public async Task OnGetAsync(string sortOrder,
                string currentFilter, string searchString, string groupBy)
        {
            CurrentSort = sortOrder;

            SizeSort = String.IsNullOrEmpty(sortOrder) ? "size" : "";
            NameSort = (sortOrder == "name") ? "name_desc" : "name";
            UpdatedSort = (sortOrder == "updated") ? "updated_desc" : "updated";

            if (searchString == null)
                searchString = currentFilter;
            
            CurrentFilter = searchString;

            if(String.IsNullOrEmpty(searchString))
                searchString = " ";

            if(String.IsNullOrEmpty(groupBy))
                groupBy = "Category";
            
            GroupBy = groupBy;

            IQueryable<Snippet> SnippetsIQ = _context.Snippets
                .Where(d => d.Name.Contains(searchString) || d.Description.Contains(searchString))
                .Include(m => m.Version)
                .Include(m => m.Includes)
                    .ThenInclude(i => i.Include)
                .Include(m => m.AddOns)
                    .ThenInclude(a => a.AddOn)
                .Include(m => m.Categories)
                    .ThenInclude(i => i.Category)
                .AsQueryable();

            // swithc case to avoid "Categorizable" polymorphism (cuz that can mess up data model in my experience)
            switch (GroupBy) {
                case "AddOn":
                    Dictionary<AddOn, List<Snippet>> SnippetDictAO = new Dictionary<AddOn, List<Snippet>>();
                    // build out snippet dict by addon
                    AddOn noadd = AddOn.GetBlank();
                    foreach(Snippet sn in SnippetsIQ)
                    {
                        if(sn.AddOns != null && sn.AddOns.Count != 0)
                        {
                            foreach(AddOnInSnippet aois in sn.AddOns)
                            {
                                if(!SnippetDictAO.ContainsKey(aois.AddOn))
                                    SnippetDictAO.Add(aois.AddOn, new List<Snippet>());
                                SnippetDictAO[aois.AddOn].Add(sn);
                            }
                        } else {
                            if(!SnippetDictAO.ContainsKey(noadd))
                                SnippetDictAO.Add(noadd, new List<Snippet>());
                            SnippetDictAO[noadd].Add(sn);
                        }
                    }

                    SnippetGroups = new List<KeyValuePair<string, List<Snippet>>>();
                    foreach(var pair in SnippetDictAO)
                        SnippetGroups.Add(new KeyValuePair<string, List<Snippet>>(pair.Key.Name, pair.Value));
                    break;
                case "Include":
                    Dictionary<Include, List<Snippet>> SnippetDictInc = new Dictionary<Include, List<Snippet>>();
                    // build out snippet dict by addon
                    Include noinc = Include.GetBlank();
                    foreach(Snippet sn in SnippetsIQ)
                    {
                        if(sn.Includes != null && sn.Includes.Count != 0)
                        {
                            foreach(IncludeInSnippet iis in sn.Includes)
                            {
                                if(!SnippetDictInc.ContainsKey(iis.Include))
                                    SnippetDictInc.Add(iis.Include, new List<Snippet>());
                                SnippetDictInc[iis.Include].Add(sn);
                            }
                        } else {
                            if(!SnippetDictInc.ContainsKey(noinc))
                                SnippetDictInc.Add(noinc, new List<Snippet>());
                            SnippetDictInc[noinc].Add(sn);
                        }
                    }

                    SnippetGroups = new List<KeyValuePair<string, List<Snippet>>>();
                    foreach(var pair in SnippetDictInc)
                        SnippetGroups.Add(new KeyValuePair<string, List<Snippet>>(pair.Key.Name, pair.Value));
                    break;
                case "Version":
                    Dictionary<LanguageVersion, List<Snippet>> SnippetDictVers = new Dictionary<LanguageVersion, List<Snippet>>();
                    // build out snippet dict by addon
                    foreach(Snippet sn in SnippetsIQ)
                    {
                        if(!SnippetDictVers.ContainsKey(sn.Version))
                            SnippetDictVers.Add(sn.Version, new List<Snippet>());
                        SnippetDictVers[sn.Version].Add(sn);
                    }

                    SnippetGroups = new List<KeyValuePair<string, List<Snippet>>>();
                    foreach(var pair in SnippetDictVers)
                        SnippetGroups.Add(new KeyValuePair<string, List<Snippet>>("C++" + pair.Key.Year_XX, pair.Value));
                    break;
                default:
                    Dictionary<Category, List<Snippet>> SnippetDictCat = new Dictionary<Category, List<Snippet>>();
                    // build out snippet dict by category
                    Category nocat = Category.GetBlank();
                    foreach(Snippet sn in SnippetsIQ)
                    {
                        if(sn.Categories != null && sn.Categories.Count != 0)
                        {
                            foreach(CategoryInSnippet cis in sn.Categories)
                            {
                                if(!SnippetDictCat.ContainsKey(cis.Category))
                                    SnippetDictCat.Add(cis.Category, new List<Snippet>());
                                SnippetDictCat[cis.Category].Add(sn);
                            }
                        } else {
                            if(!SnippetDictCat.ContainsKey(nocat))
                                SnippetDictCat.Add(nocat, new List<Snippet>());
                            SnippetDictCat[nocat].Add(sn);
                        }
                    }

                    SnippetGroups = new List<KeyValuePair<string, List<Snippet>>>();
                    foreach(var pair in SnippetDictCat)
                        SnippetGroups.Add(new KeyValuePair<string, List<Snippet>>(pair.Key.Name, pair.Value));
                    break;
            }
            

            SnippetGroups = sortOrder switch
            {
                "name" => SnippetGroups.OrderBy(d => d.Key).ToList(),
                "name_desc" => SnippetGroups.OrderByDescending(d => d.Key).ToList(),
                "updated" => SnippetGroups.OrderBy(d => d.Value.MaxBy(sn => sn.LastUpdated)).ToList(),
                "updated_desc" => SnippetGroups.OrderByDescending(d => d.Value.MaxBy(sn => sn.LastUpdated)).ToList(),
                "size" => SnippetGroups.OrderBy(d => d.Value.Count).ToList(),
                _ => SnippetGroups.OrderByDescending(d => d.Value.Count).ToList()
            };

            foreach(var group in SnippetGroups)
                group.Value.Sort((sn1, sn2) => String.Compare(sn1.Name, sn2.Name));
        }
    }
}
