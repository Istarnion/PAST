using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAST_windows.Code.GameObjects
{
	class Obstacle : GameObject
	{

		public Obstacle(int x, int y) : base(ServiceProvider.sprites.GetSprite("playerTurret"), true, x, y)
		{
			Console.WriteLine("Created an obstacle");
		}

	}
}
