using System;

namespace ShapeEditorAttempt
{
	public class EnumNotImplementedException : NotImplementedException
	{
		internal static EnumNotImplementedException Throw<T>(T enumValue, 
			string messageSuffix = ExceptionMessages.MSG_NOT_IMPLEMENTED)
			where T : struct, IConvertible
		{
			return new EnumNotImplementedException(Utils.GetEnumName(enumValue) + messageSuffix);
		}

		internal EnumNotImplementedException(string message) : base(message)
		{

		}
	}
}
