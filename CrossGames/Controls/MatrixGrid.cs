using Avalonia;
using Avalonia.Controls.Primitives;
using System;

namespace CrossGames.Controls
{
    public class MatrixGrid : TemplatedControl
    {
        #region Matrix Properties
        public static readonly StyledProperty<int> matrix11Property =
            AvaloniaProperty.Register<MatrixGrid, int>(nameof(matrix11), 0);
        public static readonly StyledProperty<int> matrix12Property =
            AvaloniaProperty.Register<MatrixGrid, int>(nameof(matrix12), 0);
        public static readonly StyledProperty<int> matrix13Property =
            AvaloniaProperty.Register<MatrixGrid, int>(nameof(matrix13), 0);
        public static readonly StyledProperty<int> matrix14Property =
            AvaloniaProperty.Register<MatrixGrid, int>(nameof(matrix14), 0);
        public static readonly StyledProperty<int> matrix21Property =
            AvaloniaProperty.Register<MatrixGrid, int>(nameof(matrix21), 0);
        public static readonly StyledProperty<int> matrix22Property =
            AvaloniaProperty.Register<MatrixGrid, int>(nameof(matrix22), 0);
        public static readonly StyledProperty<int> matrix23Property =
            AvaloniaProperty.Register<MatrixGrid, int>(nameof(matrix23), 0);
        public static readonly StyledProperty<int> matrix24Property =
            AvaloniaProperty.Register<MatrixGrid, int>(nameof(matrix24), 0);
        public static readonly StyledProperty<int> matrix31Property =
            AvaloniaProperty.Register<MatrixGrid, int>(nameof(matrix31), 0);
        public static readonly StyledProperty<int> matrix32Property =
            AvaloniaProperty.Register<MatrixGrid, int>(nameof(matrix32), 0);
        public static readonly StyledProperty<int> matrix33Property =
            AvaloniaProperty.Register<MatrixGrid, int>(nameof(matrix33), 0);
        public static readonly StyledProperty<int> matrix34Property =
            AvaloniaProperty.Register<MatrixGrid, int>(nameof(matrix34), 0);
        public static readonly StyledProperty<int> matrix41Property =
            AvaloniaProperty.Register<MatrixGrid, int>(nameof(matrix41), 0);
        public static readonly StyledProperty<int> matrix42Property =
            AvaloniaProperty.Register<MatrixGrid, int>(nameof(matrix42), 0);
        public static readonly StyledProperty<int> matrix43Property =
            AvaloniaProperty.Register<MatrixGrid, int>(nameof(matrix43), 0);
        public static readonly StyledProperty<int> matrix44Property =
            AvaloniaProperty.Register<MatrixGrid, int>(nameof(matrix44), 0);
        #endregion

        #region Matrix Accessors
        public int matrix11
        {
            get => GetValue(matrix11Property);
            set => SetValue(matrix11Property, value);
        }
        public int matrix12
        {
            get => GetValue(matrix12Property);
            set => SetValue(matrix12Property, value);
        }
        public int matrix13
        {
            get => GetValue(matrix13Property);
            set => SetValue(matrix13Property, value);
        }
        public int matrix14
        {
            get => GetValue(matrix14Property);
            set => SetValue(matrix14Property, value);
        }
        public int matrix21
        {
            get => GetValue(matrix21Property);
            set => SetValue(matrix21Property, value);
        }
        public int matrix22
        {
            get => GetValue(matrix22Property);
            set => SetValue(matrix22Property, value);
        }
        public int matrix23
        {
            get => GetValue(matrix23Property);
            set => SetValue(matrix23Property, value);
        }
        public int matrix24
        {
            get => GetValue(matrix24Property);
            set => SetValue(matrix24Property, value);
        }
        public int matrix31
        {
            get => GetValue(matrix31Property);
            set => SetValue(matrix31Property, value);
        }
        public int matrix32
        {
            get => GetValue(matrix32Property);
            set => SetValue(matrix32Property, value);
        }
        public int matrix33
        {
            get => GetValue(matrix33Property);
            set => SetValue(matrix33Property, value);
        }
        public int matrix34
        {
            get => GetValue(matrix34Property);
            set => SetValue(matrix34Property, value);
        }
        public int matrix41
        {
            get => GetValue(matrix41Property);
            set => SetValue(matrix41Property, value);
        }
        public int matrix42
        {
            get => GetValue(matrix42Property);
            set => SetValue(matrix42Property, value);
        }
        public int matrix43
        {
            get => GetValue(matrix43Property);
            set => SetValue(matrix43Property, value);
        }
        public int matrix44
        {
            get => GetValue(matrix44Property);
            set => SetValue(matrix44Property, value);
        }
        #endregion

        protected override Size MeasureOverride(Size availableSize)
        {
            var min = Math.Min(availableSize.Height, availableSize.Width);
            var newSize = new Size(min, min);
            var size = base.MeasureOverride(newSize);
            return size;
        }
        protected override Size ArrangeOverride(Size finalSize)
        {
            var min = Math.Min(finalSize.Height, finalSize.Width);
            var newSize = new Size(min, min);
            var size = base.ArrangeOverride(newSize);
            return size;
        }
    }
}
