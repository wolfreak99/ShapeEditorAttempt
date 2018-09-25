using System;
using System.Collections.Generic;
using System.Drawing;

namespace ShapeEditorAttempt
{
	public class ClickData
	{
		static private Shape m_Shape = null;
		static public Point Origin { get; internal set; }

		[Obsolete]
		static public Shape Shape
		{
			get { return m_Shape; }
			internal set
			{
				m_Shape = value;
				if (m_Shape != null)
				{
					MainForm.Instance.selectedShapeNameTextBox.Text = m_Shape.Nickname;
				}
			}
		}
		
		static public ShapeClickAction Action { get; internal set; }
		static public Point Offset { get; internal set; }

		#region Obsolete data
		[Obsolete]
		public ClickData(Point clickOrigin, Shape shape, ShapeClickAction action)
		{
			Set(clickOrigin, shape, action);
		}

		[Obsolete]
		public ClickData() : this(Point.Empty, null, ShapeClickAction.None)
		{

		}

		[Obsolete]
		static public void Set(Point origin, Shape shape, ShapeClickAction action)
		{
			Origin = origin;
			Shape = shape;
			Action = action;
		}

		[Obsolete]
		static public void Set(Shape shape, ShapeClickAction action)
		{
			Shape = shape;
			Action = action;
		}

		[Obsolete]
		static public void Set(Point origin, ShapeClickAction action)
		{
			Origin = origin;
			Action = action;
		}

		[Obsolete]
		static public void Set(Point origin, Shape shape)
		{
			Origin = origin;
			Shape = shape;
		}

		[Obsolete]
		static public void Clear(bool clearShape = true)
		{
			Set(Point.Empty, clearShape ? null : Shape, ShapeClickAction.None);
		}
#endregion

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
				var locationSnapped = Grid.SnapToGrid(location);
				Point moveTo = new Point(
					Origin.X - locationSnapped.X,
					Origin.Y - locationSnapped.Y
				);

				Shape.UpdateOffset(Action, moveTo);
			}
		}
	}
}
