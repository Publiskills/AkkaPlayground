using System;
using System.Collections.Generic;
using Akka.Actor;
using Akka.Event;
using AkkaPlayground.Actors;

namespace AkkaPlayground
{
    internal partial class Program
    {
        private static void Main()
        {
            var actorSystem = ActorSystem.Create("AkkaPlaygroundActorSystem", GetAkkaConfigurationFromHoconFile());

            var mainActor = actorSystem.ActorOf<MainActor>("mainUserActor");

            //Send some messages to awake the rootActor every 2 seconds
            for (var i = 0; i < 10; i++)
            {
                mainActor.Tell("heartbeat");
                System.Threading.Thread.Sleep(2000);
            }

            actorSystem.WhenTerminated.Wait();
        }
    }
}
