namespace ClassWork.Models
{
    public class FIeld
    {
        public List<Cell> Cells { get; set; }
        public FIeld()
        {
            Cells = new List<Cell>();
            for (int i = 0; i < 9; i++)
            {
                Cells.Add(new Cell() { Toggle = "hide",Id=i.ToString() });
            }
        }
    }
}
