using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using aspAura.Models;

namespace aspAura.Controllers.Controllers
{
    public class TodosController : ApiController
    {
        private aspAuraEntities _db;
        public TodosController()
        {
            _db = new aspAuraEntities();
        }

        // GET /api/<controller>
        public IEnumerable<Todo> Get()
        {
            return _db.Todoes;
        }

        // GET /api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST /api/<controller>
        public void Post(string value)
        {
        }

        // PUT /api/<controller>/5
        public void Put(int id, string value)
        {
        }

        // DELETE /api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}