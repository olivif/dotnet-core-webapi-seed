namespace StarterProject.Web.Api.UnitTests
{
    using Controllers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Store;

    [TestClass]
    public class ValuesControllerTests
    {
        [TestMethod]
        public void ValuesController_Constructor()
        {
            // Act
            new ValuesController(new InMemoryValuesStore());
        }
    }
}
