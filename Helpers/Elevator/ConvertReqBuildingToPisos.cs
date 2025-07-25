using MvcFreelan.GlobalVariables;
using MvcFreelan.Models.Forklift;

namespace MvcFreelan.Helpers.Worklift
{
    public class ConvertReqBuildingToPisos : IStrToPisos
    {
        public string pisos = "";
        public ConvertReqBuildingToPisos(string pisos)
        {
            this.pisos = pisos;
        }

        public IEnumerable<Piso> StrToPisos()
        {
            List<string> arrPisos = new List<string>();

            pisos = pisos.Replace(" ", "");
            arrPisos = pisos.Split(',').ToList();
            arrPisos = arrPisos.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList();

            var lstPisos = new List<Piso>();

            foreach (string s in arrPisos)
                for (int i = Info.minFloor; i <= Info.maxFloor; i++)
                    if (s.Contains(i.ToString()))
                    {
                        if (s.Contains("U") && s.Contains(i.ToString()))
                            lstPisos.Add(new Piso(i, "U"));
                        else if (s.Contains("D") && s.Contains(i.ToString()))
                            lstPisos.Add(new Piso(i, "D"));
                        else if (s.Contains(i.ToString()) && !s.Contains("U") && !s.Contains("D"))
                            lstPisos.Add(new Piso(i, ""));
                    }

            return [.. lstPisos.Distinct()];
        }
    }
}
