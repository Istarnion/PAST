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

		private Vector2 position;
		private Vector2 velocity;

		private float acceleration = 0.7f;

		private float drag = 0.7f;

		private int maxVelocity = 10;

		private Sprite turret;

		private Animation belts;

		private float aimDir = 0;
		private float moveDir = 0;

		private PlayState playState;

		public Robot(InputHandler input, PlayState ps, Vector2 startPos)
		{
			this.playState = ps;
			laser = new Laser(LaserColor.RED);

			turret = ServiceProvider.sprites.GetSprite("playerTurret");
			belts = new Animation(new Sprite[] { ServiceProvider.sprites.GetSprite("playerBase_1"), ServiceProvider.sprites.GetSprite("playerBase_2") }, 0.033f);
			this.input = input;

			position = startPos;
		}

		public void Update(GameTime time, Vector2 camOffset)
		{
			belts.Update(time);

			Vector2 movement = input.GetMovement();
			velocity += movement * acceleration;
			if(velocity.LengthSquared() > maxVelocity)
			{
				velocity *= maxVelocity / velocity.Length();
			}

			position += velocity;

			velocity *= drag;

			if(velocity.LengthSquared() != 0)	// We're moving
			{
				moveDir = (float)Math.Atan(velocity.Y / velocity.X);
				moveDir += (float)(Math.PI / 2);
				if (movement.X < 0) moveDir += (float)Math.PI;
				belts.pause = false;
			}
			else	// We're standing still
			{
				belts.pause = true;
			}

			Vector2 aim = input.GetAim(position, camOffset);
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
				laser.SetEndPoint(playState.ShootLaser(position, aim, laser));
				laser.SetStartPoint(position + aim * 9);
			}
		}

		public void Draw(GameTime time, SpriteBatch batch, Vector2 camOffset)
		{
			Vector2 relPos = position - camOffset;

			// Draw base
			belts.CurrentFrame().Draw(batch, (int)relPos.X, (int)relPos.Y, 64, 64, moveDir);

			// Draw top
			turret.Draw(batch, (int)relPos.X, (int)relPos.Y, 64, 64, aimDir);
		}

		public void DrawBloomed(GameTime time, SpriteBatch batch, Vector2 camOffset)
		{
			if (laser.active)
			{
				laser.Draw(batch, camOffset);
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

		public Vector2 GetPosition()
		{
			return position;
		}
	}
}
