using System;
using System.Collections.Generic;

namespace ShapeEditorAttempt
{
	internal static class SharedActions
	{
		internal static void RemoveShape(Shape shape)
		{
			if (shape != null)
			{
				Canvas.Instance.layer.Remove(shape);
				ClickData.Action = ShapeClickAction.Delete;
				if (ClickData.Shape == shape)
				{
					ClickData.Shape = null;
				}
			}
			Canvas.Instance.Invalidate();
		}

		internal static void TriangleIncrementAngle(Shape shape)
		{
			if (shape.Type == ShapeType.Triangle)
			{
				Triangle t = (Triangle)shape;
				t.IncrementAngle();
			}
			Canvas.Instance.Invalidate();
		}
	}
}
