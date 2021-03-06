﻿#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using PAST.Code.GameStates;
#endregion

namespace PAST.Code
{
	/// <summary>
	/// This is the main class for the game
	/// </summary>
	public class PAST : Game
	{

		private StateManager stateManager;
		SpriteFont spritefont;
		GraphicsDeviceManager graphics;
		SpriteBatch batch;

		/// <summary>
		/// Constructor sets up the GraphicsDeviceManager, sets the content directory,
		/// and initializes the stateManager.
		/// </summary>
		public PAST()
			: base()
		{
			stateManager = new StateManager();
			graphics = new GraphicsDeviceManager(this);
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
			stateManager.Push(new MenuState(stateManager));

			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			batch = new SpriteBatch(GraphicsDevice);

			spritefont = Content.Load<SpriteFont>("Console");
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// Remove this
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			stateManager.Update(gameTime);

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			batch.Begin();
			batch.DrawString(spritefont, "DEBUG", new Vector2(100, 100), Color.Black);
			stateManager.Draw(gameTime, batch);
			batch.End();

			base.Draw(gameTime);
		}
	}
}
