using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PAST.Code.GameStates
{

	/// <summary>
	/// This class handles the main menu system.
	/// </summary>
	class MenuState : State
	{

		private static string[] MENU_OPTIONS = { "PLAY", "QUIT" };

		private SpriteFont font;	// The font with wich will draw our menu-options. Atleast for now.

		int debugCounter = 0;

		/// <summary>
		/// The constructor sets up the menuoptions
		/// </summary>
		/// <param name="manager">The game's StateManager</param>
		public MenuState(StateManager manager) : base(manager)
		{
			
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="time">The GameTime object containing important timing info</param>
		public override void Update(GameTime time)
		{
			
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="time"></param>
		/// <param name="batch"></param>
		public override void Draw(GameTime time, SpriteBatch batch)
		{
			
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
			
		}
	}
}
