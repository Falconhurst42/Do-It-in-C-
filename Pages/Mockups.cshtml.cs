using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DoItInCpp.Pages;

public class MockupsModel : PageModel
{
    private readonly ILogger<MockupsModel> _logger;

    public MockupsModel(ILogger<MockupsModel> logger)
    {
        _logger = logger;
    }
    public void OnGet()
    {
    }
}