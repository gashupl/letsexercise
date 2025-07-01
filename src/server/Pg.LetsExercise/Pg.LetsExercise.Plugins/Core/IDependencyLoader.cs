using static Pg.LetsExercise.Plugins.PluginBase;

namespace Pg.LetsExercise.Plugins.Core
{
    public interface IDependencyLoader
    {
        void RegisterDefaults(LocalPluginContext localContext);

        void Register<I, C>() where I : class where C : class, I; 

        I Get<I>() where I : class; 
    }
}
