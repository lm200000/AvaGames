using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Models
{
    public class CubeMatrix
    {
        private readonly int[,] _grid = new int[4, 4];
        private readonly Random _random = new Random();
        private int _score;

        public int[,] Grid => (int[,])_grid.Clone();
        public int Score => _score;
        public bool IsGameOver { get; private set; }

        public CubeMatrix() 
        {
            AddRandomTile();
            AddRandomTile();
        }
        public bool Move(MoveDirection direction)
        {
            // 保存移动前的状态用于比较 
            var oldGrid = (int[,])_grid.Clone();

            // 根据方向处理移动 
            switch (direction)
            {
                case MoveDirection.Left:
                    MoveLeft();
                    break;
                case MoveDirection.Right:
                    MoveRight();
                    break;
                case MoveDirection.Up:
                    MoveUp();
                    break;
                case MoveDirection.Down:
                    MoveDown();
                    break;
            }

            // 检查是否发生移动
            if (!GridChanged(oldGrid))
                return false;

            // 添加新方块并检查游戏是否结束
            AddRandomTile();
            IsGameOver = CheckGameOver();

            return true;
        }
        private bool GridChanged(int[,] oldGrid)
        {
            for (int row = 0; row < 4; row++)
                for (int col = 0; col < 4; col++)
                    if (oldGrid[row, col] != _grid[row, col])
                        return true;

            return false;
        }

        private bool CheckGameOver()
        {
            // 检查是否有空格 
            for (int row = 0; row < 4; row++)
                for (int col = 0; col < 4; col++)
                    if (_grid[row, col] == 0)
                        return false;

            // 检查是否有可合并的相邻方块 
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (col < 3 && _grid[row, col] == _grid[row, col + 1])
                        return false;

                    if (row < 3 && _grid[row, col] == _grid[row + 1, col])
                        return false;
                }
            }

            return true;
        }
        private void AddRandomTile()
        {
            var emptyCells = new List<(int, int)>();

            // 收集所有空格子
            for (int row = 0; row < 4; row++)
                for (int col = 0; col < 4; col++)
                    if (_grid[row, col] == 0)
                        emptyCells.Add((row, col));

            if (emptyCells.Count == 0)
                return;

            // 随机选择一个空格子
            var (r, c) = emptyCells[_random.Next(emptyCells.Count)];

            // 90%概率生成2，10%概率生成4
            _grid[r, c] = _random.Next(10) < 9 ? 2 : 4;
        }
        private void MoveLeft()
        {
            for (int row = 0; row < 4; row++)
            {
                // 1. 移除空格 
                var nonEmptyTiles = new List<int>();
                for (int col = 0; col < 4; col++)
                {
                    if (_grid[row, col] != 0)
                        nonEmptyTiles.Add(_grid[row, col]);
                }

                // 2. 合并相同数字
                var mergedRow = new List<int>();
                for (int i = 0; i < nonEmptyTiles.Count; i++)
                {
                    if (i < nonEmptyTiles.Count - 1 && nonEmptyTiles[i] == nonEmptyTiles[i + 1])
                    {
                        mergedRow.Add(nonEmptyTiles[i] * 2);
                        _score += nonEmptyTiles[i] * 2;
                        i++; // 跳过下一个已经合并的方块
                    }
                    else
                    {
                        mergedRow.Add(nonEmptyTiles[i]);
                    }
                }

                // 3. 填充剩余位置为0 
                while (mergedRow.Count < 4)
                    mergedRow.Add(0);

                // 4. 更新行数据 
                for (int col = 0; col < 4; col++)
                    _grid[row, col] = mergedRow[col];
            }
        }
        private void MoveRight()
        {
            RotateGrid180();
            MoveLeft();
            RotateGrid180();
        }

        private void MoveUp()
        {
            RotateGridCounterClockwise();
            MoveLeft();
            RotateGridClockwise();
        }

        private void MoveDown()
        {
            RotateGridClockwise();
            MoveLeft();
            RotateGridCounterClockwise();
        }
        private void RotateGridClockwise()
        {
            var newGrid = new int[4, 4];
            for (int row = 0; row < 4; row++)
                for (int col = 0; col < 4; col++)
                    newGrid[col, 3 - row] = _grid[row, col];

            Array.Copy(newGrid, _grid, 16);
        }

        private void RotateGridCounterClockwise()
        {
            var newGrid = new int[4, 4];
            for (int row = 0; row < 4; row++)
                for (int col = 0; col < 4; col++)
                    newGrid[3 - col, row] = _grid[row, col];

            Array.Copy(newGrid, _grid, 16);
        }

        private void RotateGrid180()
        {
            var newGrid = new int[4, 4];
            for (int row = 0; row < 4; row++)
                for (int col = 0; col < 4; col++)
                    newGrid[3 - row, 3 - col] = _grid[row, col];

            Array.Copy(newGrid, _grid, 16);
        }
    }
    public enum MoveDirection
    {
        Up,
        Down,
        Left,
        Right
    }
}
