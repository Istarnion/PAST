using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PAST_windows.Code.GameStates;
using PAST_windows.Code.Graphics;
using PAST_windows.Code.Input;
using PAST_windows.Code.Rooms;
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

		private Laser laser;

		private InputHandler input;

		private float xPos, yPos;

		private Sprite turret;

		private Animation belts;

		private int velocity = 4;

		private float aimDir = 0;
		private float moveDir = 0;

		private PlayState playState;

		public Robot(InputHandler input, PlayState ps)
		{
			this.playState = ps;
			laser = new Laser(LaserColor.RED);

			turret = Sprites.GetSprite("playerTurret");
			belts = new Animation(new Sprite[]{Sprites.GetSprite("playerBase_1"), Sprites.GetSprite("playerBase_2")}, 0.033f);
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
			if(movement.LengthSquared() != 0)	// We're moving
			{
				moveDir = (float)Math.Atan(movement.X / movement.Y);
				moveDir += (float)(Math.PI);
				if (movement.X < 0) moveDir += (float)Math.PI;
				belts.pause = false;
			}
			else	// We're standing still
			{
				belts.pause = true;
			}

			Vector2 aim = input.GetAim(new Vector2(xPos, yPos));
			if (aim.X == 0)
			{
				if (aim.Y <= 0) aimDir = 0;
				else aimDir = (float)Math.PI;
			}
			else
			{
				aimDir = (float)Math.Atan(aim.Y / aim.X);
				aimDir += (float)(Math.PI / 2);
				if (aim.X < 0) aimDir += (float)(Math.PI);
			}

			if(laser.active)
			{
				laser.SetEndPoint(playState.ShootLaser(new Vector2(xPos, yPos), aim, laser));
				laser.SetStartPoint(new Vector2(xPos, yPos) + aim * 9);
			}
		}

		public void Draw(GameTime time, SpriteBatch batch)
		{
			// Draw base
			belts.CurrentFrame().Draw(batch, (int)xPos, (int)yPos, 64, 64, moveDir);

			// Draw top
			turret.Draw(batch, (int)xPos, (int)yPos, 64, 64, aimDir);
		}

		public void DrawBloomed(GameTime time, SpriteBatch batch)
		{
			if (laser.active)
			{
				laser.Draw(batch, new Vector2(0, 0));
			}
		}

		public void FireButtonPress()
		{
			laser.active = true;
		}

		public void FireButtonRelease()
		{
			laser.active = false;
		}

		public void ChangeLaser()
		{

		}
	}
}
