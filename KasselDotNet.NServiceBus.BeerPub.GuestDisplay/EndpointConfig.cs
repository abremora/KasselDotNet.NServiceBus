using NServiceBus;

namespace KasselDotNet.NServiceBus.BeerPub.GuestDisplay {
    public class EndpointConfig : IConfigureThisEndpoint {
        public void Customize(EndpointConfiguration endpointConfiguration) {
            endpointConfiguration.EnableInstallers();
            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            endpointConfiguration.UseSerialization<JsonSerializer>();
            endpointConfiguration.UseTransport<MsmqTransport>();
            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.AuditProcessedMessagesTo("audit");

            UnobtrusiveMode(endpointConfiguration);

            // Dependency Injection
            endpointConfiguration.RegisterComponents(
                c => c.ConfigureComponent<PromotionService>(DependencyLifecycle.SingleInstance));
        }

        private static void UnobtrusiveMode(EndpointConfiguration endpointConfiguration) {
            var conventions = endpointConfiguration.Conventions();
            conventions.DefiningCommandsAs(
                t => t.Namespace.StartsWith("KasselDotNet.NServiceBus.")
                        && t.Name.EndsWith("Command"));
            conventions.DefiningEventsAs(
                t => t.Namespace.StartsWith("KasselDotNet.NServiceBus.")
                        && t.Name.EndsWith("Event"));
        }
    }
}
