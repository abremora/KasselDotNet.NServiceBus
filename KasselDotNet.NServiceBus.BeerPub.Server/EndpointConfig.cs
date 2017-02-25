using NServiceBus;
using NServiceBus.Logging;
using System;

namespace KasselDotNet.NServiceBus.BeerPub.Server {
    public class EndpointConfig : IConfigureThisEndpoint {
        public void Customize(EndpointConfiguration endpointConfiguration) {
            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            endpointConfiguration.UseSerialization<JsonSerializer>();
            endpointConfiguration.UseTransport<MsmqTransport>();
            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.AuditProcessedMessagesTo("audit");

            ChangeLogDirectory();
            RegisterUnitOfWork(endpointConfiguration);
            ConfigureRetry(endpointConfiguration);
            UnobtrusiveMode(endpointConfiguration);
        }

        private void ChangeLogDirectory() {
            // Change default directory for logging. Only via code :-(
            //var defaultFactory = LogManager.Use<DefaultFactory>();
            //defaultFactory.Directory("pathToLoggingDirectory");
        }

        private static void RegisterUnitOfWork(EndpointConfiguration endpointConfiguration) {
            endpointConfiguration.RegisterComponents(
                c => c.ConfigureComponent<MyUnitOfWork>(DependencyLifecycle.InstancePerCall)
                );
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

        private static void ConfigureRetry(EndpointConfiguration endpointConfiguration) {
            var recoverability = endpointConfiguration.Recoverability();
            recoverability.Immediate(i => i.NumberOfRetries(3));
            recoverability.Delayed(d => d.NumberOfRetries(3).TimeIncrease(TimeSpan.FromSeconds(2)));
        }
    }
}
