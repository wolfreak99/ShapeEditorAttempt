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
		MainForm ParentMainForm { get; set; }

		void InitializeComponent(MainForm parentMainForm = null);

		// If parentMainForm is needed as a parameter in the future (though it should never really occur), uncomment code below
		void UninitializeComponent(/*MainForm parentMainForm = null*/);
	}
}
