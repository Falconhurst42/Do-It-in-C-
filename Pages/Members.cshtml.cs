using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DoItInCpp.Pages;

public class MembersModel : PageModel
{
    private readonly ILogger<MembersModel> _logger;

    public MembersModel(ILogger<MembersModel> logger)
    {
        _logger = logger;
    }
    public void OnGet()
    {
    }
}