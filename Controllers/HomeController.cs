using Microsoft.AspNet.Mvc;
using MvcSample.Web.Models;
using DockerTest.Models;

namespace MvcSample.Web
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(login());
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
		
		public LoginModel login()
		{
			return new LoginModel();
		
		}

	[Route("test")]
	public IActionResult Hometest()
	{
	    return View();
	}


    }
}
