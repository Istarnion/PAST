using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PAST_windows.Code.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace PAST_windows.Code.GameObjects
{
	class Laser
	{
		private Code.Graphics.Sprite sprite;

		public bool active { get; set; }

		private Vector2 startPoint = new Vector2(0, 0);

		private Vector2 endPoint = new Vector2(0, 0);

		private Animation hitAnim;

		private float animSpeed = 0.066f;

		private float animRotSpeed = 0.01f;

		private float hitAnimRotation = 0;

		public Laser(LaserColor color)
		{
			switch(color)
			{
				case LaserColor.RED:
					sprite = ServiceProvider.sprites.GetSprite("redLaser");
					hitAnim = new Animation(
						new Sprite[] {
							ServiceProvider.sprites.GetSprite("rLaser1"),
							ServiceProvider.sprites.GetSprite("rLaser2"),
							ServiceProvider.sprites.GetSprite("rLaser3")
						}, animSpeed);
					break;
				case LaserColor.BLUE:
					sprite = ServiceProvider.sprites.GetSprite("blueLaser");
					break;
				case LaserColor.GREEN:
					sprite = ServiceProvider.sprites.GetSprite("greenLaser");
					break;
				default:
					break;
			}
		}

		public void Draw(SpriteBatch batch, Vector2 camOffset, GameTime time)
		{
			if(startPoint.X == endPoint.X)	// If perfectly vertical
			{
				sprite.Draw(batch, (int)(startPoint.X - camOffset.X), (int)((startPoint.Y + endPoint.Y)/2 - camOffset.Y), 8, (int)(startPoint - endPoint).Length(), 0);
			}
			else if(startPoint.Y == endPoint.Y)	// If perfectly horizontal
			{
				sprite.Draw(batch, (int)((startPoint.X + endPoint.X) / 2 - camOffset.X), (int)(startPoint.Y - camOffset.Y), 8, (int)(startPoint - endPoint).Length(), (float)(Math.PI / 2));
			}
			else	// Any other case
			{
				Vector2 dir = (endPoint - startPoint);
				float angle = (float)Math.Atan(dir.Y / dir.X);
				angle += (float)Math.PI/2;
				if (dir.X < 0) angle += (float)Math.PI;
				float length = dir.Length();
				dir.Normalize();

				sprite.Draw(batch, (int)((startPoint.X + dir.X * (length / 2)) - camOffset.X), (int)((startPoint.Y + dir.Y * (length / 2)) - camOffset.Y), 8, (int)length, angle);
			}

			Rectangle rect = hitAnim.CurrentFrame().GetRegion();
			hitAnim.CurrentFrame().Draw(batch, (int)(endPoint.X-camOffset.X), (int)(endPoint.Y-camOffset.Y), rect.Width, rect.Height, hitAnimRotation);
			hitAnimRotation += animRotSpeed;
			hitAnim.Update(time);
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
