using Autofac;
using Microsoft.Extensions.Options;
using TrailTracker.API.Configuration;
using TrailTracker.API.Data;

namespace TrailTracker.API.Modules
{
    public class TrailsRepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(ctx => new TrailsRepository(
                ctx.Resolve<IOptions<DbConfig>>()))
                  .As<ITrailsRepository>()
                  .SingleInstance();
        }
    }
}
