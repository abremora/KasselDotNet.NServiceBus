using NServiceBus.UnitOfWork;
using System;
using System.Threading.Tasks;

namespace KasselDotNet.NServiceBus.BeerPub.Server {
    public class MyUnitOfWork : IManageUnitsOfWork {
        public Task Begin() {
            Console.WriteLine("-- MyUnitOfWork Start --");
            return Task.CompletedTask;
        }

        public Task End(Exception ex = null) {
            Console.WriteLine("-- MyUnitOfWork End --");
            return Task.CompletedTask;
        }
    }
}
