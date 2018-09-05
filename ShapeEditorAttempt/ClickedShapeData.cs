﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShapeEditorAttempt
{
	public class ClickData
	{
		static public Point Origin { get; internal set; }
		static public Shape Shape { get; internal set; }
		static public ShapeClickAction Action { get; internal set; }
		static public Point Offset { get; internal set; }

		public ClickData(Point clickOrigin, Shape shape, ShapeClickAction action)
		{
			Set(clickOrigin, shape, action);
		}

		public ClickData() : this(Point.Empty, null, ShapeClickAction.None)
		{

		}

		static public void Set(Point origin, Shape shape, ShapeClickAction action)
		{
			Origin = origin;
			Shape = shape;
			Action = action;
		}

		static public void Set(Shape shape, ShapeClickAction action)
		{
			Shape = shape;
			Action = action;
		}

		static public void Set(Point origin, ShapeClickAction action)
		{
			Origin = origin;
			Action = action;
		}

		static public void Set(Point origin, Shape shape)
		{
			Origin = origin;
			Shape = shape;
		}

		static public void Clear(bool clearShape = true)
		{
			Set(Point.Empty, clearShape ? null : Shape, ShapeClickAction.None);
		}

		static internal void ShapeApplyOffset()
		{
			if (Shape != null)
			{
				Shape.ApplyOffset(Action);
			}
		}

		static internal void ShapeUpdateOffset(Point location)
		{
			if (Shape != null && Action != ShapeClickAction.None)
			{
				Point moveTo = new Point(
					Origin.X - Grid.SnapToGrid(location).X,
					Origin.Y - Grid.SnapToGrid(location).Y
				);

				Shape.UpdateOffset(Action, moveTo);
			}
		}
	}
}
