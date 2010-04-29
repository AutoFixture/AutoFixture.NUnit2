﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Ploeh.TestTypeFoundation;
using Ploeh.AutoFixture.Kernel;
using System.Linq.Expressions;

namespace Ploeh.AutoFixtureUnitTest.Kernel
{
    public class NullSpecifiedSpecimenCommandTest
    {
        [Fact]
        public void SutIsSpecifiedSpecimenCommand()
        {
            // Fixture setup
            // Exercise system
            var sut = new NullSpecifiedSpecimenCommand<PropertyHolder<object>, object>(ph => ph.Property);
            // Verify outcome
            Assert.IsAssignableFrom<ISpecifiedSpecimenCommand<PropertyHolder<object>>>(sut);
            // Teardown
        }

        [Fact]
        public void InitializeWithNullExpressionThrows()
        {
            // Fixture setup
            // Exercise system and verify outcome
            Assert.Throws<ArgumentNullException>(() => new NullSpecifiedSpecimenCommand<PropertyHolder<object>, object>(null));
            // Teardown
        }

        [Fact]
        public void InitializeWithNonMemberExpressionWillThrow()
        {
            // Fixture setup
            Expression<Func<object, object>> invalidExpression = obj => obj;
            // Exercise system and verify outcome
            Assert.Throws<ArgumentException>(() => new NullSpecifiedSpecimenCommand<object, object>(invalidExpression));
            // Teardown
        }

        [Fact]
        public void InitializeWithMethodExpressionWillThrow()
        {
            // Fixture setup
            Expression<Func<object, string>> methodExpression = obj => obj.ToString();
            // Exercise system and verify outcome
            Assert.Throws<ArgumentException>(() => new NullSpecifiedSpecimenCommand<object, string>(methodExpression));
            // Teardown
        }

        [Fact]
        public void InitializeWithReadOnlyPropertyExpressionWillThrow()
        {
            // Fixture setup
            Expression<Func<SingleParameterType<object>, object>> readOnlyPropertyExpression = sp => sp.Parameter;
            // Exercise system and verify outcome
            Assert.Throws<ArgumentException>(() => new NullSpecifiedSpecimenCommand<SingleParameterType<object>, object>(readOnlyPropertyExpression));
            // Teardown
        }

        [Fact]
        public void ExecuteDoesNotThrow()
        {
            // Fixture setup
            var sut = new NullSpecifiedSpecimenCommand<PropertyHolder<object>, object>(ph => ph.Property);
            // Exercise system and verify outcome
            Assert.DoesNotThrow(() => sut.Execute(null, null));
            // Teardown
        }

        [Fact]
        public void IsSatisfiedByNullThrows()
        {
            // Fixture setup
            var sut = new NullSpecifiedSpecimenCommand<PropertyHolder<object>, object>(ph => ph.Property);
            // Exercise system and verify outcome
            Assert.Throws<ArgumentNullException>(() => sut.IsSatisfiedBy(null));
            // Teardown
        }

        [Fact]
        public void IsSatisfiedByReturnsFalseForAnonymousRequest()
        {
            // Fixture setup
            var request = new object();
            var sut = new NullSpecifiedSpecimenCommand<PropertyHolder<object>, object>(ph => ph.Property);
            // Exercise system
            bool result = sut.IsSatisfiedBy(request);
            // Verify outcome
            Assert.False(result);
            // Teardown
        }

        [Fact]
        public void IsSatisfiedByReturnsFalseForOtherProperty()
        {
            // Fixture setup
            var request = typeof(DoublePropertyHolder<object, object>).GetProperty("Property1");
            var sut = new NullSpecifiedSpecimenCommand<DoublePropertyHolder<object, object>, object>(ph => ph.Property2);
            // Exercise system
            bool result = sut.IsSatisfiedBy(request);
            // Verify outcome
            Assert.False(result);
            // Teardown
        }

        [Fact]
        public void IsSatisfiedByReturnsTrueForIdentifiedProperty()
        {
            // Fixture setup
            var request = typeof(DoublePropertyHolder<object, object>).GetProperty("Property1");
            var sut = new NullSpecifiedSpecimenCommand<DoublePropertyHolder<object, object>, object>(ph => ph.Property1);
            // Exercise system
            bool result = sut.IsSatisfiedBy(request);
            // Verify outcome
            Assert.True(result);
            // Teardown
        }
    }
}
