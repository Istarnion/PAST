using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAST_windows.Code
{
	interface InputListener
	{
		public void PausePress();
		public void FireButtonPress();
		public void FireButtonRelease();
		public void ChangeLaserPress();
	}
}
