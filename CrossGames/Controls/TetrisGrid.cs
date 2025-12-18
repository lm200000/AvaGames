using Avalonia;
using Avalonia.Controls.Primitives;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CrossGames.Controls
{
    public class TetrisGrid:TemplatedControl
    {
        public static readonly StyledProperty<IEnumerable<TetrisCube>> CellsProperty =
            AvaloniaProperty.Register<TetrisGrid, IEnumerable<TetrisCube>>(nameof(Cells));
        public IEnumerable<TetrisCube> Cells
        {
            get => GetValue(CellsProperty);
            set => SetValue(CellsProperty, value);
        }
        public TetrisGrid()
        {
            
        }
        protected override Size MeasureOverride(Size availableSize)
        {
            var min = Math.Min(availableSize.Height, availableSize.Width);
            var newSize = new Size(min, 2*min);
            var size = base.MeasureOverride(newSize);
            return size;
        }
        protected override Size ArrangeOverride(Size finalSize)
        {
            var min = Math.Min(finalSize.Height, finalSize.Width);
            var newSize = new Size(min, 2*min);
            var size = base.ArrangeOverride(newSize);
            return size;
        }
    }
}
