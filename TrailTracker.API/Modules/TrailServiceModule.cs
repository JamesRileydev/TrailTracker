﻿using Autofac;
using TrailTracker.API.Services;
using Serilog;
using Microsoft.Extensions.Logging;

namespace TrailTracker.API.Modules
{
    public class TrailServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TrailService>()
                .SingleInstance();
        }
    }
}
