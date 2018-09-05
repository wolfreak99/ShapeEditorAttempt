using System;

namespace ShapeEditorAttempt
{
	public partial class ShapeTypeHelper
	{
		public class ShapeTypeNotSupportedException : NotImplementedException
		{
			public ShapeTypeNotSupportedException(ShapeType shapeType) : base(Utils.GetEnumName(shapeType) + " not implemented.")
			{

			}
		}
	}
}
