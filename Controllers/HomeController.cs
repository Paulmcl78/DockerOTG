using Microsoft.AspNet.Mvc;
using MvcSample.Web.Models;

using Apache.NMS;
using Apache.NMS.ActiveMQ.Commands;
using Apache.NMS.Util;
using StackExchange.Redis;

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
		
		public User User(string username)
        {
            User user = new User()
            {
                Name = username,
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
					var message = new ActiveMQBytesMessage();
					message.Content = System.Text.Encoding.ASCII.GetBytes("Hello, World");

					producer.Send(message);
				}
			}
		}
		
		var multiPlexer = RedisFactory.GetInstance();

		IDatabase db = multiPlexer.GetDatabase();

		string value = db.StringGet("UserKey");

		var user = new User();
		
		if (String.IsNullOrWhiteSpace(value))
		{
			db.StringSet("UserKey", "AK");
		}
		else
		{
			user = new User(value);
		}
		
	    return View(user);
	}


    }
	
	public static class RedisFactory
    {
        private static ConnectionMultiplexer multiplexer;
        private static object lockObj;

        public static ConnectionMultiplexer GetInstance()
        {
            if (multiplexer == null)
            {
                lock (lockObj)
                {
                    if (multiplexer == null)
                    {
                        multiplexer =
                            ConnectionMultiplexer.Connect("ec2-52-11-220-172.us-west-2.compute.amazonaws.com:6379");
                    }
                }
            }

            return multiplexer;
        }
    }
}
