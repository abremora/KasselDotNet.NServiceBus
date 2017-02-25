using NServiceBus;
using System.Threading.Tasks;
using System;

namespace KasselDotNet.NServiceBus.BeerPub.GuestDisplay {
    public class Bootstrapper :
    IWantToRunWhenEndpointStartsAndStops {
        public Task Start(IMessageSession session) {
            Console.WriteLine("##### Guest TV #####");
            // Do startup actions here.
            // Either mark Start method as async or do the following
            return Task.CompletedTask;
        }

        public Task Stop(IMessageSession session) {
            // Do cleanup actions here.
            // Either mark Stop method as async or do the following
            return Task.CompletedTask;
        }
    }
}