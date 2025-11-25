
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace CrossGames.Controls
{
    public class Cubebox : ContentControl
    {
        public static readonly StyledProperty<int> NumberProperty =
            AvaloniaProperty.Register<Cubebox, int>(nameof(Number), 0);
        public static readonly StyledProperty<IBrush> BackgroundColorProperty =
            AvaloniaProperty.Register<Cubebox, IBrush>(nameof(BackgroundColor), Brushes.LightGray);

        public int Number
        {
            get => GetValue(NumberProperty);
            set => SetValue(NumberProperty, value);
        }
        public IBrush BackgroundColor
        {
            get => GetValue(BackgroundColorProperty);
            private set => SetValue(BackgroundColorProperty, value);
        }
        static Cubebox()
        {
            // 当Number属性变化时，更新BackgroundColor 
            NumberProperty.Changed.AddClassHandler<Cubebox>((x, _) => x.UpdateBackgroundColor());
        }
        private void UpdateBackgroundColor()
        {
            BackgroundColor = Number.ToBrush();
        }
    }
    public static class Int2Color
    {
        private static readonly IBrush brush0 = Brushes.LightGray;
        private static readonly IBrush brush2 = Brush.Parse("#EEE4DA");
        private static readonly IBrush brush4 = Brush.Parse("#EDE0C8");
        private static readonly IBrush brush8 = Brush.Parse("#F2B179");
        private static readonly IBrush brush16 = Brush.Parse("#F59563");
        private static readonly IBrush brush32 = Brush.Parse("#F67C5F");
        private static readonly IBrush brush64 = Brush.Parse("#F65E3B");
        private static readonly IBrush brush128 = Brush.Parse("#EDCF72");
        private static readonly IBrush brush256 = Brush.Parse("#EDCC61");
        private static readonly IBrush brush512 = Brush.Parse("#EDC850");
        private static readonly IBrush brush1024 = Brush.Parse("#EDC53F");
        private static readonly IBrush brush2048 = Brush.Parse("#EDC850");

        public static IBrush ToBrush(this int value)
        {
            return value switch
            {      
                0 => brush0,
                2 => brush2,
                4 => brush4,
                8 => brush8,
                16 => brush16,
                32 => brush32,
                64 => brush64,
                128 => brush128,
                256 => brush256,
                512 => brush512,
                1024 => brush1024,
                2048 => brush2048,
                _ => Brushes.LightGray,
            };
        }
    }
}
