using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeEditorAttempt
{
	public static class SharedActions
	{
		public static void RemoveShape(Shape shape)
		{
			if (shape != null)
			{
				Canvas.Instance.layer.Remove(shape);
				ClickData.Action = ShapeClickAction.Delete;
			}
			ClickData.Shape = null;
		}
	}
}
