using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using OneCall.Framework.PubSub.Subscriber;
using ReferralOrchAPITest.Helper;
using ReferralOrchAPITest.SetUp.Subscriber;
using Xunit;

namespace ReferralOrchAPITest.SetUp
{
    public class GlobalSetUpFixture : IDisposable
    {
        public static readonly Dictionary<Tuple<string,string>,object> Events = new  Dictionary<Tuple<string,string>,object>();
        private static EventSubscriber _listener;
        public ApiClient Client { get; }

        static GlobalSetUpFixture()
        {
            SetUpListener();
            Events.Clear();
        }

        public GlobalSetUpFixture() => 
            Client = new ApiClient(new HttpClient());

        private static void SetUpListener()
        {
            _listener = Factory.CreateEventSubscriber();
            _listener.StartAsync(default);
            Thread.Sleep(1000 * 6);
            Console.WriteLine($"Total messages purged : {Events.Count}");
        }

        public void Dispose() =>
            Client.Dispose();
    }
}