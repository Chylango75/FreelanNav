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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SrvEncrypt(string textoEncrypt)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(textoEncrypt);
            result = Convert.ToBase64String(encryted);
            var resultado = $"{result}";
            return Json(new { resultado });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SrvDecrypt(string textoDecrypt)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(textoDecrypt);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            var resultado = $"{result}";
            return Json(new { resultado });
        }
    }
}
