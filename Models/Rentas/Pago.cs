namespace MvcFreelan.Models.Rentas
{
    public class Pago
    {
        public int PagoId { get; set; }
        public int InquilinoId { get; set; }
        public int PagoTipo { get; set; }
        public decimal PagoTotal { get; set; }
        public string FechaPago { get; set; }
        public string FechaAplica { get; set; }
        public string PagoTipoStr { get; set; }
        public string InquiStr { get; set; }

        public string MesIni { get; set; }
        public string MesFin { get; set; }
    }
}
