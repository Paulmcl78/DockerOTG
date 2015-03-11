using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DockerOTG.Controllers
{
    public class RootController : Controller
    {
        // GET: /<controller>/
        [Route("/Success")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
