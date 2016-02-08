using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using NetMQ;
using NetMQ.Sockets;

namespace MQSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly NetMQContext m_context;
        private string m_serviceAddress;

        public HomeController(NetMQContext context)
        {
            m_context = context;
            m_serviceAddress = "tcp://127.0.0.1:10001";
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public int Calc(int a, int b)
        {
            using (var requestSocket = m_context.CreateRequestSocket())
            {
                requestSocket.Connect(m_serviceAddress);

                NetMQMessage message = new NetMQMessage();

                // converting to string, not most efficient but will do for our example
                message.Append(a.ToString());
                message.Append(b.ToString());

                requestSocket.SendMultipartMessage(message);

                var replyMessage = requestSocket.ReceiveMultipartMessage();
                string result = replyMessage.Pop().ConvertToString();

                return Convert.ToInt32(result);
            }
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
            using (var pubSocket = new PublisherSocket())
            {
                pubSocket.Options.SendHighWatermark = 1000;
                pubSocket.Bind(m_serviceAddress);
                pubSocket.SendMoreFrame("TopicA").SendFrame(message);
            }

            return View("Index");
        }
    }
}