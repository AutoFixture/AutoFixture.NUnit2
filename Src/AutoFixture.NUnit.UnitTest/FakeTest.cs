using NUnit.Framework;

namespace Ploeh.AutoFixture.NUnit.UnitTest
{
    public class FakeTest
    {
        [AutoTestCase]
        public void DoSomething(int number)
        {
            Assert.IsTrue(number > 0);
        }
    }
}