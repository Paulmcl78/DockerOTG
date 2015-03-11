using System;
using DockerOTG.Model;
using Apache.NMS;
using Apache.NMS.ActiveMQ.Commands;
using Apache.NMS.Util;
using ServiceStack.Redis;

namespace DockerOTG.Common
{
    public class Login : ILogin
    {
        public bool LoginSuccessful(LoginViewModel model)
        {
            var clientManager = new BasicRedisClientManager("ec2-52-11-220-172.us-west-2.compute.amazonaws.com:6379");

            using (var redisClient = clientManager.GetClient())
            {
                var username = redisClient.Get<string>("UserKey");

                if(!string.IsNullOrEmpty(username))
                {
                    if(model.UserName.ToLower().Equals(username.ToLower()))
                    {
                        raiseLoginEvent(model.UserName, true);
                        return true;
                    }
                }
            }

            raiseLoginEvent(model.UserName, false);
            return false;

        }


        private void raiseLoginEvent(string name, bool success)
        {
            string message = string.Format("{1} has failed to login",name);
            if(success)
            {
                message = string.Format("{1} has successfully logged in", name);
            }

            var connectionFactory = new NMSConnectionFactory("tcp://ec2-52-11-191-106.us-west-2.compute.amazonaws.com:61616");

            using (var connection = connectionFactory.CreateConnection())
            {
                connection.Start();

                using (var session = connection.CreateSession())
                {
                    using(var producer =  session.CreateProducer(SessionUtil.GetQueue(session,"Login.Event.Q")))
                    {
                        var message = new ActiveMQBytesMessage();
                        message.Content = System.Text.Encoding.ASCII.GetBytes(message);

                        producer.Send(message);
                    }

                       
                }

            }
        }
    }
}