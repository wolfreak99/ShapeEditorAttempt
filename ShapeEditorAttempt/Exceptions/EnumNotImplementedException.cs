using System;

namespace ShapeEditorAttempt
{
	public class EnumNotSupportedException : NotSupportedException
	{
		public EnumNotSupportedException(object enumValue) : base(Utils.GetEnumName(enumValue) + " not supported.")
		{

		}
	}
	public class EnumNotImplementedException : NotImplementedException
	{
		public EnumNotImplementedException(object enumValue) : base(Utils.GetEnumName(enumValue) + " not implemented.")
		{

		}
	}
}
