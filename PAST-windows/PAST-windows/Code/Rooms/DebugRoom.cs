using PAST_windows.Code.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAST_windows.Code.Rooms
{
	class DebugRoom : Room
	{

		public DebugRoom() : base(1000, 1000)
		{
			base.Add(new Obstacle(400, 250));
			base.background = ServiceProvider.sprites.GetSprite("debugBG");
		}
	}
}
