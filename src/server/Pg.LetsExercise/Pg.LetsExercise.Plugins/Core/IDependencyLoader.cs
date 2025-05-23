namespace Pg.LetsExercise.Plugins.Core
{
    public interface IDependencyLoader
    {
        void RegisterDefaults(ILocalPluginContext context);

        void Register<I, C>() where I : class where C : class, I; 

        I Get<I>() where I : class; 
    }
}
