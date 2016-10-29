namespace StarterProject.Web.Api.Store
{
    using StarterProject.Web.Api.Models;

    /// <summary>
    /// Store for values
    /// </summary>
    public interface IValuesStore
    {
        /// <summary>
        /// Creates a value in the store
        /// </summary>
        /// <param name="value">The value to be added</param>
        void Create(Value value);

        /// <summary>
        /// Reads a value from the store
        /// </summary>
        /// <param name="id">The id of the value to be read</param>
        void Read(string id);

        /// <summary>
        /// Updates a value in the store
        /// </summary>
        /// <param name="value">The value to be updated</param>
        void Update(Value value);

        /// <summary>
        /// Deletes a value from the store
        /// </summary>
        /// <param name="id">The id of the value to be deleted</param>
        void Delete(string id);
    }
}
