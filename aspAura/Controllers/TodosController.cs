using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
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
            return _db.Todoes.OrderByDescending(a => a.id );
        }

        // GET /api/<controller>/5
        public Todo Get(int id)
        {
            var getTodo = (from todo in _db.Todoes where todo.id == id select todo).FirstOrDefault();
            if(getTodo == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return getTodo;
        }

        // POST /api/<controller>
        public HttpResponseMessage<Todo> Post(Todo todo)
        {
            _db.Todoes.AddObject(todo);
            _db.SaveChanges();

            return new HttpResponseMessage<Todo>(HttpStatusCode.Created);
        }

        // PUT /api/<controller>/5
        public HttpResponseMessage Put(Todo todo)
        {
            _db.Todoes.Attach(todo);
             _db.ObjectStateManager.ChangeObjectState(todo, EntityState.Modified);
            _db.SaveChanges();

            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }

        // DELETE /api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            var stubDelete = new Todo {id = id};
            _db.Todoes.Attach(stubDelete);
            _db.Todoes.DeleteObject(stubDelete);
            _db.SaveChanges();
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}