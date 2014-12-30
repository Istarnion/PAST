using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAST_windows.Code.GameObjects
{
	/// <summary>
	/// The class for the player-controlled robot.
	/// </summary>
	class Robot
	{

		private Input input;

		private float xPos, yPos;

		private Texture2D sprite;

		private int velocity = 4;

		private float aimDir = 0;
		private float moveDir = 0;

		public Robot(Texture2D sprite, Input input)
		{
			this.sprite = sprite;
			this.input = input;

			xPos = 200;
			yPos = 200;
		}

		public void Update(GameTime time)
		{
			Vector2 movement = input.GetMovement();
			xPos += movement.X * velocity;
			yPos -= movement.Y * velocity;
			if(movement.LengthSquared() != 0)
			{
				moveDir = (float)Math.Atan(movement.X / movement.Y);
				moveDir += (float)(Math.PI);
				if (movement.X < 0) moveDir += (float)Math.PI;
			}

			Vector2 aim = input.GetAim(new Vector2(xPos, yPos));
			aimDir = (float)Math.Atan(aim.Y / aim.X);
			aimDir += (float)(Math.PI / 2);
			if (aim.X < 0) aimDir += (float)(Math.PI);
			
		}

		public void Draw(GameTime time, SpriteBatch batch)
		{
			// Draw base
			batch.Draw(sprite, new Rectangle((int)(xPos), (int)(yPos), 64, 64), new Rectangle(64, 0, 63, 64), Color.White, moveDir, new Vector2(32, 32), SpriteEffects.None, 0);

			// Draw top
			batch.Draw(sprite, new Rectangle((int)(xPos), (int)(yPos), 64, 64), new Rectangle(0, 0, 63, 64), Color.White, aimDir, new Vector2(32, 32), SpriteEffects.None, 0);

			
		}

		public void FireButtonPress()
		{

		}

		public void FireButtonRelease()
		{

		}

		public void ChangeLaser()
		{

		}
	}
}
