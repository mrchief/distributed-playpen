using System;
using System.Messaging;
using System.Text;
using System.Web.Mvc;
using RabbitMQ.Client;

namespace MQSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult About() 
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Publish(string message)
        {
            //var factory = new ConnectionFactory() { HostName = "localhost" };
            //using (var connection = factory.CreateConnection())
            //using (var channel = connection.CreateModel())
            //{
            //    channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);

            //    var body = Encoding.UTF8.GetBytes(message);

            //    channel.BasicPublish(exchange: "", routingKey: "hello", basicProperties: null, body: body);
            //    Console.WriteLine(" [x] Sent {0}", message);
            //}


            var queue = new MessageQueue(@".\Private$\HelloWorld");
            var msg = new Message
            {
                Body = message,
                Label = $"Presentation at {DateTime.Now}"
            };
            queue.Send(msg);


            return View("Index");
        }
    }
}