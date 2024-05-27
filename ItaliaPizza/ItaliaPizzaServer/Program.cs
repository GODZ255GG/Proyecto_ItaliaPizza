using System;
using System.ServiceModel;

namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(Services.ItaliaPizzaService)))
            {
                try
                {
                    host.Open();
                    Console.WriteLine("Server is running");
                }
                catch
                {
                    Console.WriteLine("Server isn't running");
                }
                Console.ReadLine();
            }
        }
    }
}
