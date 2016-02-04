using System;
using System.Threading;
using NetMQ;
using NetMQ.Sockets;

namespace MQWorker
{
    internal class Program
    {
        private static void Main()
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
        }
    }
}