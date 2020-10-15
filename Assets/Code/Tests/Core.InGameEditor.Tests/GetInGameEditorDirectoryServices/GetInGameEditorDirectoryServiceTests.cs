using Noneb.Core.InGameEditor.GetInGameEditorDirectoryServices;
using NUnit.Framework;

namespace Core.InGameEditor.Tests.GetInGameEditorDirectoryServices
{
    [TestFixture]
    public class GetInGameEditorDirectoryServiceTests
    {
        [SetUp]
        public void SetUp()
        {
            _service = new GetInGameEditorDirectoryService();
        }

        private GetInGameEditorDirectoryService _service;

        [Test]
        public void WhenCalledWithValidArgument_ReturnsCorrectValue()
        {
            const string environmentName = "noob";
            var expectedValue = $"Assets/Data/Environments/{environmentName}";

            var returnedValue = _service.GetRelativeDirectoryToSpecificEnvironment(environmentName);

            Assert.That(returnedValue, Is.EqualTo(expectedValue));
        }

        [Test]
        public void WhenEnvironmentNameIsEmpty_ThrowArgumentException()
        {
            void CallWithEmptyEnvironmentName()
            {
                _service.GetRelativeDirectoryToSpecificEnvironment(string.Empty);
            }

            Assert.That(CallWithEmptyEnvironmentName, Throws.ArgumentException);
        }

        [Test]
        public void WhenEnvironmentNameIsNull_ThrowArgumentException()
        {
            void CallWithNullEnvironmentName()
            {
                _service.GetRelativeDirectoryToSpecificEnvironment(null);
            }

            Assert.That(CallWithNullEnvironmentName, Throws.ArgumentException);
        }
    }
}