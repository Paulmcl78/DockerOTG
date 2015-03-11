using DockerOTG.Model;
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
            return View(new LoginViewModel());
        }



        [Route("test")]
        public IActionResult Hometest()
        {
            return View();
        }

        [Route("Home/Login")]
        public IActionResult Login(LoginViewModel model)
        {
            if(model.UserName == "test")
            {
                return RedirectToAction("Index", "Root");
            }

            ModelState.AddModelError(string.Empty, "Incorrect username or password");

            return View("Index");

        }
    }
}
