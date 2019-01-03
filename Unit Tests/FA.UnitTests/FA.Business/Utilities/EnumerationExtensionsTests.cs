using FA.Business.Utilities;
using NUnit.Framework;
using scm = System.ComponentModel;

namespace FA.UnitTests.FA.Business.Utilities
{
    public enum SampleEnumeration
    {
        [scm.Description("One")]
        One = 1,

        [scm.Description("Two")]
        Two = 2,

        [scm.Description("Three")]
        Three = 3
    }

    [TestFixture]
    public class EnumerationExtensionsTests
    {
        [Test]
        public void GetDescription_IfTypeIsEnum_ReturnDescription()
        {
            var enumeration = SampleEnumeration.One;

            var result = enumeration.GetDescription();

            Assert.That(result, Is.EqualTo("One"));
        }

        [Test]
        public void GetDescription_IfTypeIsNotEnum_ReturnNull()
        {
            var str = string.Empty;

            var result = str.GetDescription();

            Assert.That(result, Is.Null);
        }
    }
}
