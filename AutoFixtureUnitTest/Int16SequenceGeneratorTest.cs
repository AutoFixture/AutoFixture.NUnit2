﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixtureUnitTest.Kernel;
using Ploeh.AutoFixture.Kernel;
using Xunit;

namespace Ploeh.AutoFixtureUnitTest
{
    public class Int16SequenceGeneratorTest
    {
        public Int16SequenceGeneratorTest()
        {
        }

        [Fact]
        public void CreateWillReturnOneOnFirstCall()
        {
            new LoopTest<Int16SequenceGenerator, short>(sut => sut.CreateAnonymous()).Execute(1);
        }

        [Fact]
        public void CreateWillReturnTwoOnSecondCall()
        {
            new LoopTest<Int16SequenceGenerator, short>(sut => sut.CreateAnonymous()).Execute(2);
        }

        [Fact]
        public void CreateWillReturnTenOnTenthCall()
        {
            new LoopTest<Int16SequenceGenerator, short>(sut => sut.CreateAnonymous()).Execute(10);
        }

        [Fact]
        public void SutIsSpecimenBuilder()
        {
            // Fixture setup
            // Exercise system
            var sut = new Int16SequenceGenerator();
            // Verify outcome
            Assert.IsAssignableFrom<ISpecimenBuilder>(sut);
            // Teardown
        }

        [Fact]
        public void CreateWithNullRequestWillReturnCorrectResult()
        {
            // Fixture setup
            var sut = new Int16SequenceGenerator();
            // Exercise system
            var dummyContainer = new DelegatingSpecimenContainer();
            var result = sut.Create(null, dummyContainer);
            // Verify outcome
            Assert.Equal(new NoSpecimen(), result);
            // Teardown
        }

        [Fact]
        public void CreateWithNullContainerDoesNotThrow()
        {
            // Fixture setup
            var sut = new Int16SequenceGenerator();
            // Exercise system
            var dummyRequest = new object();
            sut.Create(dummyRequest, null);
            // Verify outcome (no exception indicates success)
            // Teardown
        }

        [Fact]
        public void CreateWithNonInt16RequestWillReturnCorrectResult()
        {
            // Fixture setup
            var nonInt16Request = new object();
            var sut = new Int16SequenceGenerator();
            // Exercise system
            var dummyContainer = new DelegatingSpecimenContainer();
            var result = sut.Create(nonInt16Request, dummyContainer);
            // Verify outcome
            var expectedResult = new NoSpecimen(nonInt16Request);
            Assert.Equal(expectedResult, result);
            // Teardown
        }

        [Fact]
        public void CreateWithInt16RequestWillReturnCorrectResult()
        {
            // Fixture setup
            var int16Request = typeof(short);
            var sut = new Int16SequenceGenerator();
            // Exercise system
            var dummyContainer = new DelegatingSpecimenContainer();
            var result = sut.Create(int16Request, dummyContainer);
            // Verify outcome
            Assert.Equal((short)1, result);
            // Teardown
        }

        [Fact]
        public void CreateWithInt16RequestWillReturnCorrectResultOnSecondCall()
        {
            // Fixture setup
            var int16Request = typeof(short);
            var dummyContainer = new DelegatingSpecimenContainer();
            var loopTest = new LoopTest<Int16SequenceGenerator, short>(sut => (short)sut.Create(int16Request, dummyContainer));
            // Exercise system and verify outcome
            loopTest.Execute(2);
            // Teardown
        }

        [Fact]
        public void CreateWithInt16RequestWillReturnCorrectResultOnTenthCall()
        {
            // Fixture setup
            var int16Request = typeof(short);
            var dummyContainer = new DelegatingSpecimenContainer();
            var loopTest = new LoopTest<Int16SequenceGenerator, short>(sut => (short)sut.Create(int16Request, dummyContainer));
            // Exercise system and verify outcome
            loopTest.Execute(10);
            // Teardown
        }
    }
}
