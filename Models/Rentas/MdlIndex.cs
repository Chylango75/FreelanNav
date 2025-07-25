namespace MvcFreelan.Models.Rentas
{
    public class MdlIndex
    {
        public MdlIndex(string conn, List<btnRenta> rentas)
        {
            this.Conn = conn;
            this.Btns = rentas;
        }

        public string Conn { get; set; }
        public List<btnRenta> Btns { get; set; }
    }
}
