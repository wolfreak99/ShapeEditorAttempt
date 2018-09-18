using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShapeEditorAttempt
{
	public class Layer
	{
		private List<Shape> shapes;
		
		public Layer()
		{
			shapes = new List<Shape>();
		}

		public void Add(Shape shape)
		{
			// Insert at top of list.
			shapes.Add(shape);
		}

		public Shape[] ToArray()
		{
			return shapes.ToArray();
		}

		public Shape DuplicateShape(Shape shape, Point location)
		{
			var index = shapes.IndexOf(shape) + 1;
			// Explicitly create triangles to add angle.
			if (shape.Type == ShapeType.Triangle)
			{
				Triangle oldTriangle = (Triangle)shape;
				Triangle newTriangle = (Triangle)AddNewShape(location, shape.Size, shape.Color, shape.Type, index, false);
				newTriangle.Angle = oldTriangle.Angle;

				return newTriangle;
			}
			else
			{
				return AddNewShape(location, shape.Size, shape.Color, shape.Type, index);
			}
		}

		/// <summary>
		/// Creates a shape with the data, adds it to the layer, and returns it.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public Shape AddNewShape(Point location, Size size, Color color, ShapeType type, int index = -1, bool stretchTriangle = true)
		{
			int x = location.X - (size.Width / 2),
				y = location.Y - (size.Height / 2),
				w = size.Width,
				h = size.Height;

			Shape shape = ShapeTypeHelper.CreateNewShape(
				x, y, w, h,
				color,
				type, 
				stretchTriangle
			);

			// Add to list and return
			if (index == -1 || index >= shapes.Count)
			{
				Add(shape);
			}
			else
			{
				shapes.Insert(index, shape);
			}

			return shape;
		}

		public bool Remove(Shape shape)
		{
			return shapes.Remove(shape);
		}

		internal void MoveShapeUp(Shape shape)
		{
			var index = shapes.IndexOf(shape);
			var newIndex = index - 1;
			if (newIndex <= 0)
				return;

			shapes.Swap(index, newIndex);
		}

		internal void MoveShapeDown(Shape shape)
		{
			var index = shapes.IndexOf(shape);
			var newIndex = index + 1;
			if (newIndex >= shapes.Count)
				return;

			shapes.Swap(index, newIndex);
		}

		public void Draw(Canvas sender, PaintEventArgs e)
		{
			foreach (Shape s in shapes)
			{
				s.Draw(sender, e.Graphics);
			}
		}

		public Shape GetShapeByPoint(GraphicsPath path, Point point)
		{
			for (int i = shapes.Count - 1; i >= 0; i--)
			{
				var s = shapes[i];
				if (s.IsPointOverShape(path, point))
				{
					return s;
				}
			}

			return null;
		}

		public void Clear()
		{
			shapes.Clear();
		}

		internal void Replace(Shape shape, Shape newShape)
		{
			// If the shape was selected, select replacement shape once created.
			bool wasShapeClicked = (ClickData.Shape == shape);

			// Replace old shape with new shape
			var index = shapes.IndexOf(shape);
			shapes[index] = newShape;

			if (wasShapeClicked)
			{
				ClickData.Shape = newShape;
			}
		}

		internal void ImportFromArray(Shape[] array)
		{
			this.shapes.AddRange(array);
		}

		/// <summary>
		/// Provides a boundary to help with exporting images
		/// </summary>
		private int GetShapeTypeBoundaryOffset(ShapeType shapeType)
		{
			// TODO find reasonable number to increase boundary by per shape.
			// Use the right ear on "Examples/HappyFaceNoBG"
			switch (shapeType)
			{
			case ShapeType.Circle:
				return 4;
			case ShapeType.Triangle:
				return 4;
			default:
				return 0;
			}
		}

		/// <summary>
		/// Calculates the boundary fitting all of the layers shapes.
		/// </summary>
		/// <returns></returns>
		public Rectangle GetAllShapesBoundary()
		{
			int l = 0, t = 0, r = 0, b = 0;

			foreach (Shape shape in shapes)
			{
				var os = GetShapeTypeBoundaryOffset(shape.Type);
				if (l > (shape.Left - os))
					l = shape.Left - os;
				if (l > (shape.Top - os))
					l = shape.Top - os;
				if (r < (shape.Right + os))
					r = shape.Right + os;
				if (b < (shape.Bottom + os))
					b = shape.Bottom + os;
			}
			if (l < 0)
				l = 0;
			if (t < 0)
				t = 0;
			return Rectangle.FromLTRB(l, t, r, b);
		}
	}
}
