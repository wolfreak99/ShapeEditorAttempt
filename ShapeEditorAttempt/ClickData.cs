using System;
using System.Collections.Generic;
using System.Drawing;

namespace ShapeEditorAttempt
{
	public static class ClickData
	{
		private static Shape[] m_Shapes = new Shape[0];
		public static Point Origin { get; internal set; } = Point.Empty;
		public static Point Offset { get; internal set; } = Point.Empty;
		private static ShapeClickAction m_Action = ShapeClickAction.None;

		public static Shape[] Shapes
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
					if (IsShapesSingle())
					{
						MainForm.Instance.selectedShapeNameTextBox.Text = m_Shapes[0].Nickname;
					}
				}
			}
		}

		public static ShapeClickAction Action
		{
			get
			{
				return m_Action;
			}
			set
			{
				if (m_Action != value)
				{
					Debug.Log("ClickData.Action set to " + Utils.GetEnumName(value));
					m_Action = value;
				}
			}
		}


		#region New data
		/*public ClickData(Point clickOrigin, ShapeClickAction action, params Shape[] shapes)
		{
			Set(clickOrigin, action, shapes);
		}

		public ClickData() : this(Point.Empty, ShapeClickAction.None, null)
		{

		}*/

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

		static public void Set(ShapeClickAction action)
		{
			Action = action;
		}

		static public void Set(params Shape[] shapes)
		{
			Shapes = shapes;
		}

		static public void AddShapes(params Shape[] shapes)
		{
			List<Shape> s = new List<Shape>(Shapes);
			s.AddRange(shapes);
			Shapes = s.ToArray();
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
			{
				Debug.Log("ShapeUpdateOffset ran when Action is none");
				return;
			}

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

		internal static void ClearShapes()
		{
			Shapes = null;
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
					throw new UnauthorizedAccessException("ShapeSingle accessed but Shapes.Length != 1");
				}
			}
			set
			{
				if (IsShapesSingle())
				{
					Shapes = new Shape[1] { value };
				}
				else
				{
					throw new UnauthorizedAccessException("ShapeSingle accessed but Shapes.Length != 1");
				}
			}
		}
	}
}
