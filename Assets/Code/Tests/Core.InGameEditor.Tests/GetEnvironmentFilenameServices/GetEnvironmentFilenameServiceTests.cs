using Noneb.Core.Game.GameEnvironments.Data;
using Noneb.Core.InGameEditor.GetEnvironmentFilenameServices;
using NUnit.Framework;

namespace Core.InGameEditor.Tests.GetEnvironmentFilenameServices
{
    [TestFixture]
    public class GetEnvironmentFilenameServiceTests
    {
        private GetEnvironmentFilenameService _service;

        [SetUp]
        public void SetUp()
        {
            _service = new GetEnvironmentFilenameService();
        }

        [Test]
        public void WhenGivenEmptyEnvironmentName_ThrowArgumentException()
        {
            void CallWithEmptyEnvironmentName()
            {
                _service.GetEnvironmentAsScriptableFilename(string.Empty, typeof(GameEnvironment));
            }
            
            Assert.That(CallWithEmptyEnvironmentName, Throws.ArgumentException);
        }
        
        [Test]
        public void WhenGivenNullEnvironmentName_ThrowArgumentException()
        {
            void CallWithEmptyEnvironmentName()
            {
                _service.GetEnvironmentAsScriptableFilename(null, typeof(GameEnvironment));
            }
            
            Assert.That(CallWithEmptyEnvironmentName, Throws.ArgumentException);
        }

        [Test]
        public void WhenGivenValidArgument_ReturnCorrectName()
        {
            const string environmentName = "noob";
            var expectedValue = $"noob{nameof(GameEnvironment)}.asset";

            var returnedValue = _service.GetEnvironmentAsScriptableFilename(environmentName, typeof(GameEnvironment));
            
            Assert.That(returnedValue, Is.EqualTo(expectedValue));
        }
    }
}