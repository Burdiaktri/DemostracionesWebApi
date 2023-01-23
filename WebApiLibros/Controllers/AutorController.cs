using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WebApiLibros.Data;
using WebApiLibros.Models;

namespace WebApiLibros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        //inyeccion de dependencia --------- inicia
        //propiedad
        private readonly DBLibrosContext context;
        //constructor
        public AutorController(DBLibrosContext context)
        {
            this.context = context;
        }

        //inyeccion de dependencia --------- fin

        [HttpGet]
        public ActionResult<IEnumerable<Autor>> Get()
        {
            return context.Autores.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Autor> GetById(int id) 
        {
            Autor autor = (from a in context.Autores
                           where a.IdAutor == id
                           select a).SingleOrDefault();
            return autor;

        }

        [HttpPost]
        public ActionResult Post(Autor autor) 
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            context.Autores.Add(autor);
            context.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Autor autor)
        {
            if(id!=autor.IdAutor)
            {
                return BadRequest();
            }

            context.Entry(autor).State = EntityState.Modified;
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Autor> Delete(int id)
        {
            var autor = (from a in context.Autores
                         where a.IdAutor == id
                         select a).SingleOrDefault();

            if (autor == null) {
                return NotFound(); }

            context.Autores.Remove(autor);
            context.SaveChanges();
            return autor;
        }

        [HttpGet("edad/{edad}")]
        public ActionResult<IEnumerable<Autor>> Get(int edad)
        {
            List<Autor> autores = (from a in context.Autores
                                     where a.Edad == edad
                                     select a).ToList();

            return autores;

        }
    }
    }
