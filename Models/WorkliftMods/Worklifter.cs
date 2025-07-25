using MvcFreelan.Models.Forklift;

namespace MvcFreelan.Models.WorkliftMods
{
    public class Worklifter
    {
        public string? StrReqsBuilding { get; set; }
        public string? StrReqsElevator { get; set; }
        public string? StrNextFloors { get; set; }

        public bool OpenDoors { get; set; }
        public int CurrentFloor { get; set; }
        public bool MovingUp { get; set; }
        public IEnumerable<Piso> FloorsToVisit { get; set; }
    }
}
