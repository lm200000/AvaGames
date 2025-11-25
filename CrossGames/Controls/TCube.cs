using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
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
            AvaloniaProperty.Register<TCube, TCubeShape>(nameof(Shape));
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
        public TCube()
        {
            ShapeProperty.Changed.AddClassHandler<TCube>((x, _) => x.UpdateColors());
        }

        private void UpdateColors()
        {
            (TopColor,BottomColor) = Shape.ToBrush();
        }
    }
    public enum TCubeShape
    {
        O,
        L,
        I,
        S,
        Z
    }
    public static class TCubeExtensions
    {
       public static (IBrush,IBrush) ToBrush(this TCubeShape shape)
        {
            return shape switch
            {
                TCubeShape.O => (Brushes.Yellow,Brushes.Gold),
                TCubeShape.L => (Brushes.Orange, Brushes.Gold),
                TCubeShape.I => (Brushes.Cyan, Brushes.Gold),
                TCubeShape.S => (Brushes.Green, Brushes.Gold),
                TCubeShape.Z => (Brushes.Red, Brushes.Gold),
                _ => (Brushes.Gray, Brushes.Gold),
            };
        }
    }
}
