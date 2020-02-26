using Autofac;
using Microsoft.Extensions.Configuration;
using TrailTracker.API.Data;

namespace TrailTracker.API.Modules
{
    public class TrailRepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(ctx => new TrailsRepository(
                ctx.Resolve<IConfiguration>()
                )).As<ITrailsRepository>().SingleInstance();
        }

    }
}
