using Microsoft.AspNet.Mvc;
using MvcSample.Web.Models;

using Apache.NMS;
using Apache.NMS.ActiveMQ.Commands;
using Apache.NMS.Util;

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
		var connectionFactory = new NMSConnectionFactory("tcp://ec2-52-11-191-106.us-west-2.compute.amazonaws.com:61616");
            
		using (var connection = connectionFactory.CreateConnection())
		{
			connection.Start();

			using (var session = connection.CreateSession())
			{
				using (var producer = session.CreateProducer(SessionUtil.GetQueue(session, "Login.Event.Q")))
				{
					var message = new ActiveMQTextMessage("Hello, World");

					producer.Send(message);
				}
			}
		}
		
		
	    return View(User());
	}


    }
}
