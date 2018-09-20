using System;

namespace ShapeEditorAttempt
{
	public class EnumNotSupportedException : NotSupportedException
	{
		public const string MESSAGESUFFIX_NOT_SUPPORTED = " not supported.";
		public const string MESSAGESUFFIX_NOT_YET_SUPPORTED = " not yet supported.";

		public EnumNotSupportedException(object enumValue) : base(Utils.GetEnumName(enumValue) + MESSAGESUFFIX_NOT_SUPPORTED)
		{

		}
	}

	public class EnumNotImplementedException : NotImplementedException
	{
		public const string MESSAGESUFFIX_NOT_IMPLEMENTED = " not implemented.";
		public const string MESSAGESUFFIX_NOT_YET_IMPLEMENTED = " not yet implemented.";

		public EnumNotImplementedException(object enumValue, string messageSuffix = MESSAGESUFFIX_NOT_IMPLEMENTED)
			: base(Utils.GetEnumName(enumValue) + messageSuffix)
		{

		}

		public EnumNotImplementedException(object enumValue)
			: this(enumValue, MESSAGESUFFIX_NOT_IMPLEMENTED)
		{

		}
	}
}
