using Blog.Core.Models;
using Blog.Core.Utility;
using NUnit.Framework;
using System.Linq;
using System.Reflection;

namespace Blog.Core.Tests
{
    [TestFixture]
    public class InfrastructureTests
    {
        private const string InterfacePrefix = "I";
        private const string MapSuffix = "Map";

        [TestCase("Repository")]
        public void EachComponentInterfaceShouldHaveImplementation(string componentName)
        {
            // given
            var assembly = Assembly.GetAssembly(typeof(CoreModule));
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

        [TestCase("Repository")]
        public void EachComponentShouldImplementSpecificInterface(string componentName)
        {
            // given
            var assembly = Assembly.GetAssembly(typeof(CoreModule));
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

        [Test]
        public void EachKeyedEntityShouldBeMapped()
        {
            // given
            var assembly = Assembly.GetAssembly(typeof(CoreModule));
            var entities = assembly
                .GetNonAbstractClasses()
                .Where(type => type.GetInterfaces().Contains(typeof(IKeyedEntity)))
                .ToList();

            // when
            var isEachKeyedEntityMapped = entities
                .All(entity => assembly
                    .GetNonAbstractClasses()
                    .SingleOrDefault(type => type.Name == $"{entity.Name}{MapSuffix}") != null);

            // then
            Assert.True(isEachKeyedEntityMapped);
        }
    }
}
