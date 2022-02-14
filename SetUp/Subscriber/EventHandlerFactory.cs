    using System;
    using System.Threading.Tasks;
    using OneCall.Framework.PubSub.Contracts;
    using OneCall.Framework.PubSub.Events;
    using OneCall.Framework.Shared.Utils;

    namespace ReferralOrchAPITest.SetUp.Subscriber
    {
        
        
        public class EventHandlerFactory : IEventHandlerFactory
        {
          
            public IEventHandler CreateEventHandler(string eventType, string sourceSystem)
            {
              
                //add more events
                return eventType switch
                {
                    "ReferralAuthNotificationSent" => new EventHandler(),
                    "ReferralAuthorizationEvaluated"=> new EventHandler(), 
                    "ReferralAuthorizationValidationFailed"=> new EventHandler(), 
                    "ReferralAuthorizationFailed"=> new EventHandler(),
                    "ReferralAuthorizationNotificationFailed"=> new EventHandler(),
                    "ReferralAuthNotificationDeliveryStatus"=> new EventHandler(),
                    _ => null
                };
            }
        }
        
        public class EventHandler : IEventHandler
        {
            public Task Handle(byte[] message)
            {
               
                    var deserializedData = SerializeUtility.Deserialize<ReceivedEventWrapper<object>>(message);
                    string data = deserializedData.Data.ToString().Split("\"referralId\":")[1].Split(",")[0];
                    if (data.Equals("2349913")||data.Equals("2349923")||data.Equals("2349691")||data.Equals("2349924"))
                    {
                        Tuple<string, string> tuple = new Tuple<string, string>(deserializedData.Type.ToString(), data);
                        GlobalSetUpFixture.Events.Add(tuple, deserializedData.Data.ToString());
                    }

                    return null;
               
            }
        }
    }