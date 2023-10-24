using ClassWork.Models;

namespace ClassWork.Services
{
    public interface ITicTacToeService
    {
        public FIeld FIeld { get; set; }
        public string PlayerMove { get; set; }
        public string PlayerWin { get; set; }
        public string GameMode { get; set; }
        public void Move(int cellId,bool firstMoveMachine);
        public void Clear();
        public void Lot();
        public void OpenCells();
        public void BlockCells();
    }
}
