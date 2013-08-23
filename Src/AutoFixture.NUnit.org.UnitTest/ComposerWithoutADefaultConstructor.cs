﻿using System;
using Ploeh.AutoFixture.Kernel;

namespace Ploe.AutoFixture.NUnit.org.UnitTest
{
    internal class ComposerWithoutADefaultConstructor : DelegatingFixture
    {
        public ComposerWithoutADefaultConstructor(Func<ISpecimenBuilder> onCompose)
        {
            if (onCompose == null)
                throw new ArgumentNullException("onCompose");
        }
    }
}
