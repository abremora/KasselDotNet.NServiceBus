using KasselDotNet.NServiceBus.BeerPub.Core;
using NServiceBus;
using System;
using System.Threading.Tasks;

namespace KasselDotNet.NServiceBus.BeerPub.Client {
    class Program {
        static async Task AsyncMain() {
            Console.WriteLine("##### Client #####");
            var endpoint = await CreateEndpoint();

            bool exit = false;
            while (!exit) {
                Console.WriteLine("----------------------");
                Console.WriteLine("Your order:");
                Console.WriteLine("[1]: Order beer.");
                Console.WriteLine("[2]: Beer was drunk.");
                Console.WriteLine("[x]: Exit.");
                var key = Console.ReadKey();
                Console.WriteLine();
                switch (key.Key) {
                    case ConsoleKey.D1:
                        Console.WriteLine("Your Name?");
                        var name = Console.ReadLine();
                        Console.WriteLine("How many beers do you want?");
                        var beers = Console.ReadLine();
                        await endpoint.Send(new OrderBeerCommand { Name = name, Amount = beers });
                        continue;
                    case ConsoleKey.D2:
                        Console.WriteLine("Which brand?");
                        var brand = Console.ReadLine();
                        await endpoint.Publish(new BeerWasDrunkEvent { Brand = brand });
                        continue;
                    case ConsoleKey.X:
                        exit = true;
                        break;
                }
            }

            await endpoint.Stop();
            Logga.Log("Endpoint stopped.");
        }

        static async Task<IEndpointInstance> CreateEndpoint() {
            var endpointName = "KasselDotNet.NServiceBus.BeerPub.Client";
            Console.Title = endpointName;

            var endpointConfiguration = new EndpointConfiguration(endpointName);
            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            endpointConfiguration.UseSerialization<JsonSerializer>();
            endpointConfiguration.UseTransport<MsmqTransport>();
            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.AuditProcessedMessagesTo("audit");

            return await Endpoint.Start(endpointConfiguration)
                 .ConfigureAwait(false);
        }

        static void Main(string[] args) {
            try {
                AsyncMain().GetAwaiter().GetResult();
            }
            catch (Exception ex) {
                Logga.Log(ex);
            }

            Console.ReadKey();
        }
    }
}
