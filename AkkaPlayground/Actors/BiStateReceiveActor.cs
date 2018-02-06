using Akka.Actor;

namespace AkkaPlayground.Actors
{
    public class BiStateReceiveActor : ReceiveActor
    {
        public BiStateReceiveActor()
        {
            Become(HappyActor); // This actor will handle next message with the behaviour "HappyActor"
        }
        
        private void HappyActor()
        {
            ReceiveAny(m => {
                Sender.Tell("I'm happy :)");
                Become(SadActor); // This actor will handle next message with the behaviour "SadActor"
            });
        }
        
        private void SadActor()
        {
            ReceiveAny(m => {
                Sender.Tell("I'm sad :(");
                Become(HappyActor); // This actor will handle next message with the behaviour "HappyActor"
            });
        }
    }
}