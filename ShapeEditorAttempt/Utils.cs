using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace ShapeEditorAttempt
{
	public class Utils
	{
		static Random random = new Random();

		private static byte ClampValue(float value)
		{
			return (byte)(value * 255.0f);
		}

		public static Color ColorSetHsv(float hue, float saturation, float value, int alpha = 255)
		{
			int i;
			float f, p, q, t;
			byte r, g, b;

			if (saturation == 0)
			{
				// achromatic (grey)
				r = g = b = ClampValue(value);
				return Color.FromArgb(r, g, b);
			}

			hue /= 60;              // Sector 0-5
			i = (int)Math.Floor(hue);
			f = hue - i;
			p = value * (1 - saturation);
			q = value * (1 - saturation * f);
			t = value * (1 - saturation * (1 - f));

			switch (i)
			{
			case 0:
					return Color.FromArgb(alpha, ClampValue(value), ClampValue(t), ClampValue(p));
			case 1:
					return Color.FromArgb(alpha, ClampValue(q), ClampValue(t), ClampValue(p));
			case 2:
					return Color.FromArgb(alpha, ClampValue(p), ClampValue(value), ClampValue(t));
			case 3:
				return Color.FromArgb(alpha, ClampValue(p), ClampValue(q), ClampValue(value));
			case 4:
				return Color.FromArgb(alpha, ClampValue(t), ClampValue(p), ClampValue(value));
			default:
				return Color.FromArgb(alpha, ClampValue(value), ClampValue(p), ClampValue(q));
			}
		}

		internal static Point Min(Point beginPoint, Point endPoint)
		{
			return new Point(
				Math.Min(beginPoint.X, endPoint.X), 
				Math.Min(beginPoint.Y, endPoint.Y)
			);
		}

		internal static Point Max(Point beginPoint, Point endPoint)
		{
			return new Point(
				Math.Max(beginPoint.X, endPoint.X), 
				Math.Max(beginPoint.Y, endPoint.Y)
			);
		}


		/// <summary>
		/// Returns "EnumType.EnumName"
		/// </summary>
		public static string GetEnumName<T>(T value)
		{
			var t = typeof(T);
			if (t.IsEnum)
				return t.Name + "." + t.GetEnumName(value);
			else
				return null;
		}

		/// <summary>
		/// This is useful for incrementing/decrementing through an enum. 
		/// </summary>
		/// <param name="current">The current value or position.</param>
		/// <param name="increment">Can be a positive or a negative number.</param>
		/// <param name="min">Can be an enum, object, or an int.</param>
		/// <param name="max">Can be an enum, object, or an int.</param>
		static public int WrapIncrement(int current, int increment, int min, int max)
		{
			return ((max + (current + increment)) % max);
		}

		/// <summary>
		/// This is useful for incrementing/decrementing through an enum. 
		/// </summary>
		/// <param name="current">Can be an enum value, object, or an int.</param>
		/// <param name="increment">Can be a positive or a negative number.</param>
		/// <param name="min">Can be an enum, object, or an int.</param>
		/// <param name="max">Can be an enum, object, or an int.</param>
		static public object WrapIncrement(object current, int increment, object min, object max)
		{
			return WrapIncrement((int)current, increment, (int)min, (int)max);
		}

		static public ImageFormat GetImageFormatByExtension(string path)
		{
			var extension = System.IO.Path.GetExtension(path);

			switch (extension)
			{
			case ".bmp": return ImageFormat.Bmp;
			case ".png": return ImageFormat.Png;
			case ".jpeg":
			case ".jpg": return ImageFormat.Jpeg;
			case ".gif": return ImageFormat.Gif;
			default: throw new NotSupportedException("File extension is not supported");
			}
		}
	}
}
