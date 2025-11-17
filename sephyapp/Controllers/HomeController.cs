using Microsoft.AspNetCore.Mvc;

namespace sephyapp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
