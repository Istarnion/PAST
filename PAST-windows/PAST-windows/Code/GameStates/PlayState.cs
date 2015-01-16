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

		public PlayState(StateManager manager, ContentManager content) : base(manager, content)
		{
			input = new InputHandler();
			player = new Robot(input, this);
			input.AddListener(this);

			cameraOffset = new Vector2(0, 0);

			cursor = Sprites.GetSprite("cursor");

			room = new DebugRoom();
		}

		public override void Update(GameTime time)
		{
			input.Update();			// Input must be updated first!
			player.Update(time);
			cursorRot += (float)(cursorRotSpeed * time.ElapsedGameTime.TotalSeconds);
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
			player.Draw(time, batch);

			if (!input.IsGamepad())
			{
				Vector2 mousePos = input.GetMousePos();
				cursor.Draw(batch, (int)mousePos.X, (int)mousePos.Y, 10, 10, cursorRot);
			}
		}

		public override void DrawBloomed(GameTime time, SpriteBatch batch)
		{
			player.DrawBloomed(time, batch);
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
