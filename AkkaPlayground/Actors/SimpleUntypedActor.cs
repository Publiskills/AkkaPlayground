using System;
using Akka.Actor;

namespace AkkaPlayground.Actors
{
    public class SimpleUntypedActor : UntypedActor
    {
        protected override void OnReceive(object msg)
        {
            switch (msg)
            {
                case string str when str.StartsWith("Warning - "):
                    WriteMessage($"/!\\ \"{str.Substring(10)}\"");
                    break;
                case string str:
                    WriteMessage($"\"{str}\"");
                    break;
                case int i:
                    WriteMessage($"{i} => {(i % 2 == 0 ? "even" : "odd")}");
                    break;
                default:
                    WriteMessage(msg.ToString());
                    break;
            }
        }

        private void WriteMessage(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{Self.Path.Name} says \"{msg}\"");
        }
    }
}
