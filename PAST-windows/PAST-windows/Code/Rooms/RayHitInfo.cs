using Microsoft.Xna.Framework;
using PAST_windows.Code.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAST_windows.Code.Rooms
{
	class RayHitInfo
	{

		public readonly Vector2 origin;

		public readonly Vector2 end;

		public readonly GameObject victim;

		public readonly int length;

		public RayHitInfo(Vector2 o, Vector2 e, GameObject v, int length)
		{
			origin = o;
			end = e;
			victim = v;
			this.length = length;
		}
	}
}
