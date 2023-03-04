using L01_2020RJ601.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2020RJ601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class publicacionesController : ControllerBase
    {
        private readonly blogContext _blogContext;
        public publicacionesController(blogContext blogContext)
        {
            _blogContext = blogContext;
        }
        //CREATE
        [HttpPost]
        [Route("Añadir")]

        public IActionResult Post([FromBody] publicaciones publicaciones)
        {
            try
            {
                _blogContext.publicaciones.Add(publicaciones);
                _blogContext.SaveChanges();
                return Ok(publicaciones);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //READ
        [HttpGet]
        [Route("getAll")]
        public IActionResult Get()
        {
            try
            {
                List<publicaciones> listPublicacion = (from e in _blogContext.publicaciones select e).ToList();
                if (listPublicacion.Count() == 0)
                {
                    return NotFound();
                }
                return Ok(listPublicacion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // UPDATE
        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult actualizarPublicacion(int id, [FromBody] publicaciones actualizarPubliacion)
        {
            try
            {
                publicaciones? publicacionA = (from e in _blogContext.publicaciones where e.publicacionId == id select e).FirstOrDefault();

                if (publicacionA == null) return NotFound();

                publicacionA.publicacionId = actualizarPubliacion.publicacionId;
                publicacionA.titulo = actualizarPubliacion.titulo;
                publicacionA.descripcion = actualizarPubliacion.descripcion;
                publicacionA.usuarioId = actualizarPubliacion.usuarioId;

                _blogContext.Entry(publicacionA).State = EntityState.Modified;
                _blogContext.SaveChanges();
                return Ok(actualizarPubliacion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //DELETE
        [HttpDelete]
        [Route("borrar/{id}")]
        public IActionResult borrarPublicacion(int id)
        {
            try
            {
                publicaciones? publicacionD = (from e in _blogContext.publicaciones where e.publicacionId == id select e).FirstOrDefault();

                if (publicacionD == null) return NotFound();
                
                _blogContext.publicaciones.Attach(publicacionD);
                _blogContext.publicaciones.Remove(publicacionD);
                _blogContext.SaveChanges();
                return Ok(publicacionD);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //Obtener por usuario en especifico       
        [HttpGet]
        [Route("getuserid/{id}")]

        public IActionResult GetPublicId(int id)
        {
            try
            {
                List<publicaciones> listUsuariosEspecifica = (from e in _blogContext.publicaciones where e.publicacionId == id select e).ToList();
                if (listUsuariosEspecifica.Count() == 0)
                {
                    return NotFound();
                }
                return Ok(listUsuariosEspecifica);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            

        }
    }
}
