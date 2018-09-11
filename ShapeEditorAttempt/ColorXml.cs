using System.Drawing;

namespace ShapeEditorAttempt
{
	public struct ColorXml
	{
		public int A;
		public int R;
		public int G;
		public int B;

		public Color ToColor()
		{
			return Color.FromArgb(this.A, this.R, this.G, this.B);
		}
		public static ColorXml FromColor(Color color)
		{
			ColorXml result = new ColorXml(){ A = color.A, R = color.R, G = color.G, B = color.B };
			return result;
		}
	}
}