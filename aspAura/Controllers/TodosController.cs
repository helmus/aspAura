﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using aspAura.Helpers;
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
            var getTodo = _db.Todoes.Find(id);
            if(getTodo == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return getTodo;
        }

        // POST /api/<controller>
        public HttpResponseMessage<Todo> Post(Todo todo)
        {
            _db.Todoes.Add(todo);
            _db.SaveChanges();

            return new HttpResponseMessage<Todo>(HttpStatusCode.Created);
        }

        // PUT /api/<controller>/5
        public HttpResponseMessage Put(Todo todo)
        {
            _db.Entry(todo).State = EntityState.Modified;
            _db.SaveChanges();
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }

        // DELETE /api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            _db.Entry(new Todo {id = id}).State = EntityState.Deleted;
            _db.SaveChanges();

            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }

        [AcceptVerbs("PATCH")]
        public HttpResponseMessage Patch(PatchModel<Todo> todoPatch)
        {
            todoPatch.UpdateModel(_db.Todoes.Find( todoPatch.GetKeys(_db)));
            _db.SaveChanges();
            return new HttpResponseMessage<Todo>(HttpStatusCode.NoContent);
        }
    }
}