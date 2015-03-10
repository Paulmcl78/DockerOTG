using Microsoft.AspNet.Mvc;
using MvcSample.Web.Models;

namespace MvcSample.Web
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(User());
        }

        public User User()
        {
            User user = new User()
            {
                Name = "Team 31",
                Address = "NaviNet"
            };

            return user;
        }

	[Route("test")]
	public IActionResult Hometest()
	{
	    return View(User());
	}


    }
}
