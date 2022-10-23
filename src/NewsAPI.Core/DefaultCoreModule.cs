using Autofac;
using NewsAPI.Core.Interfaces;
using NewsAPI.Core.Services;

namespace NewsAPI.Core;

public class DefaultCoreModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
        .RegisterType<NewsService>()
        .As<INewsService>()
        .InstancePerLifetimeScope();
    }
}
