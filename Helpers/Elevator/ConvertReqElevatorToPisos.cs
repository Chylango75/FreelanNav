using MvcFreelan.GlobalVariables;
using MvcFreelan.Models.Forklift;

namespace MvcFreelan.Helpers.Worklift
{
    public class ConvertReqElevatorToPisos : IStrToPisos
    {
        public string pisos = "";
        public ConvertReqElevatorToPisos(string pisos)
        {
            this.pisos = pisos;
        }

        public IEnumerable<Piso> StrToPisos()
        {
            pisos = pisos.Replace(" ", "");
            pisos = pisos.Replace("undefined", "");
            var arrPisos = pisos.Split(',').ToList();

            var lstPisos = new List<Piso>();

            foreach (string s in arrPisos)
                for (int i = Info.minFloor; i <= Info.maxFloor; i++)
                    if (s.Contains(i.ToString()))
                    {
                        if (s.Contains(i.ToString()) && !s.Contains("U") && !s.Contains("D"))
                            lstPisos.Add(new Piso(i, ""));
                    }

            return [.. lstPisos.Distinct()];

        }
    }
}
