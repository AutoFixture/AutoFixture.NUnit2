﻿using System;
using Ploeh.AutoFixture.Kernel;

namespace Ploeh.AutoFixture.NUnit.UnitTest
{
    internal class DelegatingSpecimenBuilder : ISpecimenBuilder
    {
        public DelegatingSpecimenBuilder()
        {
            OnCreate = (r, c) => new object();
        }

        public object Create(object request, ISpecimenContext context)
        {
            return OnCreate(request, context);
        }

        internal Func<object, ISpecimenContext, object> OnCreate { get; set; }
    }
}
