using System;
using System.Web.Http;
using TrabajoAutomotriza.Models;

namespace TrabajoAutomotriza.Controllers.Api
{
    public class PagosController : ApiController
    {
        public class RegistrarPagoRequest
        {
            public int idFactura { get; set; }
            public decimal montoPagado { get; set; }
        }

        [HttpPost]
        public IHttpActionResult RegistrarPago(RegistrarPagoRequest modelo)
        {
            if (modelo == null)
            {
                return BadRequest("Datos de pago inválidos.");
            }

            if (modelo.montoPagado <= 0)
            {
                return BadRequest("El monto pagado debe ser mayor que cero.");
            }

            using (var _context = new ApplicationDbContext())
            {
                var factura = _context.Facturas.Find(modelo.idFactura);
                if (factura == null)
                {
                    return BadRequest("La factura especificada no existe.");
                }

                if (factura.estado == "Pagada")
                {
                    return BadRequest("La factura ya se encuentra pagada.");
                }

                var pago = new Pago
                {
                    idFactura = modelo.idFactura,
                    fechaPago = DateTime.Now,
                    montoPagado = modelo.montoPagado
                };

                // Marcar factura como pagada y registrar el pago
                factura.estado = "Pagada";

                _context.Pagos.Add(pago);
                _context.SaveChanges();

                return Ok(new
                {
                    pago.idPago,
                    pago.idFactura,
                    pago.fechaPago,
                    pago.montoPagado,
                    estadoFactura = factura.estado
                });
            }
        }
    }
}
