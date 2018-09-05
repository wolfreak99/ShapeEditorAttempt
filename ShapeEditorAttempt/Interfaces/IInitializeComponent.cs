using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShapeEditorAttempt
{
	public interface IInitializeComponent
	{
		void InitializeComponent();

		void UninitializeComponent();
	}
}
