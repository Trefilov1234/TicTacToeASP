using ClassWork.Models;

namespace ClassWork.ViewModels
{
    public class TicTacToeViewModel
    {
        public FIeld FIeld { get; set; }
        public string PlayerMove { get; set; }
        public string PlayerWin { get; set; }
        public string GameMode { get; set; }
        public bool ButtonBlock { get; set; }   
    }
}
