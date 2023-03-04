using L01_2020RJ601.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2020RJ601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class comentariosController : ControllerBase
    {
        private readonly blogContext _blogContext;
        public comentariosController(blogContext blogContext)
        {
            _blogContext = blogContext;
        }
        //CREATE
        [HttpPost]
        [Route("Añadir")]

        public IActionResult Post([FromBody] comentarios comentarios)
        {
            try
            {
                _blogContext.comentarios.Add(comentarios);
                _blogContext.SaveChanges();
                return Ok(comentarios);

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
                List<comentarios> listComentarios = (from e in _blogContext.comentarios select e).ToList();
                if (listComentarios.Count() == 0)
                {
                    return NotFound();
                }
                return Ok(listComentarios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // UPDATE
        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult actualizarComentario(int id, [FromBody] comentarios actualizarComentario)
        {
            try
            {
                comentarios? comentariosA = (from e in _blogContext.comentarios where e.cometarioId == id select e).FirstOrDefault();

                if (comentariosA == null) return NotFound();

                comentariosA.cometarioId = actualizarComentario.cometarioId;
                comentariosA.publicacionId = actualizarComentario.publicacionId;
                comentariosA.comentario = actualizarComentario.comentario;
                comentariosA.usuarioId = actualizarComentario.usuarioId;

                _blogContext.Entry(comentariosA).State = EntityState.Modified;
                _blogContext.SaveChanges();
                return Ok(actualizarComentario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //DELETE
        [HttpDelete]
        [Route("borrar/{id}")]
        public IActionResult borrarComentario(int id)
        {
            try
            {
                comentarios? comentarioD = (from e in _blogContext.comentarios where e.cometarioId == id select e).FirstOrDefault();

                if (comentarioD == null) return NotFound();
                
                _blogContext.comentarios.Attach(comentarioD);
                _blogContext.comentarios.Remove(comentarioD);
                _blogContext.SaveChanges();
                return Ok(comentarioD);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //Obtener por publicacion en especifico       
        [HttpGet]
        [Route("getcomentarios/{id}")]

        public IActionResult GetComentId(int id)
        {
            try
            {
                List<comentarios> listComentarioEspecifica = (from e in _blogContext.comentarios where e.publicacionId == id select e).ToList();
                if (listComentarioEspecifica.Count() == 0)
                {
                    return NotFound();
                }
                return Ok(listComentarioEspecifica);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }



        }
    }
}
