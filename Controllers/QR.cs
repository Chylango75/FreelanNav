using Microsoft.AspNetCore.Mvc;
using MvcFreelan.Models.QR;
using QRCoder;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace MvcFreelan.Controllers
{
    public class QR : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public QR(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProcesarQR([FromBody] QRRequest request)
        {
            // Procesa el código QR recibido
            var qrLeido = request.Qr;
            // Lógica adicional aquí

            return Json(new { success = true, mensaje = "QR recibido correctamente" });
        }

        public IActionResult GenerarQR(string texto)
        {
            using (var qrGenerator = new QRCodeGenerator())
            using (var qrCodeData = qrGenerator.CreateQrCode(texto ?? "Texto por defecto", QRCodeGenerator.ECCLevel.Q))
            using (var qrCode = new QRCode(qrCodeData))
            using (var bitmap = qrCode.GetGraphic(20))
            using (var stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Png);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream.ToArray(), "image/png");
            }
        }
    }
}
