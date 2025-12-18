using Avalonia;
using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace CrossGames.Common
{
    /// <summary>
    /// 根据Bounds转换为对应的Point
    /// </summary>
    public class Bounds2PointConverter : IValueConverter
    {
        public static Bounds2PointConverter Instance { get; } = new Bounds2PointConverter();
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if(value is Rect d)
            {
                var param = parameter?.ToString();
                if (param == "TopRight")
                {
                    return new Point(d.Width, 0);
                }
                else if (param == "BottomRight")
                {
                    return new Point(d.Width,d.Height);
                }
                else if (param == "BottomLeft")
                {
                    return new Point(0, d.Height);
                }
                else if(param=="Center")
                {
                    return new List<Point>() { new(d.Width, 0), new(0, d.Height), new(d.Width, d.Height) };
                }
                else //TopLeft
                {
                    return new Point(0,0);
                }
            }
            return new Point(0, 0);
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
