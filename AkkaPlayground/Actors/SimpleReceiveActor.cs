using System;
using Akka.Actor;
using Akka.Util;

namespace AkkaPlayground.Actors
{
    public class SimpleReceiveActor : ReceiveActor
    {
        public SimpleReceiveActor()
        {
            Receive<string>(str => str.StartsWith("Warning - "),
                str => WriteMessage($"/!\\ \"{str.Substring(10)}\""));

            Receive<string>(str => 
                WriteMessage($"\"{str}\""));

            Receive<int>(i => 
                WriteMessage($"{i} => {(i % 2 == 0 ? "even" : "odd")}"));
            
            Receive<int>(intVal => WriteMessage($"{intVal} est pair"), 
                intVal => intVal % 2 == 0);
            
            Receive<int>(intVal => WriteMessage($"{intVal} est impair") );
            
            ReceiveAny(msg => 
                WriteMessage(msg.ToString()));
        }

        private void WriteMessage(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{Self.Path.Name} says \"{msg}\"");
        }
    }
}
