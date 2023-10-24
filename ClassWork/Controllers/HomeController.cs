using ClassWork.Models;
using ClassWork.Services;
using ClassWork.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ClassWork.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITicTacToeService ticTacToeService;
        public HomeController(ITicTacToeService ticTacToeService)
        {
            this.ticTacToeService = ticTacToeService;
        }
        TicTacToeViewModel ticTacToeViewModel = new TicTacToeViewModel();
        public IActionResult Index()
        {
            ticTacToeViewModel.FIeld = ticTacToeService.FIeld;
            ticTacToeViewModel.PlayerMove = ticTacToeService.PlayerMove;
            ticTacToeViewModel.PlayerWin = ticTacToeService.PlayerWin;
            ticTacToeViewModel.GameMode = ticTacToeService.GameMode;
            return View(ticTacToeViewModel);
        }
        
        public IActionResult CellClick(int cellId)
        {
            ticTacToeService.Move(cellId,false);
            
            return RedirectToAction("Index", ticTacToeViewModel);
        }
        public IActionResult StartGame()
        {
            ticTacToeService.Clear();
            ticTacToeService.OpenCells();
            ticTacToeService.Lot();
            if(ticTacToeService.GameMode.Equals("vsMachine")&&ticTacToeService.PlayerMove.Equals("machine"))
            {
                ticTacToeService.Move(0, true);
            }
            
            return RedirectToAction("Index", ticTacToeViewModel);
        }
        public IActionResult ChooseMode(string mode)
        {
            ticTacToeService.GameMode= mode;
            ticTacToeViewModel.ButtonBlock = true;
            return RedirectToAction("Index", ticTacToeViewModel);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}