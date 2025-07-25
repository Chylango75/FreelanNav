namespace MvcFreelan.Models.Forklift
{
    public class MdlIndex
    {
        public MdlIndex(int minFloor, int maxFloor)
        {
            this.minFloor = minFloor;
            this.maxFloor = maxFloor;
        }
        public int minFloor { get; set; }
        public int maxFloor { get; set; }
    }
}
