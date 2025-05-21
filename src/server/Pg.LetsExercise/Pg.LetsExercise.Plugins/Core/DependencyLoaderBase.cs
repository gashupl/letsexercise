using System;
using System.Collections.Generic;

namespace Pg.LetsExercise.Plugins.Core
{
    public class DependencyLoaderBase : IDependencyLoader
    {
        protected Dictionary<Type, Type> dependencies = new Dictionary<Type, Type>();

        public I Get<I>() where I : class
        {
            return dependencies[typeof(I)] != null ? (I)Activator.CreateInstance(dependencies[typeof(I)]) : null;
        }

        public void Register<I, C>() where C : I
        {
            dependencies.Add(typeof(I), typeof(C));
        }

    }
}
