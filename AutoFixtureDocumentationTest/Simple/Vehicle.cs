﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.AutoFixtureDocumentationTest.Simple
{
    public class Vehicle
    {
        public Vehicle()
        {
            this.Wheels = 4;
        }

        public int Wheels { get; set; }
    }
}
