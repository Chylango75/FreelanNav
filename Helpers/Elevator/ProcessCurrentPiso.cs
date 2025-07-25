using MvcFreelan.GlobalVariables;
using MvcFreelan.Models.WorkliftMods;


namespace MvcFreelan.Helpers.Worklift
{
    public class ProcessCurrentPiso
    {
        Worklifter worklift;
        public ProcessCurrentPiso(Worklifter worklift)
        {
            this.worklift = worklift;
        }

        public Worklifter SetPiso()
        {
            if (worklift.MovingUp)
                ProcessUpDown("UP");

            else if (!worklift.MovingUp)
                ProcessUpDown("DOWN");

            return this.worklift;
        }


        private Worklifter ProcessUpDown(string direction)
        {
            string wildcard = "";

            if (direction == "UP")
                wildcard = "U";
            if (direction == "DOWN")
                wildcard = "D";

            //  Veriy if open doors
            worklift.OpenDoors = worklift.FloorsToVisit.Where(d =>
                d.Floor == worklift.CurrentFloor && d.Up == wildcard
            || d.Floor == worklift.CurrentFloor && d.Up == ""
            ).ToList().Any();

            //   Remove this floor from "floors to visit"
            worklift.FloorsToVisit = worklift.FloorsToVisit.Where(
                u => u.Floor != worklift.CurrentFloor
                || u.Floor == worklift.CurrentFloor && u.Up != wildcard
                ).ToList();

            var filtrados = worklift.FloorsToVisit.Except(
                worklift.FloorsToVisit.Where(
                    x => x.Floor == worklift.CurrentFloor && x.Up == ""
                    ).ToList())
                .ToList();

            worklift.FloorsToVisit = filtrados;

            //  Return in case just keep open doors
            if (worklift.OpenDoors == true)
                return this.worklift;


            //  Then,  verify if visiting floors above
            if (direction == "UP")
            {
                if (worklift.FloorsToVisit.Where(p => p.Floor > worklift.CurrentFloor).ToList().Count > 0)  /////////////////
                {
                    ///   Go Up 1 floor if not in top
                    if (worklift.CurrentFloor + 1 == Info.maxFloor)
                    {
                        worklift.CurrentFloor = Info.maxFloor;
                    }
                    else
                        worklift.CurrentFloor++;
                }
                else
                    worklift.MovingUp = false;
            }


            //  Or,  verify if visiting floors down
            if (direction == "DOWN")
            {
                if (worklift.FloorsToVisit.Where(p => p.Floor < worklift.CurrentFloor).ToList().Count > 0)
                {
                    ///   Go Down 1 floor if not in top
                    if (worklift.CurrentFloor - 1 == Info.minFloor)
                    {
                        worklift.CurrentFloor = Info.minFloor;
                    }
                    else
                        worklift.CurrentFloor--;
                }
                else
                    worklift.MovingUp = true;
            }

            return this.worklift;
        }
    }
}
