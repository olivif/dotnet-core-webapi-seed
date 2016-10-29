namespace StarterProject.Web.Api.Store
{
    using Exceptions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models;

    [TestClass]
    public class InMemoryValuesStoreTests
    {
        [TestMethod]
        public void InMemoryValuesStore_Constructor()
        {
            // Act
            new InMemoryValuesStore();
        }

        [TestMethod]
        public void InMemoryValuesStore_Create_Success()
        {
            // Arrange
            var value = new Value() { Id = "1", Data = "data" };
            var valuesStore = new InMemoryValuesStore();

            // Act
            valuesStore.Create(value);

            // Assert
            Assert.AreEqual(value, valuesStore.Read(value.Id));
            Assert.AreSame(value, valuesStore.Read(value.Id));
        }

        [TestMethod]
        public void InMemoryValuesStore_Create_KeyAlreadyExists()
        {
            // Arrange
            var value = new Value() { Id = "1", Data = "data" };
            var valuesStore = new InMemoryValuesStore();
            valuesStore.Create(value);

            // Act & Assert
            var e = Assert.ThrowsException<ApiException>(() => valuesStore.Create(value));
            Assert.AreEqual(ApiExceptionError.ValueAlreadyExists, e.Error);
        }

        [TestMethod]
        public void InMemoryValuesStore_Create_LessThanMaxCreated()
        {
            // Arrange
            var valuesStore = new InMemoryValuesStore();
            var valuesToCreate = 9;

            // Act
            this.CreateMultiple(valuesStore, valuesToCreate);
        }

        [TestMethod]
        public void InMemoryValuesStore_Create_MaxCreated()
        {
            // Arrange
            var valuesStore = new InMemoryValuesStore();
            var valuesToCreate = 10;

            // Act
            this.CreateMultiple(valuesStore, valuesToCreate);
        }

        [TestMethod]
        public void InMemoryValuesStore_Create_MoreThanMaxCreated()
        {
            // Arrange
            var valuesStore = new InMemoryValuesStore();
            var valuesToCreate = 11;

            // Act & Assert
            var e = Assert.ThrowsException<ApiException>(() => this.CreateMultiple(valuesStore, valuesToCreate));
            Assert.AreEqual(ApiExceptionError.ValuesStoreFull, e.Error);
        }

        private void CreateMultiple(IValuesStore valuesStore, int numberOfValuesToCreate)
        {
            for (int valueId = 0; valueId < numberOfValuesToCreate; valueId++)
            {
                var value = new Value() { Id = valueId.ToString(), Data = "data" };
                valuesStore.Create(value);
            }
        }
    }
}
