﻿using System;
using System.Linq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using Ploeh.AutoFixture.NUnit.org;
using Ploeh.TestTypeFoundation;

namespace Ploe.AutoFixture.NUnit.org.UnitTest
{
    [TestFixture]
    public class FavorListsAttributeTest
    {
        [Test]
        public void SutIsAttribute()
        {
            // Fixture setup
            // Exercise system
            var sut = new FavorListsAttribute();
            // Verify outcome
            Assert.IsInstanceOf<CustomizeAttribute>(sut);
            // Teardown
        }

        [Test]
        public void GetCustomizationFromNullParameterThrows()
        {
            // Fixture setup
            var sut = new FavorListsAttribute();
            // Exercise system and verify outcome
            Assert.Throws<ArgumentNullException>(() =>
                sut.GetCustomization(null));
            // Teardown
        }

        [Test]
        public void GetCustomizationReturnsCorrectResult()
        {
            // Fixture setup
            var sut = new FavorListsAttribute();
            var parameter = typeof(TypeWithOverloadedMembers).GetMethod("DoSomething", new[] { typeof(object) }).GetParameters().Single();
            // Exercise system
            var result = sut.GetCustomization(parameter);
            // Verify outcome
            Assert.IsInstanceOf<ConstructorCustomization>(result);
            var invoker = (ConstructorCustomization)result;
            Assert.AreEqual(parameter.ParameterType, invoker.TargetType);
            Assert.IsInstanceOf<ListFavoringConstructorQuery>(invoker.Query);
            // Teardown
        }
    }
}
