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

		public PlayState(StateManager manager, ContentManager content) : base(manager, content)
		{
			input = new Input();
			player = new Robot(content.Load<Texture2D>("sprites/player"), input);
			input.AddListener(this);
		}

		public override void Update(GameTime time)
		{
			input.Update();			// Input must be updated first!
			player.Update(time);
		}

		public override void Draw(GameTime time, SpriteBatch batch)
		{
			player.Draw(time, batch);

			Vector2 mousePos = input.GetMousePos();
			batch.Draw(base.content.Load<Texture2D>("sprites/cursor"), new Rectangle((int)(mousePos.X - 4), (int)(mousePos.Y - 4), 8, 8), Color.White);
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
