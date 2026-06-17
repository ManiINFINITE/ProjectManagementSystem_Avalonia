using System;
using System.Globalization;
using System.IO;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;

namespace ProjectManagementSystem.Converters;

public class ByteArrayToBitmapConverter : IValueConverter {
    
    public static readonly ByteArrayToBitmapConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
        
        if (value is byte[] bytes && bytes.Length > 0) {
            using var stream = new MemoryStream(bytes);
            return new Bitmap(stream);
        }

        return null;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
        throw new NotSupportedException();
    }
}