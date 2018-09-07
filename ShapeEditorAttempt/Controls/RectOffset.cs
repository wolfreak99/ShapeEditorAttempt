namespace ShapeEditorAttempt
{
	public class RectOffset
	{
		public int Left, Top, Right, Bottom;
		public RectOffset(int left, int top, int right, int bottom)
		{
			this.Left = left;
			this.Top = top;
			this.Right = right;
			this.Bottom = bottom;
		}
		public int Width
		{
			get { return Right + Left; }
		}
		public int Height
		{
			get { return Bottom + Top; }
		}
	}
}