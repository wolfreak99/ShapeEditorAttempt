using System;

namespace ShapeEditorAttempt
{
	public class EnumNotSupportedException : NotSupportedException
	{
		internal static EnumNotSupportedException Throw<T>(T enumValue,
			string messageSuffix = ExceptionMessages.MSG_NOT_SUPPORTED)
			where T : struct, IConvertible
		{
			return new EnumNotSupportedException(Utils.GetEnumName(enumValue) + messageSuffix);
		}

		internal EnumNotSupportedException(string message) : base(message)
		{

		}
	}

}
