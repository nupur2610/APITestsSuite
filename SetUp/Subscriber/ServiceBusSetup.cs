// using System.Collections.Generic;
// using System.Threading;
// using System.Threading.Tasks;
// using Microsoft.Extensions.Logging;
// using Microsoft.Extensions.Logging.Abstractions;
// using OneCall.Framework.PubSub;
// using OneCall.Framework.PubSub.Events;
// using OneCall.Framework.PubSub.Factories;
// using OneCall.Framework.PubSub.Publisher;
// using OneCall.Framework.PubSub.Subscriber;
//
// namespace ReferralOrchAPITest.SetUp.Subscriber
// {
//     public class ServiceBusSetup
//     {
//         public static  List<object> _messages = new List<object>();
//         private EventSubscriber _subscriber = null;
//
//         public ServiceBusSetup()
//         {
//             CreateEventSubscriber();
//         }
//         public EventPublisher CreateEventPublisher()
//         {
//             
//             return new EventPublisher(_serviceBusSettings, new DefaultTopicClientFactory());
//         }
//
//         public EventSubscriber CreateEventSubscriber()
//         {
//             if (_subscriber == null)
//             {
//                 var subscriptionClientFactory = new DefaultSubscriptionClientFactory();
//                 var eventPublisher = CreateEventPublisher();
//                 _subscriber = new EventSubscriber(_serviceBusSettings,
//                     subscriptionClientFactory, eventPublisher, new EventHandlerFactory(), new Logger<EventSubscriber>(new NullLoggerFactory()));
//             }
//
//             return _subscriber;
//         }
//
//         public async Task PublishEvent()
//         {
//             var publisher = CreateEventPublisher();
//             await publisher.PublishAsync(PublishedEventWrapper
//                 .Create("eventName", "event source", new object()));
//         }
//
//         public async Task ListenEvent()
//         
//         {
//             await _subscriber.StartAsync(default);
//         }
//
//         public async Task StopListen()
//         {
//             await _subscriber.StopAsync(default);
//         }
//
//         public async Task PurgeMessages()
//         {
//             await ListenEvent();
//             Thread.Sleep(10000);
//             await StopListen();
//         }
//     }
// }