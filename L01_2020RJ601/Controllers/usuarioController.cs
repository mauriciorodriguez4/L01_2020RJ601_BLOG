using L01_2020RJ601.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2020RJ601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usuarioController : ControllerBase
    {
        private readonly blogContext _blogContext;
        public usuarioController(blogContext blogContext)
        {
            _blogContext = blogContext;
        }
        //READ
        [HttpGet]
        [Route("getAll")]
        public IActionResult Get()
        {
            try
            {
                List<usuarios> listUsuarios = (from e in _blogContext.usuarios select e).ToList();
                if (listUsuarios.Count() == 0)
                {
                    return NotFound();
                }
                return Ok(listUsuarios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //CREATE
        [HttpPost]
        [Route("Añadir")]

        public IActionResult Post([FromBody] usuarios usuario)
        {
            try
            {
                _blogContext.usuarios.Add(usuario);
                _blogContext.SaveChanges();
                return Ok(usuario);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // UPDATE
        [HttpPut]
        [Route("actualizar/{id}")]

        public IActionResult actualizarUsuario(int id, [FromBody] usuarios usuarioActualizado)
        {
            try
            {
                usuarios? usuarioA = (from e in _blogContext.usuarios where e.usuarioId == id select e).FirstOrDefault();

                if (usuarioA == null) return NotFound();

                usuarioA.nombreUsuario = usuarioActualizado.nombreUsuario;
                usuarioA.clave = usuarioActualizado.clave;
                usuarioA.nombre = usuarioActualizado.nombre;
                usuarioA.apellido = usuarioActualizado.apellido;

                _blogContext.Entry(usuarioA).State = EntityState.Modified;
                _blogContext.SaveChanges();
                return Ok(usuarioActualizado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //DELETE
        [HttpDelete]
        [Route("borrar/{id}")]
        public IActionResult borrarUsuario(int id)
        {
            try
            {
                usuarios? usuarioD = (from e in _blogContext.usuarios where e.usuarioId == id select e).FirstOrDefault();

                if (usuarioD == null) return NotFound();
                
                _blogContext.usuarios.Attach(usuarioD);
                _blogContext.usuarios.Remove(usuarioD);                
                _blogContext.SaveChanges();
                return Ok(usuarioD);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //Metodo filtrado de nombre y apellido
        [HttpGet]
        [Route("filtro/{nombre}/{apellido}")]
        public IActionResult getNombreyApellido(string nombre, string apellido)
        {
            try
            {
                List<usuarios> listNomApp = (from e in _blogContext.usuarios
                                             where e.nombre.Contains(nombre) && e.apellido.Contains(apellido)
                                             select e).ToList();

                if (listNomApp == null) return NotFound();
                return Ok(listNomApp);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        //Metodo de filtración por rol
        [HttpGet]
        [Route("getrolid/{id}")]

        public IActionResult GetRolId(int id)
        {

            try
            {
                usuarios? rol = (from e in _blogContext
                          .usuarios
                                 where e.rolId == id
                                 select e).FirstOrDefault();

                if (rol == null) return NotFound();
                return Ok(rol);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
