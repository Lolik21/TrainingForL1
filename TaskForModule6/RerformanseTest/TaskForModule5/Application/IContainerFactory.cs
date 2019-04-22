using Unity;

namespace TaskForModule5.Application
{
    public interface IContainerFactory
    {
        IUnityContainer ConfigureApplicationContainer();
    }
}