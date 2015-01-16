#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using PAST_windows.Code.GameStates;
using BloomPostprocess;
using System.Windows.Forms;
using PAST_windows.Code.Graphics;
#endregion

namespace PAST_windows.Code
{
	/// <summary>
	/// This is the main class for the game
	/// </summary>
	public class Past : Game
	{

		private StateManager stateManager;

		GraphicsDeviceManager graphics;
		SpriteBatch batch;
		Rectangle screenRectangle;

		private Sprites sprites;

		public readonly int WIDTH;
		public readonly int HEIGHT;

		/// <summary>
		/// Constructor sets up the GraphicsDeviceManager, sets the content directory,
		/// and initializes the stateManager.
		/// </summary>
		public Past()
			: base()
		{

			stateManager = new StateManager(this);
			graphics = new GraphicsDeviceManager(this);
			WIDTH = graphics.PreferredBackBufferWidth;
			HEIGHT = graphics.PreferredBackBufferHeight;
			screenRectangle = new Rectangle(0, 0, WIDTH, HEIGHT);

			Content.RootDirectory = "Content";
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			graphics.GraphicsDevice.PresentationParameters.BackBufferFormat = SurfaceFormat.Color;

			stateManager.Push(new MenuState(stateManager, Content));

			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Load sprites
			sprites = new Sprites(this, Content);
			ServiceProvider.sprites = sprites;

			// Create a new SpriteBatch, which can be used to draw textures.
			batch = new SpriteBatch(GraphicsDevice);
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			stateManager.Update(gameTime);

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// TODO:
		/// Fix the bloom!
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

			batch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

			stateManager.Draw(gameTime, batch);
			stateManager.DrawBloomed(gameTime, batch);

			batch.End();

			base.Draw(gameTime);
		}
	}
}
