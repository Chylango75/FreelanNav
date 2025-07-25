namespace MvcFreelan.Models.Rentas
{
    public class Inquilino
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Active { get; set; }
        public DateTime FechaCaptura { get; set; }
        public Decimal CostoMensual { get; set; }
    }
}
