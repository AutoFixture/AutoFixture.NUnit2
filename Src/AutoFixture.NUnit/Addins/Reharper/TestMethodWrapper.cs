﻿//http://youtrack.jetbrains.com/issue/RSRP-205480

using NUnit.Core;

namespace Ploeh.AutoFixture.NUnit.Addins.Reharper
{
    internal class TestMethodWrapper : NUnitTestMethod
    {
        public TestMethodWrapper(NUnitTestMethod testMethod) 
            : base(testMethod.Method)
        {   
        }
 
        public override TestResult Run(EventListener listener, ITestFilter filter)
        {
            return base.Run(new EventListenerWrapper(listener), filter);
        }
    }
}