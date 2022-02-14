using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using OneCall.Framework.PubSub;
using OneCall.Framework.PubSub.Factories;
using OneCall.Framework.PubSub.Publisher;
using OneCall.Framework.PubSub.Subscriber;

namespace ReferralOrchAPITest.SetUp.Subscriber
{
    public class Factory
    {
        private static readonly ServiceBusSettings ServiceBusSettings = new ServiceBusSettings()
        {
            ServiceBusConnectionString =
                "Endpoint=sb://onecall-servicebus-qa.servicebus.windows.net/;SharedAccessKeyName=csrd-sendlisten-rule;SharedAccessKey=2vsU3aDy76vNxE+CIXOPueq/3b5hIgboItU8LgBSmPQ=",
            TopicName = "csrd",
            SubscriptionName = "ReferralOrchAPITest"
        };

        public static EventSubscriber CreateEventSubscriber()
        {
            var subscriptionClientFactory = new DefaultSubscriptionClientFactory();
            var eventPublisher = CreateEventPublisher();
            return new EventSubscriber(ServiceBusSettings,
                subscriptionClientFactory, eventPublisher, new EventHandlerFactory(), new Logger<EventSubscriber>(new NullLoggerFactory()));
        }

        public static EventPublisher CreateEventPublisher() => 
            new EventPublisher(ServiceBusSettings, new DefaultTopicClientFactory());
    }
}