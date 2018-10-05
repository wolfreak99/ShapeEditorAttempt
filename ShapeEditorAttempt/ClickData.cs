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
			// Create list, add passed shapes, and return to Shapes.
			List<Shape> s = new List<Shape>(Shapes);
			s.AddRange(shapes);
			Shapes = s.ToArray();
		}

		/// <summary>
		/// Shortkey for "Set(Point.Empty, ShapeClickAction.None, clearShapes ? null : Shapes);"
		/// </summary>
		/// <param name="clearShapes">If true, 'Shapes' are set to null, otherwise 'Shapes' is left untouched</param>
		static public void Clear(bool clearShapes = true)
		{
			if (clearShapes)
				Set(Point.Empty, ShapeClickAction.None, null);
			else
				Set(Point.Empty, ShapeClickAction.None);
		}
		
		static internal void ShapeApplyOffset()
		{
			if (Shapes == null)
				throw new NullReferenceException("Shapes is null");

			foreach (var s in Shapes)
			{
				s.ApplyOffset(Action);
			}
		}

		static internal void ShapeUpdateOffset(Point location)
		{
			if (Shapes == null)
				throw new NullReferenceException("Shapes is null");

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
				throw new NullReferenceException("Shapes is null");

			// Search through locally passed shapes
			foreach (Shape i in shapes)
			{
				// Search through Selected shapes in memory
				foreach (Shape j in Shapes)
				{
					if (i == j)
					{
						return true;
					}
				}
			}
			return false;
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
