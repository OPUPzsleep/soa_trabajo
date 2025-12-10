using System;
using System.Linq;
using System.Web.Http;
using TrabajoAutomotriza.Models;

namespace TrabajoAutomotriza.Controllers.Api
{
    public class FacturasController : ApiController
    {

        public class CrearFacturaRequest
        {
            public int idPresupuesto { get; set; }
            public decimal totalFactura { get; set; }
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            using (var _context = new ApplicationDbContext())
            {
            var facturas = _context.Facturas
                .Join(_context.Presupuestos, f => f.idPresupuesto, p => p.idPresupuesto, (f, p) => new { f, p })
                .Join(_context.OrdenesMantenimiento, fp => fp.p.idOrden, o => o.idOrden, (fp, o) => new { fp.f, fp.p, o })
                .Join(_context.Autos, fpo => fpo.o.idAuto, a => a.idAuto, (fpo, a) => new { fpo.f, fpo.p, fpo.o, a })
                .Select(x => new
                {
                    x.f.idFactura,
                    x.f.idPresupuesto,
                    x.f.fechaFactura,
                    x.f.totalFactura,
                    x.f.estado,
                    x.o.idOrden,
                    x.a.placa,
                    x.a.marca
                })
                    .ToList();
                return Ok(facturas);
            }
        }

        [HttpPost]
        public IHttpActionResult CrearFactura(CrearFacturaRequest modelo)
        {
            using (var _context = new ApplicationDbContext())
            {
            if (modelo == null)
            {
                return BadRequest("Datos de factura inválidos.");
            }

            var factura = new Factura
            {
                idPresupuesto = modelo.idPresupuesto,
                fechaFactura = DateTime.Now,
                totalFactura = modelo.totalFactura,
                estado = "Emitida"
            };

            _context.Facturas.Add(factura);
            _context.SaveChanges();

            return Ok(new
            {
                factura.idFactura,
                factura.idPresupuesto,
                factura.fechaFactura,
                factura.totalFactura,
                    factura.estado
                });
            }
        }

    }
}
