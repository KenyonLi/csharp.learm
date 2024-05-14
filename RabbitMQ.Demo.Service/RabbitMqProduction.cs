
namespace RabbitMQ.Demo.Service;

public class RabbitMqProduction
{
    /// <summary>
    /// 生产者
    /// </summary>
    public void CreateProduct()
    {
        #region 1、生产者
        {
            // 1、创建连接工厂
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672,
                Password = "guest",
                UserName = "guest",
                VirtualHost = "/"
            };
            using (var connection = factory.CreateConnection())
            {
                var channel = connection.CreateModel();
                // 2、定义队列
                channel.QueueDeclare(queue: "product-QueueName",
                                                 durable: false,// 消息持久化(防止rabbitmq宕机导致队列丢失风险)
                                                 exclusive: false,
                                                 autoDelete: false,
                                                 arguments: null);
                dataDayModel dataDayModel = new($"NO1{DateTime.Now.ToLongTimeString()}", "deviceName");
                string deviceJson = JsonConvert.SerializeObject(dataDayModel);
                var body = Encoding.UTF8.GetBytes(deviceJson);

                // 3、发送消息
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true; // 设置消息持久化（个性化控制）
                channel.BasicPublish(exchange: "",
                                     routingKey: "product-QueueName",
                                     basicProperties: properties,
                                     body: body);
            }
            Console.WriteLine("添加");
        }
        #endregion

    }
}
