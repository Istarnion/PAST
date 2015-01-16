using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAST_windows.Code.Input
{
	interface InputListener
	{
		void PausePress();
		void FireButtonPress();
		void FireButtonRelease();
		void ChangeLaserPress();
	}
}
