using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using TrabajoAutomotriza.Models;

namespace TrabajoAutomotriza.Controllers
{
    public class PagosController : Controller
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        // GET: Pagos
        public async Task<ActionResult> Index()
        {
            var facturasPendientes = await _context.Facturas
                .Include(f => f.Presupuesto.OrdenMantenimiento.Auto)
                .Where(f => f.estado == "Emitida")
                .ToListAsync();

            return View(facturasPendientes);
        }

        // POST: Pagos/ProcesarPago
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ProcesarPago(int facturaId, decimal monto, string metodoPago)
        {
            var factura = await _context.Facturas.FindAsync(facturaId);
            if (factura == null)
            {
                return HttpNotFound();
            }

            factura.estado = "Pagada";

            var pago = new Pago
            {
                idFactura = facturaId,
                fechaPago = DateTime.Now,
                montoPagado = monto
            };

            _context.Pagos.Add(pago);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
