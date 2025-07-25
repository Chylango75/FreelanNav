namespace MvcFreelan.Models.Rentas
{
    public class btnRenta
    {
        public btnRenta(int Id, string Name, string Clicks)
        {
            this.Id = Id;
            this.Name= Name;
            this.Clicks = Clicks;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Clicks { get; set; }
    }
}
