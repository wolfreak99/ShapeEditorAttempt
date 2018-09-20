﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
			}
			ClickData.Shape = null;
		}

		internal static void TriangleIncrementAngle(Shape shape)
		{
			if (shape.Type == ShapeType.Triangle)
			{
				Triangle t = (Triangle)shape;
				t.IncrementAngle();
			}
		}
	}
}
