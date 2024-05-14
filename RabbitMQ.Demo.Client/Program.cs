// See https://aka.ms/new-console-template for more information


Console.WriteLine("消费者-服务已开始启动 ......");

new RabbitMQDataServices().ReceivedMQData();


bool b = true;
while (b)
{
    if (Console.ReadLine().ToLower() == "exit") {
        
        b = false;
        Console.WriteLine( "异常触发");
    }
    Thread.Sleep(1000);
}
