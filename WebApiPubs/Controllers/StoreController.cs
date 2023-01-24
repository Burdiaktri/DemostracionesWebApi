using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using WebApiPubs.Models;

namespace WebApiPubs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly pubsContext context;

        public StoreController(pubsContext context)
        {
            this.context = context;

        }

        [HttpGet]
        public ActionResult<IEnumerable<Store>> Get()
        {
            return context.Stores.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Store> GetById(string id)
        {
            Store store = (from s in context.Stores
                           where s.StorId == id
                           select s).SingleOrDefault();

            return store;
        }


        [HttpPost]
        public ActionResult Post(Store store)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Stores.Add(store);
            context.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Store store)
        {
            if (id != store.StorId)
            {
                return BadRequest();
            }
            context.Entry(store).State = EntityState.Modified;
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Store> Delete(string id)
        {
            var store = (from s in context.Stores
                         where s.StorId == id
                         select s).SingleOrDefault();

            if(store == null)
            {
                return NotFound();
            }

            context.Stores.Remove(store);
            context.SaveChanges();
            return store;
        }

        [HttpGet("name/{name}")]
        public ActionResult<IEnumerable<Store>> GetByName(string name)
        {
            List<Store> stores = (from s in context.Stores
                           where s.StorName == name
                           select s).ToList();

            return stores;
        }

        [HttpGet("zip/{zip}")]
        public ActionResult<IEnumerable<Store>> GetByZip(string zip)
        {
            List<Store> stores = (from s in context.Stores
                                  where s.Zip == zip
                                  select s).ToList();

            return stores;
        }

        [HttpGet("city/{city}")]
        public ActionResult<IEnumerable<Store>> GetByCity(string city)
        {
            List<Store> stores = (from s in context.Stores
                           where s.City == city
                           select s).ToList();

            return stores;
        }

        [HttpGet("state/{state}")]
        public ActionResult<IEnumerable<Store>> GetByState(string state)
        {
            List<Store> stores = (from s in context.Stores
                           where s.State == state
                           select s).ToList();

            return stores;
        }

    }
}
