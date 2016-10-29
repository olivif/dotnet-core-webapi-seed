using System;
using System.Collections.Generic;
using StarterProject.Web.Api.Exceptions;
using StarterProject.Web.Api.Models;

namespace StarterProject.Web.Api.Store
{
    /// <summary>
    /// In memory values store
    /// </summary>
    public class InMemoryValuesStore : IValuesStore
    {
        private Dictionary<string, Value> valuesStore;

        public InMemoryValuesStore()
        {
            valuesStore = new Dictionary<string, Value>();
        }

        public void Create(Value value)
        {
            if (valuesStore.ContainsKey(value.Id))
            {
                throw new ApiException(ApiExceptionError.ValueAlreadyExists);
            }

            valuesStore.Add(value.Id, value);
        }

        public void Delete(string id)
        {
            if (!valuesStore.ContainsKey(id))
            {
                throw new ApiException(ApiExceptionError.ValueDoesntExist);
            }

            valuesStore.Remove(id);
        }

        public Value Read(string id)
        {
            Value value;
            if (!valuesStore.TryGetValue(id, out value))
            {
                throw new ApiException(ApiExceptionError.ValueDoesntExist);
            }

            return value;
        }

        public void Update(Value value)
        {
            if (!valuesStore.ContainsKey(value.Id))
            {
                throw new ApiException(ApiExceptionError.ValueDoesntExist);
            }

            valuesStore[value.Id] = value;
        }
    }
}
