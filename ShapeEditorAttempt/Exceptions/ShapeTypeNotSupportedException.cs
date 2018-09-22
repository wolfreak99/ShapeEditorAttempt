using System;

namespace ShapeEditorAttempt
{
	public partial class ShapeTypeHelper
	{
		public class ShapeTypeNotSupportedException : EnumNotSupportedException
		{
			new internal static ShapeTypeNotSupportedException Throw<T>(T enumValue,
			string messageSuffix = ExceptionMessages.MSG_NOT_SUPPORTED)
			where T : struct, IConvertible
			{
				return new ShapeTypeNotSupportedException(Utils.GetEnumName(enumValue) + messageSuffix);
			}

			internal ShapeTypeNotSupportedException(string message) : base(message)
			{

			}
		}
	}
}
