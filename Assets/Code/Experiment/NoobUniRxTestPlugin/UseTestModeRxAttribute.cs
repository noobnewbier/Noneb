using System;
using Experiment.NoobUniRxPlugin;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace Experiment.NoobUniRxTestPlugin
{
    public class UseTestModeRxAttribute : Attribute, ITestAction    
    {
        public void BeforeTest(ITest test) => NoobSchedulers.ToTestMode();

        public void AfterTest(ITest test) => NoobSchedulers.ToDefaultMode();

        public ActionTargets Targets => ActionTargets.Default;
    }
}