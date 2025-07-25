using MvcFreelan.Models.Forklift;

namespace MvcFreelan.Helpers.Worklift
{
    public class ConvertPisosToStr
    {
        IEnumerable<Piso> pisos;
        string strPisos;

        public ConvertPisosToStr(IEnumerable<Piso> pisos)
        {
            this.pisos = pisos;
            strPisos = "";
        }

        public string PisosToString()
        {
            foreach (Piso p in pisos)
                strPisos += "," + p.Floor.ToString() + p.Up;

            return strPisos;
        }
    }
}
