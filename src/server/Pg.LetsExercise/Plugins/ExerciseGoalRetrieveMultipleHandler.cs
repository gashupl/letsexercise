﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pg.LetsExercise.Plugins
{
    internal class ExerciseGoalRetrieveMultipleHandler : PluginBase
    {
        public ExerciseGoalRetrieveMultipleHandler(Type pluginClassName) : base(pluginClassName)
        {
        }

        // Entry point for custom business logic execution
        protected override void ExecuteDataversePlugin(ILocalPluginContext localPluginContext)
        {
            if (localPluginContext == null)
            {
                throw new ArgumentNullException(nameof(localPluginContext));
            }

            var context = localPluginContext.PluginExecutionContext;
            
        }
    }
}