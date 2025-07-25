using MvcFreelan.Models.Forklift;

namespace MvcFreelan.Helpers.Worklift
{
    public class AccommodatePisos
    {
        IEnumerable<Piso> pisos;
        public AccommodatePisos(IEnumerable<Piso> pisos)
        {
            this.pisos = pisos;
        }

        public IEnumerable<Piso> GetOrdered()
        {
            //   Filter repeated
            var ordered = pisos.GroupBy(c => new { c.Up, c.Floor })
                .Select(c => c.First()).ToList();

            //   Order By Floor, then Up
            ordered = ordered.OrderBy(f => f.Floor).ThenBy(u => u.Up).Reverse().ToList();

            return ordered;
        }
    }
}
