namespace Pg.LetsExercise.Plugins.Core
{
    public interface IDependencyLoader
    {
        void Register<I, C>() where C : I;

        I Get<I>() where I : class; 

    }
}
