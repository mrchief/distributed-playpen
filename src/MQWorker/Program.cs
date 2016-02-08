using System;
using System.Threading;
using System.Threading.Tasks;
using NetMQ;
using NetMQ.Sockets;

namespace MQWorker
{
    internal class Program
    {
        /*private static void Main()
        {
            using (NetMQContext context = NetMQContext.Create())
            {
                using (var responseSocket = context.CreateResponseSocket())
                {
                    responseSocket.Bind("tcp://*:10001");

                    while (true)
                    {
                        var requestMessage = responseSocket.ReceiveMultipartMessage();
                        string a = requestMessage.Pop().ConvertToString();
                        string b = requestMessage.Pop().ConvertToString();

                        int aNumber = Convert.ToInt32(a);
                        int bNumber = Convert.ToInt32(b);

                        string result = (aNumber + bNumber).ToString();

                        NetMQMessage responseMessage = new NetMQMessage();
                        responseMessage.Append(result);

                        responseSocket.SendMultipartMessage(responseMessage);
                    }
                }
            }
        }*/

        private static void Main()
        {
            /*Task.Factory.StartNew(() =>
            {
                using (var xpubSocket = new XPublisherSocket("@tcp://127.0.0.1:1234"))
                using (var xsubSocket = new XSubscriberSocket("@tcp://127.0.0.1:5678"))
                {
                    Console.WriteLine("Intermediary started, and waiting for messages");

                    // proxy messages between frontend / backend
                    var proxy = new Proxy(xsubSocket, xpubSocket);

                    // blocks indefinitely
                    proxy.Start();
                }
            });*/
            Console.WriteLine("Subscriber started for TopicA");

            using (var subSocket = new SubscriberSocket())
            {
                subSocket.Options.ReceiveHighWatermark = 1000;
                subSocket.Connect("tcp://*:10001");
                subSocket.Subscribe("TopicA");
                Console.WriteLine("Subscriber socket connecting...");
                while (true)
                {
                    Console.WriteLine("waiting");
                    var messageTopicReceived = subSocket.ReceiveFrameString();
                    Console.WriteLine("messageTopicReceived");
                    var messageReceived = subSocket.ReceiveFrameString();
                    Console.WriteLine($"received: {messageReceived}");
                }
            }

        }
    }
}