using System;
using System.Linq;
using System.Web.Http;
using TrabajoAutomotriza.Models;

namespace TrabajoAutomotriza.Controllers.Api
{
    public class PresupuestosController : ApiController
    {

        public class CrearPresupuestoRequest
        {
            public int idAuto { get; set; }
            public int idCliente { get; set; }
            public string descripcionProblema { get; set; }
            public decimal montoEstimado { get; set; }
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            using (var _context = new ApplicationDbContext())
            {
            var presupuestos = _context.Presupuestos
                .Join(_context.OrdenesMantenimiento, p => p.idOrden, o => o.idOrden, (p, o) => new { p, o })
                .Join(_context.Autos, po => po.o.idAuto, a => a.idAuto, (po, a) => new { po.p, po.o, a })
                .Select(x => new
                {
                    x.p.idPresupuesto,
                    x.p.idOrden,
                    x.p.fechaPresupuesto,
                    x.p.montoEstimado,
                    x.p.estado,
                    x.a.placa,
                    x.a.marca,
                    x.o.descripcionProblema
                })
                    .ToList();
                return Ok(presupuestos);
            }
        }

        [HttpPost]
        public IHttpActionResult CrearPresupuesto(CrearPresupuestoRequest modelo)
        {
            using (var _context = new ApplicationDbContext())
            {
            if (modelo == null)
            {
                return BadRequest("Datos de presupuesto inválidos.");
            }

            try
            {
                // Verificar que el auto existe
                var autoExiste = _context.Autos.Any(a => a.idAuto == modelo.idAuto);
                if (!autoExiste)
                {
                    return BadRequest("El auto especificado no existe.");
                }

                // Crear o usar cliente por defecto
                int idCliente = 1;
                var clienteExiste = _context.Clientes.Any(c => c.idCliente == 1);
                
                if (!clienteExiste)
                {
                    // Crear cliente por defecto
                    var clienteDefault = new Cliente
                    {
                        nombres = "Cliente",
                        apellidos = "Predeterminado",
                        telefono = "000-0000",
                        idAuto = modelo.idAuto
                    };
                    _context.Clientes.Add(clienteDefault);
                    _context.SaveChanges();
                    idCliente = clienteDefault.idCliente;
                }

                // Crear orden inicialmente en estado Pendiente
                var orden = new OrdenMantenimiento
                {
                    idAuto = modelo.idAuto,
                    idCliente = idCliente,
                    descripcionProblema = modelo.descripcionProblema,
                    fechaOrden = DateTime.Now,
                    estado = "Pendiente"
                };

                _context.OrdenesMantenimiento.Add(orden);
                _context.SaveChanges();

                // Crear presupuesto en estado Pendiente
                var presupuesto = new Presupuesto
                {
                    idOrden = orden.idOrden,
                    fechaPresupuesto = DateTime.Now,
                    montoEstimado = modelo.montoEstimado,
                    estado = "Pendiente"
                };

                _context.Presupuestos.Add(presupuesto);
                _context.SaveChanges();

                return Ok(new
                {
                    presupuesto.idPresupuesto,
                    presupuesto.idOrden,
                    presupuesto.fechaPresupuesto,
                    presupuesto.montoEstimado,
                    presupuesto.estado
                });
            }
            catch (Exception ex)
            {
                    return BadRequest("Error al crear presupuesto: " + ex.Message);
                }
            }
        }

        [HttpPut]
        [Route("api/presupuestos/{id}/aprobar")]
        public IHttpActionResult Aprobar(int id)
        {
            using (var _context = new ApplicationDbContext())
            {
            var presupuesto = _context.Presupuestos.FirstOrDefault(p => p.idPresupuesto == id);
            if (presupuesto == null)
            {
                return NotFound();
            }

            var orden = _context.OrdenesMantenimiento.FirstOrDefault(o => o.idOrden == presupuesto.idOrden);
            if (orden == null)
            {
                return BadRequest("No se encontró la orden asociada al presupuesto.");
            }

            presupuesto.estado = "Aprobado";
            orden.estado = "En Progreso";
            _context.SaveChanges();

            return Ok(new
            {
                presupuesto.idPresupuesto,
                EstadoPresupuesto = presupuesto.estado,
                orden.idOrden,
                    EstadoOrden = orden.estado
                });
            }
        }

    }
}
