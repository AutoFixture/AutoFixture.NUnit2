﻿using System.Linq;
using NUnit.Framework;
using Ploeh.AutoFixture.NUnit.org;

namespace Ploe.AutoFixture.NUnit.org.UnitTest
{
    [TestFixture]
    public class DependencyConstraints
    {
        [Test]
        [TestCase("Moq")]
        [TestCase("Rhino.Mocks")]
        public void AutoFixtureXunitDoesNotReference(string assemblyName)
        {
            // Fixture setup
            // Exercise system
            var references = typeof(AutoDataAttribute).Assembly.GetReferencedAssemblies();
            // Verify outcome
            Assert.False(references.Any(an => an.Name == assemblyName));
            // Teardown
        }

        [Test]
        [TestCase("Moq")]
        [TestCase("Rhino.Mocks")]
        public void AutoFixtureXunitUnitTestsDoNotReference(string assemblyName)
        {
            // Fixture setup
            // Exercise system
            var references = GetType().Assembly.GetReferencedAssemblies();
            // Verify outcome
            Assert.False(references.Any(an => an.Name == assemblyName));
            // Teardown
        }
    }
}
