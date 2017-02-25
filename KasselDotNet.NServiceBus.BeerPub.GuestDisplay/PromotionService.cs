using KasselDotNet.NServiceBus.BeerPub.Core;

namespace KasselDotNet.NServiceBus.BeerPub.GuestDisplay {
    public class PromotionService : IPromotionService {
        public string Message() {
            return "Kassel .NET Usergroup";
        }
    }
}