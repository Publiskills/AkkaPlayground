using System;
using Akka.Actor;

namespace AkkaPlayground.Actors
{
    public class BiStateUntypedActor : UntypedActor
    {
        private readonly string _happyMessage;
        private readonly string _sadMessage;
        
        public BiStateUntypedActor(string happyMessage, string sadMessage)
        {
            _sadMessage = sadMessage;
            _happyMessage = happyMessage;
            
            BecomeStacked(Happy);
        }
        
        private UntypedReceive Happy => 
            _ =>
            {
                WriteMessage("I'm happy :)"); 
                BecomeStacked(Sad);
            };
        
        private UntypedReceive Sad =>
            _ =>
            {
                WriteMessage("I'm sad :("); 
                UnbecomeStacked();
            }; 

        
        protected override void OnReceive(object message)
        {
        }

        public static Props CreateProps(string happyMessage, string sadMessage)
        {
            return Props.Create(() => new BiStateUntypedActor(happyMessage, sadMessage));
        }
        
        private void WriteMessage(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{Self.Path.Name} says \"{msg}\"");
        }
    }
}