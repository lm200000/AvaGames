using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CrossGames.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossGames.Models
{
    public partial class TetrisMatrix : ObservableObject
    {
        private readonly Random _random = new();
        [ObservableProperty]
        private int _score;
        [ObservableProperty]
        public TetrisCube[,] _gridCells = new TetrisCube[24, 12];

        private DispatcherTimer? timer;
        private TetrisCube[,]? currentShape;
        private readonly object _lock = new();
        public TetrisMatrix()
        {
            Initgrid();
            
            //timer.Start();
        }
        private void timer_Tick(object? sender, EventArgs e)
        {
            MoveDown();
        }
        public void Initgrid()
        {
            for (int x = 0; x < 24; x++)
            {
                for (int y = 0; y < 12; y++)
                {
                    var cube = new TetrisCube(x, y, TCubeShape.None);
                    GridCells[x, y] = cube;
                }
            }
        }
        public void startgame()
        {
            if (timer == null)
            {
                timer = new DispatcherTimer();
                currentShape = CreateShape();
                AddShapeToGrid();
                timer.Tick += timer_Tick;
                timer.Interval = TimeSpan.FromMilliseconds(1000);
                timer.Start();
            }            
        }
        private TetrisCube[,] CreateShape()
        {
            var newShape = _random.Next(5);
            return newShape switch
            {
                0 => new TetrisCube[,]
                {
                     { new TetrisCube(0,5,TCubeShape.O), new TetrisCube(0, 6, TCubeShape.O) },
                     { new TetrisCube(1,5,TCubeShape.O), new TetrisCube(1,6,TCubeShape.O) }
                },
                1 => new TetrisCube[,]
                {
                     { new TetrisCube(0,5,TCubeShape.L), new TetrisCube(0, 6, TCubeShape.None) },
                     { new TetrisCube(1,5,TCubeShape.L), new TetrisCube(1,6,TCubeShape.None) },
                     { new TetrisCube(2,5,TCubeShape.L), new TetrisCube(2,6,TCubeShape.L) }
                },
                2 => new TetrisCube[,]
                {
                    { new TetrisCube(0, 6, TCubeShape.I) },
                     { new TetrisCube(1,6,TCubeShape.I) },
                     { new TetrisCube(2,6,TCubeShape.I) },
                     { new TetrisCube(3,6,TCubeShape.I) }
                },
                3 => new TetrisCube[,]
                {
                     { new TetrisCube(0, 4, TCubeShape.None), new TetrisCube(0,5,TCubeShape.S), new TetrisCube(0,6,TCubeShape.S) },
                     { new TetrisCube(1,4,TCubeShape.S), new TetrisCube(1,5,TCubeShape.S), new TetrisCube(1,6,TCubeShape.None) }
                },
                4 => new TetrisCube[,]
                {
                     { new TetrisCube(0,4,TCubeShape.Z), new TetrisCube(0,5,TCubeShape.Z), new TetrisCube(0,6,TCubeShape.None) },
                     { new TetrisCube(1,4,TCubeShape.None), new TetrisCube(1,5,TCubeShape.Z), new TetrisCube(1,6,TCubeShape.Z) }
                },
                _ => throw new InvalidOperationException("Invalid shape index"),
            };
        }
        private void AddShapeToGrid()
        {
            if (currentShape != null)
            {
                for (int x = 0; x < currentShape.GetLength(0); x++)
                {
                    for (int y = 0; y < currentShape.GetLength(1); y++)
                    {
                        if (currentShape[x, y].Shape != TCubeShape.None)
                        {
                            currentShape[x, y].IsActive = true;
                            int gridX = currentShape[x, y].PositionX;
                            int gridY = currentShape[x, y].PositionY;
                            GridCells[gridX, gridY].Shape = currentShape[x, y].Shape;
                            GridCells[gridX, gridY].IsActive = currentShape[x, y].IsActive;
                        }
                    }
                }
            }
        }
        [RelayCommand]
        private void pasue()
        {
            if (timer?.IsEnabled==true)
            {
                timer.Stop();
            }
            else
            {
                timer.Start();
            }
        }
        [RelayCommand]
        private void MoveRight()
        {
            if (currentShape is not null && timer?.IsEnabled==true)
            {
                lock (_lock)
                {
                    if (CanMoveRight())
                    {
                        int row = currentShape.GetLength(0);
                        int col = currentShape.GetLength(1);
                        for (int x = row - 1; x >= 0; x--)
                        {
                            for (int y = col-1; y >=0; y--)
                            {
                                if (currentShape[x, y].Shape != TCubeShape.None)
                                {
                                    var px = currentShape[x, y].PositionX;
                                    var py = currentShape[x, y].PositionY;
                                    currentShape[x, y].PositionY += 1;
                                    GridCells[px, py].IsActive = false;
                                    GridCells[px, py].Shape = TCubeShape.None;
                                    GridCells[px, py + 1].Shape = currentShape[x, y].Shape;
                                    GridCells[px, py + 1].IsActive = true;
                                }
                            }
                        }
                    }
                }
            }
        }
        [RelayCommand]
        private void MoveLeft()
        {
            if (currentShape is not null && timer?.IsEnabled == true)
            {
                lock (_lock)
                {
                    if (CanMoveLeft())
                    {
                        int row = currentShape.GetLength(0);
                        int col = currentShape.GetLength(1);
                        for (int x = row - 1; x >= 0; x--)
                        {
                            for (int y = 0; y < col; y++)
                            {
                                if (currentShape[x, y].Shape != TCubeShape.None)
                                {
                                    var px = currentShape[x, y].PositionX;
                                    var py = currentShape[x, y].PositionY;
                                    currentShape[x, y].PositionY -= 1;
                                    GridCells[px, py].IsActive = false;
                                    GridCells[px, py].Shape = TCubeShape.None;
                                    GridCells[px, py - 1].Shape = currentShape[x, y].Shape;
                                    GridCells[px, py - 1].IsActive = true;
                                }
                            }
                        }
                    }
                }
            }
        }
        [RelayCommand]
        private void MoveDown()
        {
            if (currentShape is not null && timer?.IsEnabled == true)
            {
                lock (_lock)
                {
                    if (CanMoveDown())
                    {
                        int row = currentShape.GetLength(0);
                        int col = currentShape.GetLength(1);
                        for (int x = row - 1; x >= 0; x--)
                        {
                            for (int y = 0; y < col; y++)
                            {
                                if (currentShape[x, y].Shape != TCubeShape.None)
                                {
                                    var px = currentShape[x, y].PositionX;
                                    var py = currentShape[x, y].PositionY;
                                    currentShape[x, y].PositionX += 1;
                                    GridCells[px, py].IsActive = false;
                                    GridCells[px, py].Shape = TCubeShape.None;
                                    GridCells[px + 1, py].Shape = currentShape[x, y].Shape;
                                    GridCells[px + 1, py].IsActive = true;
                                }
                            }
                        }
                    }
                    else//不可继续移动，检查行是否可消除，生成新方块
                    {
                        CheckAddScore();
                        setNonactive();
                        currentShape = CreateShape();
                        AddShapeToGrid();
                    }
                }
                
            }
        }
        [RelayCommand]
        private void Rotate()
        {
            // Rotation logic to be implemented
        }
        /// <summary>
        /// 检查当前对象是否可向右移动
        /// </summary>
        /// <returns></returns>
        private bool CanMoveRight()
        {
            if (currentShape is not null)
            {
                int row = currentShape.GetLength(0);
                int col = currentShape.GetLength(1);
                for (int x = row - 1; x >= 0; x--)
                {
                    for (int y = 0; y < col; y++)
                    {
                        if (currentShape[x, y].Shape != TCubeShape.None)
                        {
                            int gridX = currentShape[x, y].PositionX;
                            int gridY = currentShape[x, y].PositionY;
                            // Check if the position below is out of bounds or occupied
                            if (gridY + 1 >= 12 || (GridCells[gridX, gridY + 1].Shape != TCubeShape.None && GridCells[gridX, gridY + 1].IsActive == false))
                            {
                                return false;
                            }
                        }
                    }
                }

            }
            else
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 检查当前对象是否可向左移动
        /// </summary>
        /// <returns></returns>
        private bool CanMoveLeft()
        {
            if (currentShape is not null)
            {
                int row = currentShape.GetLength(0);
                int col = currentShape.GetLength(1);
                for (int x = row - 1; x >= 0; x--)
                {
                    for (int y = 0; y < col; y++)
                    {
                        if (currentShape[x, y].Shape != TCubeShape.None)
                        {
                            int gridX = currentShape[x, y].PositionX;
                            int gridY = currentShape[x, y].PositionY;
                            // Check if the position below is out of bounds or occupied
                            if (gridY - 1 < 0 || (GridCells[gridX, gridY-1].Shape != TCubeShape.None && GridCells[gridX, gridY-1].IsActive == false))
                            {
                                return false;
                            }
                        }
                    }
                }

            }
            else
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 检查当前对象是否可向下移动
        /// </summary>
        /// <returns></returns>
        private bool CanMoveDown()
        {
            if (currentShape is not null)
            {
                int row = currentShape.GetLength(0);
                int col = currentShape.GetLength(1);
                for(int x = row - 1; x >= 0; x--)
                {
                    for(int y = 0; y < col; y++)
                    {
                        if (currentShape[x, y].Shape != TCubeShape.None)
                        {
                            int gridX = currentShape[x, y].PositionX;
                            int gridY = currentShape[x, y].PositionY;
                            // Check if the position below is out of bounds or occupied
                            if (gridX + 1 >= 24 || (GridCells[gridX+1, gridY].Shape != TCubeShape.None && GridCells[gridX + 1, gridY].IsActive==false))
                            {
                                return false;
                            }                            
                        }
                    }
                }
                
            }
            else
            {
                return false;
            }
            return true;
        }

        private bool CanRotate()
        {
            // Rotation check logic to be implemented
            return true;
        }

        private void CheckAddScore()
        {
            for(int x = 0; x < 24; x++)
            {
                bool fullLine = true;
                for(int y = 0; y < 12; y++)
                {
                    if (GridCells[x, y].Shape == TCubeShape.None)
                    {
                        fullLine = false;
                        break;
                    }
                }
                if (fullLine)
                {
                    Score += 100;
                    // Clear the line and move everything above down
                    for(int row = x; row > 0; row--)
                    {
                        for(int col = 0; col < 12; col++)
                        {
                            GridCells[row, col].Shape = GridCells[row - 1, col].Shape;
                            GridCells[row, col].IsActive = false;
                        }
                    }
                    // Clear the top line
                    for(int col = 0; col < 12; col++)
                    {
                        GridCells[0, col].Shape = TCubeShape.None;
                        GridCells[0, col].IsActive = false;
                    }
                }
            }
        }
        private void setNonactive()
        {
           for(int x=0;x<24;x++)
            {
                for(int y=0;y<12;y++)
                {
                    if (GridCells[x, y].IsActive)
                    {
                        GridCells[x, y].IsActive = false;
                    }
                }
            }
        }
    }
}
