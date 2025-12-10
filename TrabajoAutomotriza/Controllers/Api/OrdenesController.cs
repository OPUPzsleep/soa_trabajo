using System;
using System.Linq;
using System.Web.Http;
using TrabajoAutomotriza.Models;

namespace TrabajoAutomotriza.Controllers.Api
{
    public class OrdenesController : ApiController
    {

        public class CrearOrdenRequest
        {
            public int idAuto { get; set; }
            public int idCliente { get; set; }
            public string descripcionProblema { get; set; }
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            using (var _context = new ApplicationDbContext())
            {
            var ordenes = _context.OrdenesMantenimiento
                .Join(_context.Autos, o => o.idAuto, a => a.idAuto, (o, a) => new { o, a })
                .GroupJoin(
                    _context.Presupuestos,
                    ordenAuto => ordenAuto.o.idOrden,
                    presupuesto => presupuesto.idOrden,
                    (ordenAuto, presupuestos) => new { ordenAuto, presupuestos }
                )
                .SelectMany(
                    x => x.presupuestos.DefaultIfEmpty(),
                    (x, p) => new {
                        x.ordenAuto.o.idOrden,
                        x.ordenAuto.o.idAuto,
                        x.ordenAuto.o.idCliente,
                        x.ordenAuto.o.fechaOrden,
                        x.ordenAuto.o.fechaFinalizacion,
                        x.ordenAuto.o.descripcionProblema,
                        x.ordenAuto.o.estado,
                        x.ordenAuto.a.placa,
                        x.ordenAuto.a.marca,
                        montoEstimado = p == null ? 0 : p.montoEstimado
                    }
                )
                    .ToList();
                return Ok(ordenes);
            }
        }

        [HttpPost]
        public IHttpActionResult CrearOrden(CrearOrdenRequest modelo)
        {
            using (var _context = new ApplicationDbContext())
            {
            if (modelo == null)
            {
                return BadRequest("Datos de orden inválidos.");
            }

            var orden = new OrdenMantenimiento
            {
                idAuto = modelo.idAuto,
                idCliente = modelo.idCliente,
                descripcionProblema = modelo.descripcionProblema,
                fechaOrden = DateTime.Now,
                estado = "En Progreso"
            };

            _context.OrdenesMantenimiento.Add(orden);
            _context.SaveChanges();

            return Ok(new
            {
                orden.idOrden,
                orden.idAuto,
                orden.idCliente,
                orden.fechaOrden,
                orden.descripcionProblema,
                    orden.estado
                });
            }
        }

        [HttpPut]
        public IHttpActionResult FinalizarOrden(int id)
        {
            using (var _context = new ApplicationDbContext())
            {
            var orden = _context.OrdenesMantenimiento.FirstOrDefault(o => o.idOrden == id);
            if (orden == null)
            {
                return NotFound();
            }

            orden.estado = "Completada";
            orden.fechaFinalizacion = DateTime.Now;
            _context.SaveChanges();

            return Ok(new
            {
                orden.idOrden,
                orden.estado,
                    orden.fechaFinalizacion
                });
            }
        }

    }
}
