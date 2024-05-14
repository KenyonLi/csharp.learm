// See https://aka.ms/new-console-template for more information
using RabbitMQ.Demo.Service;

Console.WriteLine("生产者-服务已开始启动......");

while (true)
{
    new RabbitMqProduction().CreateProduct();
    Thread.Sleep(10);
    Console.WriteLine("休息一秒");
}
