using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace PAST_windows.Code.GameObjects
{
	class Laser
	{
		private Sprite sprite;

		public bool active { get; set; }

		private Vector2 startPoint = new Vector2(0, 0);

		private Vector2 endPoint = new Vector2(0, 0);

		public Laser(LaserColor color)
		{
			switch(color)
			{
				case LaserColor.RED:
					sprite = Sprites.GetSprite("redLaser");
					break;
				case LaserColor.BLUE:
					sprite = Sprites.GetSprite("blueLaser");
					break;
				case LaserColor.GREEN:
					sprite = Sprites.GetSprite("greenLaser");
					break;
				default:
					break;
			}
		}

		public void Draw(SpriteBatch batch, Vector2 camOffset)
		{
			if(startPoint.X == endPoint.X)	// If perfectly vertical
			{
				sprite.Draw(batch, (int)(startPoint.X), (int)((startPoint.Y + endPoint.Y)/2), 8, (int)(startPoint - endPoint).Length(), 0);
			}
			else if(startPoint.Y == endPoint.Y)	// If perfectly horizontal
			{
				sprite.Draw(batch, (int)((startPoint.X + endPoint.X) / 2), (int)(startPoint.Y), 8, (int)(startPoint - endPoint).Length(), (float)(Math.PI / 2));
			}
			else	// Any other case
			{
				Vector2 dir = (endPoint - startPoint);
				float angle = (float)Math.Atan(dir.Y / dir.X);
				angle += (float)Math.PI/2;
				if (dir.X < 0) angle += (float)Math.PI;
				float length = dir.Length();
				dir.Normalize();

				sprite.Draw(batch, (int)(startPoint.X + dir.X * (length / 2)), (int)(startPoint.Y + dir.Y * (length / 2)), 8, (int)length, angle);
			}
		}

		public void SetStartPoint(Vector2 pos)
		{
			startPoint = pos;
		}

		public void SetEndPoint(Vector2 pos)
		{
			endPoint = pos;
		}
	}

	public enum LaserColor
	{
		RED,
		BLUE,
		GREEN
	}
}
