using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAST_windows.Code
{
	class Input
	{

		private List<InputListener> listeners;
		private KeyboardState prevState;

		private Vector2 movement;

		private Vector2 aim;

		public Input()
		{
			prevState = Keyboard.GetState();
		}

		/// <summary>
		/// This method should be called before the update to the game logic occurs every frame.
		/// </summary>
		public void Update()
		{
			KeyboardState state = Keyboard.GetState();
			Keys[] keysDown = state.GetPressedKeys();

			int mx, my, ax, ay;

			foreach(Keys ks in keysDown)
			{
				if(ks == Keys.Enter && prevState.IsKeyUp(Keys.Enter))
				{
					foreach (InputListener l in listeners) { l.PausePress(); }
				}
				else if(ks == Keys.LeftControl && prevState.IsKeyUp(Keys.LeftControl))
				{
					foreach (InputListener l in listeners) { l.ChangeLaserPress(); }
				}
				else if (ks == Keys.Space && prevState.IsKeyUp(Keys.Space))
				{
					foreach (InputListener l in listeners) { l.FireButtonPress(); }
				}
				else if (ks != Keys.Space && prevState.IsKeyDown(Keys.Space))
				{
					foreach (InputListener l in listeners) { l.FireButtonRelease(); }
				}
			}

			mx = 0;
			my = 0;
			mx += (state.IsKeyDown(Keys.D) ? 1 : 0);
			mx -= (state.IsKeyDown(Keys.A) ? 1 : 0);
			my += (state.IsKeyDown(Keys.W) ? 1 : 0);
			my -= (state.IsKeyDown(Keys.S) ? 1 : 0);
			movement = new Vector2(mx, my);
			if (mx != 0 && my != 0) movement.Normalize();

			ax = 0;
			ay = 0;
			ax += (state.IsKeyDown(Keys.Right) ? 1 : 0);
			ax -= (state.IsKeyDown(Keys.Left) ? 1 : 0);
			ay += (state.IsKeyDown(Keys.Up) ? 1 : 0);
			ay -= (state.IsKeyDown(Keys.Down) ? 1 : 0);
			aim = new Vector2(ax, ay);
			if (ax != 0 && ay != 0) aim.Normalize();


			prevState = state;
		}

	}
}
