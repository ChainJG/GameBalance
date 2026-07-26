using GameBalance.Framework.Controls;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace GameBalance.MVVM.Utilities.Converters
{
    /// <summary>
    /// Converts an elevation value to a shadow depth (blur radius).
    /// </summary>
    public class ElevationToShadowDepthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not double baseDepth)
                return 5.0;

            if (parameter is not string paramString)
                return baseDepth;

            if (!double.TryParse(paramString, out double elevation))
                return baseDepth;

            return baseDepth + (elevation * 0.75);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Converts an elevation value to a shadow opacity.
    /// </summary>
    public class ElevationToShadowOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not double baseOpacity)
                return 0.3;

            if (parameter is not string paramString)
                return baseOpacity;

            if (!double.TryParse(paramString, out double elevation))
                return baseOpacity;

            return Math.Min(baseOpacity + (elevation * 0.03), 0.8);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Converts a background brush to an appropriate shadow color.
    /// </summary>
    public class BackgroundToShadowColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not SolidColorBrush brush)
                return Colors.Black;

            var color = brush.Color;
            var luminance = (0.299 * color.R + 0.587 * color.G + 0.114 * color.B) / 255;

            if (luminance < 0.5)
            {
                return Color.FromRgb(
                    (byte)Math.Min(255, color.R + 40),
                    (byte)Math.Min(255, color.G + 40),
                    (byte)Math.Min(255, color.B + 40)
                );
            }
            else
            {
                return Color.FromRgb(
                    (byte)Math.Max(0, color.R - 60),
                    (byte)Math.Max(0, color.G - 60),
                    (byte)Math.Max(0, color.B - 60)
                );
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Adds a value to a CornerRadius.
    /// </summary>
    public class CornerRadiusAddConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not CornerRadius radius)
                return radius;

            if (parameter is not string paramString)
                return radius;

            if (!double.TryParse(paramString, out double add))
                return radius;

            return new CornerRadius(
                radius.TopLeft + add,
                radius.TopRight + add,
                radius.BottomRight + add,
                radius.BottomLeft + add
            );
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Converts WindowCommand to tooltip text.
    /// </summary>
    public class WindowCommandToToolTipConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value switch
            {
                WindowCommand.Minimise => "Minimize",
                WindowCommand.Maximise => "Maximize",
                WindowCommand.Close => "Close",
                _ => string.Empty
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}