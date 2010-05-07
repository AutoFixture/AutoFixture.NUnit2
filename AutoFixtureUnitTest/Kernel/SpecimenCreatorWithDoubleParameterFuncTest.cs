﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ploeh.AutoFixture.Kernel;
using Xunit;

namespace Ploeh.AutoFixtureUnitTest.Kernel
{
    public class SpecimenCreatorWithDoubleParameterFuncTest
    {
        [Fact]
        public void SutIsSpecimenBuilder()
        {
            // Fixture setup
            Func<string, int, object> dummyFunc = (x, y) => new object();
            // Exercise system
            var sut = new SpecimenCreator<string, int, object>(dummyFunc);
            // Verify outcome
            Assert.IsAssignableFrom<ISpecimenBuilder>(sut);
            // Teardown
        }

        [Fact]
        public void InitializeWithNullFuncThrows()
        {
            // Fixture setup
            // Exercise system and verify outcome
            Assert.Throws<ArgumentNullException>(() => new SpecimenCreator<int, string, object>((Func<int, string, object>)null));
            // Teardown
        }

        [Fact]
        public void CreateWithNullContainerThrows()
        {
            // Fixture setup
            var sut = new SpecimenCreator<object, object, object>((x, y) => x);
            var dummyRequest = new object();
            // Exercise system and verify outcome
            Assert.Throws<ArgumentNullException>(() =>
                sut.Create(dummyRequest, null));
            // Teardown
        }

        [Fact]
        public void CreateWillReturnCorrectResult()
        {
            // Fixture setup
            var expectedSpecimen = new object();

            var param1 = new { ExpectedRequest = typeof(decimal), Specimen = (object)1m };
            var param2 = new { ExpectedRequest = typeof(TimeSpan), Specimen = (object)TimeSpan.FromDays(1) };
            var subRequests = new[] { param1, param2 };

            var container = new DelegatingSpecimenContainer();
            container.OnResolve = r => (from x in subRequests
                                        where x.ExpectedRequest.Equals(r)
                                        select x.Specimen).DefaultIfEmpty(new NoSpecimen(r)).SingleOrDefault();

            Func<decimal, TimeSpan, object> f = (d, ts) => param1.Specimen.Equals(d) && param2.Specimen.Equals(ts) ? expectedSpecimen : new NoSpecimen();
            var sut = new SpecimenCreator<decimal, TimeSpan, object>(f);
            // Exercise system
            var dummyRequest = new object();
            var result = sut.Create(dummyRequest, container);
            // Verify outcome
            Assert.Equal(expectedSpecimen, result);
            // Teardown
        }
    }
}
