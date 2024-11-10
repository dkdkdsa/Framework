using Framework.Core;

namespace Framework.DI
{

    public sealed class MonoInstaller : MonoInstallerBase
    {
        protected sealed override void InstallBinding(DIContainer container)
        {
            var singletons = Singletons.GetSingletons();
            foreach (var singleton in singletons)
            {
                container.Bind(singleton.Key, singleton.Value);
            }
        }
    }
}
