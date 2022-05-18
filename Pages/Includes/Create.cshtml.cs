#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DoItInCpp.Models;

namespace DoItInCpp.Pages.Includes
{
    public class CreateModel : PageModel
    {
        private readonly DoItInCppContext _context;

        public CreateModel(DoItInCppContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Include Include { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var emptyInclude = new Include();
            if (await TryUpdateModelAsync<Include>(
                emptyInclude,
                "Include",   // Prefix for form value.
                d => d.Name))
            {
                // check for valid cplusplus link, https://stackoverflow.com/a/3808841, http://www.dotnetthoughts.net/2009/10/14/how-to-check-remote-file-exists-using-c/
                try
                {
                    //Creating the HttpWebRequest
                    HttpWebRequest request = WebRequest.Create($"https://cplusplus.com/reference/{emptyInclude.Name}/") as HttpWebRequest;
                    //Setting the Request method HEAD, you can also use GET too.
                    request.Method = "HEAD";
                    //Getting the Web Response.
                    HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                    //Returns TRUE if the Status code == 200
                    if(response.StatusCode == HttpStatusCode.OK) {
                        response.Close();
                        _context.Includes.Add(emptyInclude);
                        await _context.SaveChangesAsync();
                        return RedirectToPage("./Index");
                    }
                    response.Close();
                } catch {
                    Console.WriteLine("HTTP failed");
                }
            }

            return Page();
        }
    }
}
