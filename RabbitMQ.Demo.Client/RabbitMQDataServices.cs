


namespace RabbitMQ.Demo;
public class RabbitMQDataServices
{
    //消费
    public void ReceivedMQData()
    {
        // 1、创建连接
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            Port = 5672,
            Password = "guest",
            UserName = "guest",
            VirtualHost = "/"
        };
        var connection = factory.CreateConnection();
        #region 1、工作队列(单消费者)
        var channel = connection.CreateModel();

        // 2、定义队列
        channel.QueueDeclare(queue: "product-QueueName",
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {

            Console.WriteLine($"model:{model}");
            try
            {
                byte[] body = ea.Body.ToArray();
                string message = Encoding.UTF8.GetString(body);
                //  callBack(message);
                Console.WriteLine($"数据解析：{ea.Body}");

                channel.BasicAck(ea.DeliveryTag, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"队列监听方法出错，{ex.Message}");
            }
        };

        channel.BasicConsume(queue: "product-QueueName",
                             autoAck: false,
                             consumer: consumer);
        #endregion
    }
}
