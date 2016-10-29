namespace StarterProject.Web.Api.Exceptions
{
    using System;

    public class ApiException : Exception
    {
        public ApiExceptionError Error { get; }

        public ApiException(ApiExceptionError error) 
            : base()
        {
            this.Error = error;
        }
    }
}
