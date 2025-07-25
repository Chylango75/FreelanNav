using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MvcFreelan.Data;
using MvcFreelan.Helpers;
using MvcFreelan.Models;
using MvcFreelan.Models.Rentas;
using Rentas.Models;
using System.Configuration;
using System.Drawing.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MvcFreelan.Controllers
{
    public class RentasController : Controller
    {
        private IConfiguration _configuration;
        private string conn = "";

        public RentasController(IConfiguration configuration)
        {
            _configuration = configuration;
            string codedConn = configuration.GetConnectionString("conn");
            conn = codedConn.DesEncriptar();
        }

        public IActionResult Index()
        {
            DbRenta db = new DbRenta(conn);
            var respDb = db.VerifConn();

            // Verif DB connection
            if (respDb.Contains("ERROR"))
            {
                var errorViewModel = new ErrorViewModel();
                errorViewModel.RequestId = respDb;
                return View("Error", errorViewModel);
            }

            var btns = GetMenuButtons();
            var btnsVM = new MdlIndex(respDb, btns);
            
            return View(btnsVM);
        }

        /////////////////          Servicios         //////////////////////

        public JsonResult InquisGetN(string term)
        {
            DbRenta db = new DbRenta(conn);

            var lst = db.InquisGetN(term);
            return Json(lst);
        }

        public JsonResult InquisSinContrato(string term)
        {
            DbRenta db = new DbRenta(conn);
            var lst = db.InquisSinContrato(term);
            return Json(lst);
        }

        [HttpPost]
        public string InquiAdd(Inquilino inqui)
        {
            try{
                string resp = "ERROR";

                DbRenta db = new DbRenta(conn);
                resp = db.InquiAdd(inqui);
                return resp;
            }
            catch (Exception ex)
            {
                return "ERROR: " + ex.Message;
            }
        }

        public JsonResult ContratosGetN(string term)
        {
            DbRenta db = new DbRenta(conn);
            var lst = db.ContratosGetN(term);
            return Json(lst);
        }

        [HttpPost]
        public string ContratoAdd(Contrato contrato)
        {
            try
            {
                string resp = "ERROR";

                DbRenta db = new DbRenta(conn);
                resp = db.ContratoAdd(contrato);
                return resp;
            }
            catch (Exception ex)
            {
                return "ERROR: " + ex.Message;
            }
        }


        [HttpPost]
        public string PagoAdd(Pago pago)
        {
            try
            {
                string resp = "ERROR";
                DbRenta db = new DbRenta(conn);
                resp = db.PagoAdd(pago);
                return resp;
            }
            catch (Exception ex)
            {
                return "ERROR: " + ex.Message;
            }
        }

        /////////////////        Partial Views       //////////////////////

        [HttpPost]
        public PartialViewResult GetPartialInquilino()
        {
            return PartialView("~/Views/Rentas/_Inquilino.cshtml");
        }

        [HttpPost]
        public PartialViewResult GetPartialContrato()
        {
            return PartialView("~/Views/Rentas/_Contrato.cshtml");
        }

        [HttpPost]
        public PartialViewResult GetPartialPago()
        {
            return PartialView("~/Views/Rentas/_Pago.cshtml");
        }

        [HttpPost]
        public PartialViewResult GetPartialReportes()
        {
            return PartialView("~/Views/Rentas/_Reportes.cshtml");
        }

        [HttpPost]
        public PartialViewResult GetPartialPagos(string iden)
        {
            try
            {
                DbRenta db = new DbRenta(conn);
                var pagos = db.PagosByInqui(iden);
                return PartialView("_TblPagos", pagos);
            }
            catch (Exception ex)
            {
                var pagos = new List<Pago>()
                {
                    new Pago() { MesIni = "Error en Petición", MesFin = ex.Message }
                };
                
                return PartialView("_TblPagos", pagos);
            }
        }

        /////////////////          Local Functions         //////////////////////

        private List<btnRenta> GetMenuButtons()
        {
            var btns = new List<btnRenta>()
            {
                new btnRenta(0,"Reportes", "GetPartialReportes()"),
                new btnRenta(2,"Inquilinos", "GetPartialInquilino()"),
                new btnRenta(3,"Contrato", "GetPartialContrato()"),
                new btnRenta(1,"Pagos", "GetPartialPago()"),
            };

            return btns;
        }

    }
}
