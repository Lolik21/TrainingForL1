namespace ContainerLibrary
{
    public interface IContainer
    {
        void Register<TResolve, TTarget>(bool isSingleton = false);
        void Register<TResolve>(bool isSingleton = false);
        TResolve Resolve<TResolve>();
    }
}
