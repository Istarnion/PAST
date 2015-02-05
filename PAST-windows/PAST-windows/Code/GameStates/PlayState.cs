using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PAST_windows.Code.GameObjects;
using PAST_windows.Code.Graphics;
using PAST_windows.Code.Input;
using PAST_windows.Code.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAST_windows.Code.GameStates
{

	/// <summary>
	/// This is the main class of the game play.
	/// This class holds the player, and the current room, and handles all relations between these.
	/// </summary>
	class PlayState : State, InputListener
	{

		private Robot player;

		private InputHandler input;

		private Sprite cursor;

		private float cursorRot = 0;
		private float cursorRotSpeed = 1.5f;

		private Room room;

		private Vector2 cameraOffset;

		private Vector2 viewPortOffset;

		private int viewportWidth;

		private int viewportHeight;

		public PlayState(StateManager manager) : base(manager)
		{
			input = new InputHandler();
			player = new Robot(input, this, new Vector2(300, 300));
			input.AddListener(this);

			cameraOffset = new Vector2(0, 0);

			viewportWidth = Past.WIDTH;
			viewportHeight = Past.HEIGHT;

			viewPortOffset = new Vector2(viewportWidth / 2, viewportHeight / 2);

			cursor = ServiceProvider.sprites.GetSprite("cursor");

			room = new DebugRoom();
		}

		public override void Update(GameTime time)
		{
			input.Update(cameraOffset);			// Input must be updated first!
			player.Update(time, cameraOffset);
			cursorRot += (float)(cursorRotSpeed * time.ElapsedGameTime.TotalSeconds);

			cameraOffset = player.GetPosition() - viewPortOffset;
			if (cameraOffset.X < 0) cameraOffset.X = 0;
			if (cameraOffset.X > room.width - viewportWidth) cameraOffset.X = room.width - viewportWidth;
			if (cameraOffset.Y < 0) cameraOffset.Y = 0;
			if (cameraOffset.Y > room.height - viewportHeight) cameraOffset.Y = room.height - viewportHeight;
		}

		/// <summary>
		/// Shoots a laser through the current room.
		/// </summary>
		/// <param name="origin"> The origin of the laser</param>
		/// <param name="dir"> The direction of the laser </param>
		/// <param name="l"> The laser object we're shooting </param>
		/// <returns> THe end point </returns>
		public Vector2 ShootLaser(Vector2 origin, Vector2 dir, Laser l) {
			RayHitInfo info = room.Raycast(origin, dir);

			if(info.victim != null)
			{
				info.victim.HitWithLaser(l);
			}

			return info.end;
		}

		public override void Draw(GameTime time, SpriteBatch batch)
		{
			room.Draw(time, batch, cameraOffset);

			player.Draw(time, batch, cameraOffset);

			if (!input.IsGamepad())
			{
				Vector2 mousePos = input.GetMousePos();
				cursor.Draw(batch, (int)mousePos.X, (int)mousePos.Y, 10, 10, cursorRot);
			}
		}

		public override void DrawBloomed(GameTime time, SpriteBatch batch)
		{
			player.DrawBloomed(time, batch, cameraOffset);
		}

		public override void Resume()
		{
			
		}

		public override void Pause()
		{
			
		}

		void InputListener.PausePress()
		{
			// Pause the game, show pause menu
		}

		void InputListener.FireButtonPress()
		{
			player.FireButtonPress();
		}

		void InputListener.FireButtonRelease()
		{
			player.FireButtonRelease();
		}

		void InputListener.ChangeLaserPress()
		{
			player.ChangeLaser();
		}
	}
}
