using FaceDetection;
using NUnit.Framework;

namespace FA.UnitTests.FA.UI.Console
{
    [TestFixture]
    public class ValidatorsTests
    {
        private Validators _validators;

        [SetUp]
        public void SetUp()
        {
            _validators = new Validators();
        }

        [Test]
        public void PersonGroup_PersonGroupIdIsValid_IsValidMustBeTrue()
        {
            _validators.PersonGroup("sample-group");

            Assert.That(_validators.IsValid, Is.True);
        }

        [Test]
        public void PersonGroup_PersonGroupIdIsInvalid_ThrowInvalidOperationException()
        {
            Assert.That(() => _validators.PersonGroup("Sample-group"), Throws.InvalidOperationException);
        }

        [Test]
        public void PersonName_PersonNameIsValid_IsValidMustBeTrue()
        {
            _validators.PersonName("a");

            Assert.That(_validators.IsValid, Is.True);
        }

        [Test]
        public void PersonName_PersonNameIsInvalid_ThrowInvalidOperationException()
        {
            Assert.That(() => _validators.PersonName(string.Empty), Throws.InvalidOperationException);
        }

        [Test]
        public void PersonId_PersonIdIsValid_IsValidMustBeTrue()
        {
            _validators.Id("defe67aa-b421-40d7-8200-8c7e4ffe5484");

            Assert.That(_validators.IsValid, Is.True);
        }

        [Test]
        public void PersonId_PersonIdIsInvalid_ThrowInvalidOperationException()
        {
            Assert.That(() => _validators.Id("a"), Throws.InvalidOperationException);
        }
    }
}
