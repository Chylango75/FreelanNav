namespace MvcFreelan.Models.Forklift
{
    public class Ubica
    {
        public Ubica(int NumPiso, bool OpenDoors)
        {
            this.NumPiso = NumPiso;
            this.OpenDoors = OpenDoors;
        }

        public int NumPiso { get; set; }
        public bool OpenDoors { get; set; }
    }
}
