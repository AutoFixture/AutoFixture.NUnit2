﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Ploeh.AutoFixture.Kernel;

namespace Ploeh.AutoFixtureUnitTest.Kernel
{
    public class SpecimenCreatorWithTripleParameterFuncTest
    {
        [Fact]
        public void SutIsSpecimenBuilder()
        {
            // Fixture setup
            Func<string, int, decimal, object> dummyFunc = (x, y, z) => new object();
            // Exercise system
            var sut = new SpecimenCreator<string, int, decimal, object>(dummyFunc);
            // Verify outcome
            Assert.IsAssignableFrom<ISpecimenBuilder>(sut);
            // Teardown
        }

        [Fact]
        public void InitializeWithNullFuncThrows()
        {
            // Fixture setup
            // Exercise system and verify outcome
            Assert.Throws<ArgumentNullException>(() => new SpecimenCreator<bool, int, string, object>((Func<bool, int, string, object>)null));
            // Teardown
        }

        [Fact]
        public void CreateWithNullContainerThrows()
        {
            // Fixture setup
            var sut = new SpecimenCreator<object, object, object, object>((x, y, z) => x);
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
            var param3 = new { ExpectedRequest = typeof(string), Specimen = (object)"Anonymous value - with Foo!" };
            var subRequests = new[] { param1, param2, param3 };

            var container = new DelegatingSpecimenContainer();
            container.OnResolve = r => (from x in subRequests
                                        where x.ExpectedRequest.Equals(r)
                                        select x.Specimen).DefaultIfEmpty(new NoSpecimen(r)).SingleOrDefault();

            Func<decimal, TimeSpan, string, object> f = (d, ts, s) => 
                param1.Specimen.Equals(d) && param2.Specimen.Equals(ts) && param3.Specimen.Equals(s) ? expectedSpecimen : new NoSpecimen();
            var sut = new SpecimenCreator<decimal, TimeSpan, string, object>(f);
            // Exercise system
            var dummyRequest = new object();
            var result = sut.Create(dummyRequest, container);
            // Verify outcome
            Assert.Equal(expectedSpecimen, result);
            // Teardown
        }
    }
}
