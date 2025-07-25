namespace Rentas.Models
{
    public class Saldo
    {
        public string Mes { get; set; }
        public DateTime MesIni { get; set; }
        public DateTime MesFin { get; set; }
        public decimal CostoMensual { get; set; }
        public decimal Pagado { get; set; }
        public decimal Diferencia { get; set; }
    }
}
