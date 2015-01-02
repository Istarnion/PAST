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

		private Sprite turret;

		private Animation belts;

		private int velocity = 4;

		private float aimDir = 0;
		private float moveDir = 0;

		public Robot(Input input)
		{
			turret = GameContent.GetSprite("playerTurret");
			belts = new Animation(new Sprite[]{GameContent.GetSprite("playerBase_1"), GameContent.GetSprite("playerBase_2")}, 33);
			this.input = input;
			
			xPos = 200;
			yPos = 200;
		}

		public void Update(GameTime time)
		{
			belts.Update(time);

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
			belts.CurrentFrame().Draw(batch, (int)xPos, (int)yPos, 64, 64, moveDir);

			// Draw top
			turret.Draw(batch, (int)xPos, (int)yPos, 64, 64, aimDir);

			
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
