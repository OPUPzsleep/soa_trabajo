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

            using (var _context = new ApplicationDbContext())
            {
                var pago = new Pago
                {
                    idFactura = modelo.idFactura,
                    fechaPago = DateTime.Now,
                    montoPagado = modelo.montoPagado
                };

                _context.Pagos.Add(pago);
                _context.SaveChanges();

                return Ok(new
                {
                    pago.idPago,
                    pago.idFactura,
                    pago.fechaPago,
                    pago.montoPagado
                });
            }
        }
    }
}
