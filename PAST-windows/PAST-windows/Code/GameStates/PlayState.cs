using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PAST_windows.Code.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAST_windows.Code.GameStates
{
	class PlayState : State, InputListener
	{

		private Robot player;

		private Input input;

		private Sprite cursor;

		public PlayState(StateManager manager, ContentManager content) : base(manager, content)
		{
			input = new Input();
			player = new Robot(input);
			input.AddListener(this);

			cursor = GameContent.GetSprite("cursor");
		}

		public override void Update(GameTime time)
		{
			input.Update();			// Input must be updated first!
			player.Update(time);
		}

		public override void Draw(GameTime time, SpriteBatch batch)
		{
			player.Draw(time, batch);

			if (!input.IsGamepad())
			{
				Vector2 mousePos = input.GetMousePos();
				cursor.Draw(batch, (int)mousePos.X, (int)mousePos.Y, 8, 8, 0);
			}
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
