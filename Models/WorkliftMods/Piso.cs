namespace MvcFreelan.Models.Forklift
{
    public class Piso
    {
        public Piso(int Floor, string Up)
        {
            this.Floor = Floor;
            this.Up = Up;
        }
        public int Floor { get; set; }
        public string Up { get; set; }
    }
}
