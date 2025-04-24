using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pg.LetsExercise.Plugins.Core
{
    public interface IDependencyLoader
    {
        void Register(ILocalPluginContext localPluginContext); 
    }
}
