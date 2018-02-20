using System;
using System.Collections.Generic;
using Akka.Actor;

namespace AkkaPlayground.Actors
{
    public class MainActor : UntypedActor
    {
        private List<IActorRef> _actorRefs;

        public MainActor()
        {
            _actorRefs = new List<IActorRef>
            {
                Context.ActorOf<SimpleUntypedActor>("simpleUntypedActor"),
                Context.ActorOf<SimpleReceiveActor>("simpleReceiveActor"),
                Context.ActorOf(BiStateUntypedActor.CreateProps("I'm happy :)", "I'm sad :)"), "biStateUntypedActor"),
                Context.ActorOf<BiStateReceiveActor>("biStateReceiveActor"),
                Context.ActorOf<AsyncBiStateReceiveActor>("asyncBiStateReceiveActor"),
            };
        }
        
        protected override void OnReceive(object message)
        {
            if (message.Equals("heartbeat"))
            {
                _actorRefs.ForEach(actor =>
                {
                    actor.Tell("simple message");
                    actor.Tell("Warning - Important message");
                    actor.Tell(DateTimeOffset.UtcNow.Millisecond);
                    actor.Tell(new
                    {
                        val1 = $"dummy value {DateTimeOffset.UtcNow.Millisecond}",
                        val2 = "other dummy value"
                    });
                });
            }
            else
            {
                WriteMessage(message.ToString());
            }
        }

        private void WriteMessage(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($">> \"{Self.Path.Name}\" receives message \"{msg}\" from \"{Sender.Path}\"");
        }
    }
}