using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CrossGames.Controls;
using CrossGames.Models;
using System.Collections.Generic;

namespace CrossGames.ViewModels
{
    public partial class PageTetrisViewModel:ViewModelBase
    {
        public TetrisMatrix matrix { get; set; } = new TetrisMatrix();
        public IEnumerable<TetrisCube> Cells => GetCubes();
       
        public PageTetrisViewModel()
        {
            //GetCubes();
            matrix.startgame();
        }
        private IEnumerable<TetrisCube> GetCubes()
        {
            for (int y = 0; y < 24; y++)
            {
                for (int x = 0; x<12; x++)
                {
                    yield return matrix.GridCells[y, x];
                }
            }
        }
    }
}
