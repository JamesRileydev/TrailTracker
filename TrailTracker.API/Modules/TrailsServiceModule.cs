using Autofac;
using TrailTracker.API.Services;

namespace TrailTracker.API.Modules
{
    public class TrailsServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TrailsService>()
                .As<ITrailsService>()
                .SingleInstance();
        }
    }
}
