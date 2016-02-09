using System;
using System.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MQWorker
{
    internal class Program
    {


        private static void Main()
        {
            //var factory = new ConnectionFactory() { HostName = "localhost" };
            //using (var connection = factory.CreateConnection())
            //using (var channel = connection.CreateModel())
            //{
            //    channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);

            //    var consumer = new EventingBasicConsumer(channel);
            //    consumer.Received += (model, ea) =>
            //    {
            //        var body = ea.Body;
            //        var message = Encoding.UTF8.GetString(body);
            //        Console.WriteLine(" [x] Received {0}", message);
            //    };
            //    channel.BasicConsume(queue: "hello", noAck: true, consumer: consumer);

            //    Console.WriteLine(" Press [enter] to exit.");
            //    Console.ReadLine();
            //}

            while (true)
            {
                try
                {
                    var queue = new MessageQueue(@".\Private$\HelloWorld");
                    var message = queue.Receive(new TimeSpan(0, 0, 1));
                    message.Formatter = new XmlMessageFormatter(
                                        new String[] { "System.String, mscorlib" });
                    Console.WriteLine(message.Body.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("No Message");
                }
                Thread.Sleep(100);
            }


        }
    }
}