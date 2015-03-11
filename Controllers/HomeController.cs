using Microsoft.AspNet.Mvc;
using MvcSample.Web.Models;
using System;
using Apache.NMS;
using Apache.NMS.ActiveMQ.Commands;
using Apache.NMS.Util;
using ServiceStack.Redis;

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
		
		var user = User();
		
		var clientManager = new BasicRedisClientManager("ec2-52-11-220-172.us-west-2.compute.amazonaws.com:6379");

		using (var redisClient = clientManager.GetClient())
		{
			var userName = redisClient.Get<string>("UserKey");

			if (string.IsNullOrEmpty(userName))
			{
				bool result = redisClient.Set("UserKey", "AK");
			}
			else
			{
				user = User(userName);
			}

			long browseCount = redisClient.Increment("Browse Count", 1);
				
			ViewBag.BrowseCount = browseCount;
			ViewBag.ConfigUrl = System.Environment.GetEnvironmentVariable("ConfigStoreUrl");
		
			return View(user);
		}
	}


    }
}
