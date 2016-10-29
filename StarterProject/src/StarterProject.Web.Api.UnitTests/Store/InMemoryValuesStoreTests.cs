namespace StarterProject.Web.Api.Store
{
    using System;
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
            var readValue = valuesStore.Read(value.Id);
            Assert.AreEqual(value.Id, readValue.Id);
            Assert.AreEqual(value, readValue);
            Assert.AreSame(value, readValue);
        }

        [TestMethod]
        public void InMemoryValuesStore_Create_KeyAlreadyExists()
        {
            // Arrange
            var value = new Value() { Id = "1", Data = "data" };
            var valuesStore = new InMemoryValuesStore();
            valuesStore.Create(value);

            // Act
            Action action = () => valuesStore.Create(value);

            // Assert
            ExceptionAssert.ThrowsApiException(action, ApiExceptionError.ValueAlreadyExists);
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

            // Act
            Action action = () => this.CreateMultiple(valuesStore, valuesToCreate);

            // Assert
            ExceptionAssert.ThrowsApiException(action, ApiExceptionError.ValuesStoreFull);
        }

        [TestMethod]
        public void InMemoryValuesStore_Delete_Success()
        {
            // Arrange
            var value = new Value() { Id = "1", Data = "data" };
            var valuesStore = new InMemoryValuesStore();
            valuesStore.Create(value);

            // Act
            valuesStore.Delete(value.Id);

            // Assert
            Action action = () => valuesStore.Read(value.Id);
            ExceptionAssert.ThrowsApiException(action, ApiExceptionError.ValueDoesntExist);
        }

        [TestMethod]
        public void InMemoryValuesStore_Delete_ValueDoesntExist()
        {
            // Arrange
            var valuesStore = new InMemoryValuesStore();
            var valueId = "nonExistentId";

            // Act
            Action action = () => valuesStore.Delete(valueId);

            // Assert
            ExceptionAssert.ThrowsApiException(action, ApiExceptionError.ValueDoesntExist);
        }

        [TestMethod]
        public void InMemoryValuesStore_Read_Success()
        {
            // Arrange
            var value = new Value() { Id = "1", Data = "data" };
            var valuesStore = new InMemoryValuesStore();
            valuesStore.Create(value);

            // Act
            var readValue = valuesStore.Read(value.Id);

            // Assert
            Assert.AreEqual(value.Id, readValue.Id);
            Assert.AreEqual(value, readValue);
            Assert.AreSame(value, readValue);
        }

        [TestMethod]
        public void InMemoryValuesStore_Read_ValueDoesntExist()
        {
            // Arrange
            var valuesStore = new InMemoryValuesStore();
            var valueId = "nonExistentId";

            // Act
            Action action = () => valuesStore.Read(valueId);

            // Assert
            ExceptionAssert.ThrowsApiException(action, ApiExceptionError.ValueDoesntExist);
        }

        [TestMethod]
        public void InMemoryValuesStore_Update_ValueDoesntExist()
        {
            // Arrange
            var valuesStore = new InMemoryValuesStore();
            var value = new Value() { Id = "nonExistentId", Data = "data" };

            // Act
            Action action = () => valuesStore.Update(value);

            // Assert
            ExceptionAssert.ThrowsApiException(action, ApiExceptionError.ValueDoesntExist);
        }

        [TestMethod]
        public void InMemoryValuesStore_Update_Success()
        {
            // Arrange
            var value = new Value() { Id = "1", Data = "data" };
            var valuesStore = new InMemoryValuesStore();
            valuesStore.Create(value);

            var updatedValue = "updatedData";

            // Act
            value.Data = updatedValue;
            valuesStore.Update(value);

            // Assert
            var readValue = valuesStore.Read(value.Id);

            Assert.AreEqual(value.Id, readValue.Id);
            Assert.AreEqual(updatedValue, readValue.Data);
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
