using ClassWork.Models;

namespace ClassWork.Services
{
    public class TicTacToeService:ITicTacToeService
    {
        public string PlayerMove { get; set; }
        public FIeld FIeld { get; set; }
        public string PlayerWin { get; set; }
        public string GameMode { get; set; }
        public TicTacToeService()
        {
            FIeld = new FIeld();
        }
        public void Move(int cellId,bool firstMoveMachine)
        {
            if (FIeld.Cells[cellId].Occupied)
            {
                return;
            }
            if (GameMode.Equals("vsMachine"))
            {
                int machineMoveCellId;
                if (firstMoveMachine) {
                    machineMoveCellId = FindCell();
                    FIeld.Cells.First(x => x.Id == machineMoveCellId.ToString()).Src = "/img/zero.jpg";
                    FIeld.Cells.First(x => x.Id == machineMoveCellId.ToString()).OccupiedBy = "zero";
                    FIeld.Cells.First(x => x.Id == machineMoveCellId.ToString()).Toggle = "show";
                    FIeld.Cells.First(x => x.Id == machineMoveCellId.ToString()).Occupied = true;
                    if (PlayerMove.Equals("player1"))
                        PlayerMove = "machine";
                    else
                        PlayerMove = "player1";
                    return;
                }
                
                FIeld.Cells[cellId].Src = "/img/cross.png";
                FIeld.Cells[cellId].OccupiedBy = "cross";
                FIeld.Cells[cellId].Toggle = "show";
                FIeld.Cells[cellId].Occupied = true;
                PlayerMove = "machine";
                if(WinCheck(PlayerMove))
                {
                    return;
                }
                machineMoveCellId = FindCell();

                FIeld.Cells.First(x => x.Id == machineMoveCellId.ToString()).Src = "/img/zero.jpg";
                FIeld.Cells.First(x => x.Id == machineMoveCellId.ToString()).OccupiedBy = "zero";
                FIeld.Cells.First(x => x.Id == machineMoveCellId.ToString()).Toggle = "show";
                FIeld.Cells.First(x => x.Id == machineMoveCellId.ToString()).Occupied = true;
                if (WinCheck(PlayerMove))
                {
                    return;
                }
                PlayerMove = "player1";
                
            }
            else
            {
                FIeld.Cells[cellId].Toggle = "show";
                if (PlayerMove.Equals("player1"))
                {
                    FIeld.Cells[cellId].Src = "/img/cross.png";
                    FIeld.Cells[cellId].OccupiedBy = "cross";
                }
                else
                {
                    FIeld.Cells[cellId].Src = "/img/zero.jpg";
                    FIeld.Cells[cellId].OccupiedBy = "zero";
                }
                FIeld.Cells[cellId].Occupied = true;
                if (WinCheck(PlayerMove))
                {
                    return;
                }
                if (PlayerMove.Equals("player1"))
                    PlayerMove = "player2";
                else
                    PlayerMove = "player1";
            }
            
        }
        public void Clear()
        {
            FIeld = new FIeld();
            PlayerMove = null;
            PlayerWin = null;
        }
        public void Lot()
        {
            Random rnd=new Random();
            var dig = rnd.Next(0, 2);
            if(GameMode.Equals("vsMachine"))
            {
                if (dig == 0)
                {
                    PlayerMove = "player1";
                }
                else
                {
                    PlayerMove = "machine";
                }
                return;
            }
            if(dig==0)
            {
                PlayerMove = "player1";
            }
            else
            {
                PlayerMove = "player2";
            }
        }
        private bool WinCheck(string player)
        {
            if (ThreeEqual(FIeld.Cells[0].OccupiedBy,FIeld.Cells[4].OccupiedBy,FIeld.Cells[8].OccupiedBy)||
                ThreeEqual(FIeld.Cells[2].OccupiedBy, FIeld.Cells[4].OccupiedBy, FIeld.Cells[6].OccupiedBy)||
                ThreeEqual(FIeld.Cells[0].OccupiedBy, FIeld.Cells[3].OccupiedBy, FIeld.Cells[6].OccupiedBy)||
                ThreeEqual(FIeld.Cells[1].OccupiedBy, FIeld.Cells[4].OccupiedBy, FIeld.Cells[7].OccupiedBy)||
                ThreeEqual(FIeld.Cells[2].OccupiedBy, FIeld.Cells[5].OccupiedBy, FIeld.Cells[8].OccupiedBy)||
                ThreeEqual(FIeld.Cells[0].OccupiedBy, FIeld.Cells[1].OccupiedBy, FIeld.Cells[2].OccupiedBy)||
                ThreeEqual(FIeld.Cells[3].OccupiedBy, FIeld.Cells[4].OccupiedBy, FIeld.Cells[5].OccupiedBy)||
                ThreeEqual(FIeld.Cells[6].OccupiedBy, FIeld.Cells[7].OccupiedBy, FIeld.Cells[8].OccupiedBy)
            )
            {
                string temp;
                if (player.Equals("player1"))
                {
                    temp = "Игрок 1";
                }
                else if(player.Equals("player2"))
                {
                    temp = "Игрок 2";
                }
                else
                {
                    temp = "Компьютер";
                }
                PlayerWin = $"{temp} победил!";
                BlockCells();
                return true;
            }
            else
            {
                PlayerWin = null;
                var occCells=FIeld.Cells.Where(x=>x.Occupied).Count();
                if(occCells==FIeld.Cells.Count)
                {
                    PlayerWin = "ничья";
                    BlockCells();
                }
                return false;
            }
        }
        private bool ThreeEqual(string cell1, string cell2, string cell3)
        {
            if(cell1!=null&& cell2 != null&& cell3 != null)
                return cell1 == cell2 && cell2 == cell3;
            return false;
        }
        public void BlockCells()
        {
            foreach(var item in FIeld.Cells)
            {
                item.Blocked = true;
            }
        }
        public void OpenCells()
        {
            foreach (var item in FIeld.Cells)
            {
                item.Blocked = false;
            }
        }
        public int FindCell()
        {
            var freeCells=FIeld.Cells.Where(x => !x.Occupied).ToList();

            Random rnd= new Random();
            if (freeCells.Count() == 0) return 100;
            int randomId=rnd.Next(freeCells.Count());
            while (FIeld.Cells.First(x=>x.Id == freeCells[randomId].Id).Occupied)
            {
                randomId = rnd.Next(freeCells.Count());
            }
            return Convert.ToInt32(FIeld.Cells.First(x => x.Id == freeCells[randomId].Id).Id);
        }
    }
}
