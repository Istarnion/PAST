using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace PAST_windows.Code.GameStates
{
	
	/// <summary>
	/// This class handles the main menu system.
	/// </summary>
	class MenuState : State
	{

		private static string[] MENU_OPTIONS = { "PLAY", "QUIT" };

		private int selectedOption = 0;

		private SpriteFont font;	// The font with wich will draw our menu-options. Atleast for now.

		private const int xOffset = 100;
		private const int yOffset = 200;
		private const int spacing = 50;

		private KeyboardState oldState;

		/// <summary>
		/// The constructor sets up the menuoptions
		/// </summary>
		/// <param name="manager">The game's StateManager</param>
		public MenuState(StateManager manager, ContentManager content) : base(manager, content)
		{
			font = content.Load<SpriteFont>("fonts/Console");
			oldState = Keyboard.GetState();	// Initialize
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="time">The GameTime object containing important timing info</param>
		public override void Update(GameTime time)
		{
			KeyboardState state = Keyboard.GetState();

			// Up
			if (state.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
			{
				selectedOption--;
				if (selectedOption < 0) selectedOption = MENU_OPTIONS.Length-1;
			}

			// Down
			if (state.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
			{
				selectedOption++;
				if (selectedOption >= MENU_OPTIONS.Length) selectedOption = 0;
			}

			// Select
			if (state.IsKeyDown(Keys.Enter) && oldState.IsKeyUp(Keys.Enter))
			{
				if (selectedOption == 0) manager.Push(new PlayState(manager, content));
				else manager.Pop();
			}

			oldState = state;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="time"></param>
		/// <param name="batch"></param>
		public override void Draw(GameTime time, SpriteBatch batch)
		{
			for(int i=0; i<MENU_OPTIONS.Length; i++)
			{
				if (selectedOption == i)
				{
					batch.DrawString(font, ">"+MENU_OPTIONS[i], new Vector2(xOffset, yOffset + i * spacing), Color.White);
				}
				else
				{
					batch.DrawString(font, " "+MENU_OPTIONS[i], new Vector2(xOffset, yOffset + i * spacing), Color.White);
				}
			}
		}

		public override void DrawBloomed(GameTime time, SpriteBatch batch)
		{
			for (int i = 0; i < MENU_OPTIONS.Length; i++)
			{
				if (selectedOption == i)
				{
					batch.DrawString(font, ">" + MENU_OPTIONS[i], new Vector2(xOffset, yOffset + i * spacing), Color.White);
				}
				else
				{
					batch.DrawString(font, " " + MENU_OPTIONS[i], new Vector2(xOffset, yOffset + i * spacing), Color.White);
				}
			}
		}

		/// <summary>
		/// Is called when the player returns to the menu after play.
		/// Maybe implement a fade-in thingy?
		/// </summary>
		public override void Resume()
		{
			
		}

		/// <summary>
		/// Is called when the playState is put on the stack.
		/// If we're implementing fade-out here, it must be blocking,
		/// Update() will not be called after Pause(), until Resume is called.
		/// We probably want to reset counters here.
		/// </summary>
		public override void Pause()
		{
			selectedOption = 0;
		}
	}
}
