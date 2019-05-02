using Unity;

namespace MongoDB
{
    internal interface IContainerFactory
    {
        IUnityContainer ConfigureContainer();
    }
}