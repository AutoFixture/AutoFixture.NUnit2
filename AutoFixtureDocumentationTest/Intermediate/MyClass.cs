﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.AutoFixtureDocumentationTest.Intermediate
{
    public class MyClass
    {
        private readonly IMyInterface d;

        public MyClass(IMyInterface mi)
        {
            this.d = mi;
        }

        public int CalculateSumOfThings()
        {
            return this.d.ThingNumbers.Sum();
        }
    }
}
