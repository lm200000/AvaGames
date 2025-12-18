using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace CrossGames.Controls
{
    public class TCube:TemplatedControl
    {
        public static readonly StyledProperty<IBrush> TopColorProperty =
            AvaloniaProperty.Register<TCube, IBrush>(nameof(TopColor));
        public static readonly StyledProperty<IBrush> BottomColorProperty =
            AvaloniaProperty.Register<TCube, IBrush>(nameof(BottomColor));
        public static readonly StyledProperty<TCubeShape> ShapeProperty =
            AvaloniaProperty.Register<TCube, TCubeShape>(nameof(Shape),TCubeShape.None); 
        public static readonly StyledProperty<bool>  VisibleProperty=
            AvaloniaProperty.Register<TCube, bool>(nameof(Shape), false);
        public IBrush BottomColor
        {
            get => GetValue(BottomColorProperty);
            set => SetValue(BottomColorProperty, value);
        }
        public TCubeShape Shape
        {
            get => GetValue(ShapeProperty);
            set => SetValue(ShapeProperty, value);
        }
        public IBrush TopColor
        {
            get => GetValue(TopColorProperty);
            set => SetValue(TopColorProperty, value);
        }
        public bool Visible
        {
            get => GetValue(VisibleProperty);
            set => SetValue(VisibleProperty, value);
        }
        public TCube()
        {
            ShapeProperty.Changed.AddClassHandler<TCube>((x, _) => x.UpdateColors());
        }

        private void UpdateColors()
        {
            (TopColor,BottomColor) = Shape.ToBrush();
            Visible = Shape != TCubeShape.None;
        }      
        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            UpdateColors();
        }
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
    public enum TCubeShape
    {
        O,
        L,
        I,
        S,
        Z,
        None
    }
    public static class TCubeExtensions
    {
       public static (IBrush,IBrush) ToBrush(this TCubeShape shape)
        {

            return shape switch
            {
                TCubeShape.O => (Brushes.Yellow, Brushes.Gold),
                TCubeShape.L => (Brushes.Orange, Brushes.Gold),
                TCubeShape.I => (Brushes.Cyan, Brushes.Gold),
                TCubeShape.S => (Brushes.Green, Brushes.Gold),
                TCubeShape.Z => (Brushes.Red, Brushes.Gold),
                TCubeShape.None => (Brushes.Transparent, Brushes.Transparent),
                _ => (Brushes.Black, Brushes.Black),
            };
        }
    }
    public partial class TetrisCube:ObservableObject
    {
        [ObservableProperty]
        public int _positionX;
        [ObservableProperty]
        public int _positionY;
        public TetrisCube(int X, int Y,TCubeShape shape = TCubeShape.None)
        {
            Shape = shape;
            PositionX = X;
            PositionY = Y;
        }
        [ObservableProperty]
        public bool _isActive;
        [ObservableProperty]
        public TCubeShape _shape;
    }
}
