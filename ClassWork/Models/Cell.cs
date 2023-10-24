namespace ClassWork.Models
{
    public class Cell
    {
        public string Src{get;set;}
        public string Toggle { get; set;}
        public string Id { get;set;}
        public bool Occupied { get; set;}
        public string OccupiedBy { get;set;}
        public bool Blocked { get; set; }
        public Cell()
        {
            Toggle = "hide";
            Occupied=false;
            Blocked = true;
        }
    }
}
