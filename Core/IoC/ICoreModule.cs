using Microsoft.Extensions.DependencyInjection;

namespace Core.IoC
{
    public interface ICoreModule
    {
        void Load(IServiceCollection serviceCollection);
    }
}
