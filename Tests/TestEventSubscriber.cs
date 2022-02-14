using Microsoft.Extensions.Hosting;
using OneCall.Framework.PubSub;
using OneCall.Framework.PubSub.Factories;
using OneCall.Framework.PubSub.Publisher;
using OneCall.Framework.PubSub.Subscriber;
using RuleEngineAPITests;

namespace ReferralOrchAPITest.Tests
{
    public class TestEventSubscriber : EventSubscriber, IHostedService
    {
        public static readonly ServiceBusSettings serviceBusSettings = new ServiceBusSettings();
        public static readonly DefaultSubscriptionClientFactory subscriptionClientFactory = new DefaultSubscriptionClientFactory();
        public static readonly EventPublisher eventPublisher = new ServiceBusSetup().CreateEventPublisher();
        
        public TestEventSubscriber() : base(serviceBusSettings, subscriptionClientFactory, eventPublisher,
            new EventHandlerFactory(), null)
        {
        }
    }
}