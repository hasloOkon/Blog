using Blog.Core.Utility;
using NUnit.Framework;
using System.Linq;
using System.Reflection;

namespace Blog.WebApp.Tests
{
    [TestFixture]
    public class InfrastructureTests
    {
        private const string InterfacePrefix = "I";

        [TestCase("Provider")]
        [TestCase("Factory")]
        public void EachComponentInterfaceShouldHaveImplementation(string componentName)
        {
            // given
            var assembly = Assembly.GetAssembly(typeof(WebModule));
            var componentInterfaces = assembly
                .GetTypes()
                .Where(type => type.IsInterface && type.Name.EndsWith(componentName) && !type.IsGenericType)
                .ToList();

            // when
            var isEachComponentInterfaceImplemeted = componentInterfaces
                .All(componentInterface => assembly
                    .GetNonAbstractClasses()
                    .SingleOrDefault(componentInterface.IsAssignableFrom) != null);

            // then
            Assert.True(isEachComponentInterfaceImplemeted);
        }

        [TestCase("Provider")]
        [TestCase("Factory")]
        public void EachComponentShouldImplementSpecificInterface(string componentName)
        {
            // given
            var assembly = Assembly.GetAssembly(typeof(WebModule));
            var components = assembly
                .GetNonAbstractClasses()
                .Where(type => type.Name.EndsWith(componentName))
                .ToList();

            // when
            var eachComponentImplementsSpecificInterface = components
                .All(component => component.GetInterface($"{InterfacePrefix}{component.Name}") != null);

            // then
            Assert.True(eachComponentImplementsSpecificInterface);
        }
    }
}