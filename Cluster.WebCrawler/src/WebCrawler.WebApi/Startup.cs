using Akka.Actor;
using Akka.Routing;
using Owin;
using WebCrawler.Messages.Actors;
using WebCrawler.WebApi.Actors;

[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(WebCrawler.WebApi.Startup), "ApplicationShutdown")]

namespace WebCrawler.WebApi
{
    public class Startup
    {
        protected static ActorSystem ActorSystem;

        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();

            CreateActorSystem();
        }

        private void CreateActorSystem()
        {
            ActorSystem = ActorSystem.Create("webcrawler");

            var router = ActorSystem.ActorOf(Props.Create(() => new RemoteJobActor()).WithRouter(FromConfig.Instance), "tasker");
            SystemActors.CommandProcessor = ActorSystem.ActorOf(Props.Create(() => new CommandProcessor(router)),
                "commands");
            SystemActors.SignalRActor = ActorSystem.ActorOf(Props.Create(() => new SignalRActor()), "signalr");
        }
    }
}