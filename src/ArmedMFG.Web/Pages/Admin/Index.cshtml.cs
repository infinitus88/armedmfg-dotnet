using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ArmedMFG.ApplicationCore.Constants;
using ArmedMFG.Web.Extensions;
using ArmedMFG.Web.Services;
using ArmedMFG.Web.ViewModels;
using Microsoft.Extensions.Caching.Memory;

namespace ArmedMFG.Web.Pages.Admin;

[Authorize(Roles = ArmedMFG.BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS)]
public class IndexModel : PageModel
{
    public IndexModel()
    {

    }
}
