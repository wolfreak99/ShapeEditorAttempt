using System;
using System.Collections.Generic;
using System.Drawing;

namespace ShapeEditorAttempt
{
	public class ClickData
	{
		static private Shape[] m_Shapes = new Shape[0];
		static public Point Origin { get; internal set; }
		
		static public Shape[] Shapes
		{
			get { return m_Shapes; }
			set
			{
				if (value == null)
				{
					m_Shapes = new Shape[0];
				}
				else
				{
					m_Shapes = value;
					if (m_Shapes.Length == 1)
					{
						MainForm.Instance.selectedShapeNameTextBox.Text = m_Shapes[0].Nickname;
					}
				}
			}
		}
		static public ShapeClickAction Action { get; internal set; }
		static public Point Offset { get; internal set; }

		#region New data
		public ClickData(Point clickOrigin, ShapeClickAction action, params Shape[] shapes)
		{
			Set(clickOrigin, action, shapes);
		}

		public ClickData() : this(Point.Empty, ShapeClickAction.None, null)
		{

		}

		static public void Set(Point origin, ShapeClickAction action, params Shape[] shapes)
		{
			Origin = origin;
			Shapes = shapes;
			Action = action;
		}

		static public void Set(ShapeClickAction action, params Shape[] shapes)
		{
			Shapes = shapes;
			Action = action;
		}

		static public void Set(Point origin, ShapeClickAction action)
		{
			Origin = origin;
			Action = action;
		}

		static public void Set(Point origin, params Shape[] shapes)
		{
			Origin = origin;
			Shapes = shapes;
		}

		static public void Set(params Shape[] shapes)
		{
			Shapes = shapes;
		}

		static public void Clear(bool clearShape = true)
		{
			Set(Point.Empty, ShapeClickAction.None, clearShape ? null : Shapes);
		}
		#endregion

		static internal void ShapeApplyOffset()
		{
			if (Shapes == null)
				throw new NullReferenceException();

			foreach (var s in Shapes)
			{
				s.ApplyOffset(Action);
			}
		}

		static internal void ShapeUpdateOffset(Point location)
		{
			if (Shapes == null)
				throw new NullReferenceException();

			if (Action == ShapeClickAction.None)
				return;

			var locationSnapped = Grid.SnapToGrid(location);
			Point moveTo = new Point(
				Origin.X - locationSnapped.X,
				Origin.Y - locationSnapped.Y
			);

			foreach (var s in Shapes)
			{
				s.UpdateOffset(Action, moveTo);
			}
		}

		static public bool ContainsShapes(params Shape[] shapes)
		{
			if (Shapes == null)
				throw new NullReferenceException();

			foreach (Shape i in shapes)
			{
				bool shapeFound = false;
				foreach (Shape j in Shapes)
				{
					if (i == j)
					{
						shapeFound = true;
						break;
					}
				}
				if (!shapeFound)
				{
					return false;
				}
			}
			return true;
		}

		static public bool IsShapesEmpty()
		{
			return Shapes.Length == 0;
		}

		static public bool IsShapesSingle()
		{
			return Shapes.Length == 1;
		}

		static public Shape ShapeSingle
		{
			get
			{
				if (IsShapesSingle())
				{
					return Shapes[0];
				}
				else
				{
					throw new UnauthorizedAccessException("ShapeSingle is accessed when Shapes.Length != 1");
				}
			}
		}
	}
}
