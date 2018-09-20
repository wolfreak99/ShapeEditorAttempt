using System;

namespace ShapeEditorAttempt
{
	public partial class ShapeTypeHelper
	{
		public class ShapeTypeNotSupportedException : EnumNotSupportedException
		{
			public ShapeTypeNotSupportedException(ShapeType shapeType) : base(shapeType)
			{

			}
		}
	}
}
