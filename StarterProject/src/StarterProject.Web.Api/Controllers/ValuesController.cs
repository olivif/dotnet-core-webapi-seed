namespace StarterProject.Web.Api.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Store;

    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private IValuesStore valuesStore;

        public ValuesController(IValuesStore valuesStore)
        {
            this.valuesStore = valuesStore;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<Value> Get()
        {
            return this.valuesStore.Read();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Value Get(string id)
        {
            return this.valuesStore.Read(id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]Value value)
        {
            this.valuesStore.Create(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody]Value value)
        {
            this.valuesStore.Update(value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            this.valuesStore.Delete(id);
        }
    }
}
