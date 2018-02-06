using System.Threading.Tasks;
using Akka.Actor;

namespace AkkaPlayground.Actors
{
    public class AsyncBiStateReceiveActor : ReceiveActor
    {
        public AsyncBiStateReceiveActor()
        {
            Become(HappyActor);
        }
        
        private void HappyActor()
        {
            ReceiveAnyAsync(async m => {
                Sender.Tell("I'm happy :)");
                Become(SadActor);
                await Task.Delay(50);
            });
        }
        
        private void SadActor()
        {
            ReceiveAnyAsync(async m => {
                Sender.Tell("I'm sad :(");
                Become(HappyActor);
                await Task.Delay(50);
            });
        }
    }
}