namespace MvcFreelan.Models.Rentas
{
    public class Contrato
    {
        public int ContratoId { get; set; }
        public int InquilinoId { get; set; }
        public DateTime MesIni { get; set; }
        public DateTime MesFin { get; set; }

        public string NumContrato { get; set; }
        
        public decimal CostoMensual { get; set; }

        public string NombreInqui { get; set; }

        public DateTime FechaCreacion { get; set; }
    }
}
