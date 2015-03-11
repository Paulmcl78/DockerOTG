using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DockerOTG.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/

        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("test")]
        public IActionResult Hometest()
        {
            return View();
        }
    }
}
