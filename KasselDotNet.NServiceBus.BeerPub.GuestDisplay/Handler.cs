using System;
using System.Threading.Tasks;
using NServiceBus;
using KasselDotNet.NServiceBus.BeerPub.Core;

namespace KasselDotNet.NServiceBus.BeerPub.GuestDisplay {
    public class Handler :
        IHandleMessages<BeerWasDrunkEvent>,
        IHandleMessages<PickUpYourBeerCommand> {
        private IPromotionService _promotionService;

        public Handler(IPromotionService promotionService) {
            _promotionService = promotionService;
        }

        public Task Handle(PickUpYourBeerCommand message, IMessageHandlerContext context) {
            Console.WriteLine($"{message.Name}! Pick up your beer.");
            Console.WriteLine("Promotion: " + _promotionService.Message());
            return Task.CompletedTask;
        }

        public Task Handle(BeerWasDrunkEvent message, IMessageHandlerContext context) {
            Console.WriteLine($"Yippee! Thank you for drinking {message.Brand}.");
            Console.WriteLine("Promotion: " + _promotionService.Message());
            return Task.CompletedTask;
        }
    }
}
