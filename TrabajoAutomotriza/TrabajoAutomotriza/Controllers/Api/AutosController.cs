using System.Linq;
using System.Web.Http;
using TrabajoAutomotriza.Models;

namespace TrabajoAutomotriza.Controllers.Api
{
    public class AutosController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            using (var _context = new ApplicationDbContext())
            {
                var autos = _context.Autos
                .Select(a => new
                {
                    a.idAuto,
                    a.placa,
                    a.marca,
                    a.modelo,
                    a.ano,
                    a.propietario
                })
                    .ToList();
                return Ok(autos);
            }
        }

        [HttpPost]
        public IHttpActionResult RegistrarAuto(Auto modelo)
        {
            // Validar que el cuerpo de la petición exista
            if (modelo == null)
            {
                return BadRequest("Datos de vehículo inválidos.");
            }

            // Validar reglas de modelo (DataAnnotations)
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using (var _context = new ApplicationDbContext())
            {
                var auto = new Auto
                {
                    placa = modelo.placa,
                    marca = modelo.marca,
                    modelo = modelo.modelo,
                    ano = modelo.ano,
                    propietario = modelo.propietario
                };

                _context.Autos.Add(auto);
                _context.SaveChanges();

                return Ok(new
                {
                    auto.idAuto,
                    auto.placa,
                    auto.marca,
                    auto.modelo,
                    auto.ano,
                    auto.propietario
                });
            }
        }

    }
}
