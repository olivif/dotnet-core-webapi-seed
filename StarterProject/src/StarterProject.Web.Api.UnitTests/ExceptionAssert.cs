namespace StarterProject.Web.Api
{
    using System;
    using StarterProject.Web.Api.Exceptions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public static class ExceptionAssert
    {
        public static void ThrowsApiException(Action action, ApiExceptionError error)
        {
            var e = Assert.ThrowsException<ApiException>(action);

            Assert.AreEqual(error, e.Error);
        }
    }
}
