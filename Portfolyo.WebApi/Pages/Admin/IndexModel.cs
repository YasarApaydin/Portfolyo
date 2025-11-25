using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Portfolyo.WebApi.Pages.Admin
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
         
        }
    }
}
