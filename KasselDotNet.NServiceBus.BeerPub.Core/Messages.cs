using NServiceBus;

namespace KasselDotNet.NServiceBus.BeerPub.Core {
    public class OrderBeerCommand : ICommand {
        public string Name { get; set; }
        public string Amount { get; set; }
    }

    public class BeerWasDrunkEvent : IEvent {
        public string Brand { get; set; }
    }

    // No marker interface. Use 
    public class PickUpYourBeerCommand {
        public string Name { get; set; }
    }
}
