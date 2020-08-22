using System;
using Xunit;
using Set;
using System.Linq;
using System.Reflection;

namespace SortedSetTests
{
    class TestTypes
    {
        public class Person
        {
            public int Age { get; set; }
        }

        public class Car
        {
            public string Name { get; set; }
        }
    }

    public class SortedSetUnitTests
    {
        private SortedSet<int> _set;

        public SortedSetUnitTests()
        {
            _set = new SortedSet<int>();
        }

        [Fact]
        public void EmptySetHasZeroCount()
        {
            Assert.Empty(_set);
        }

        [Theory]
        [InlineData(typeof(TestTypes.Person))]
        [InlineData(typeof(TestTypes.Car))]
        public void SetThrowsExceptionWhenTypeDoesNotImplementComparer(Type dataType)
        {
            const int insertCount = 2;
            const string methodName = "Add";
            
            var genericBase = typeof(SortedSet<>);
            var combinedType = genericBase.MakeGenericType(dataType);
            var genericInstance = Activator.CreateInstance(combinedType);
            var addMethod = genericInstance?.GetType().GetMethod(methodName);

            var exception = Assert.Throws<TargetInvocationException>(() =>
            {
                for (int i = 0; i < insertCount; i++)
                {
                    var objInstance = Activator.CreateInstance(dataType);
                    addMethod?.Invoke(genericInstance, new object[] {objInstance});
                }
            });

            Assert.NotNull(exception);
            Assert.IsType<ArgumentException>(exception.InnerException);
        }
    }
}