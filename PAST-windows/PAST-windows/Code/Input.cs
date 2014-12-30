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

		private Vector2 mousePos;

		public Input()
		{
			listeners = new List<InputListener>(1);
			prevState = Keyboard.GetState();
		}

		/// <summary>
		/// This method should be called before the update to the game logic occurs every frame.
		/// TODO: Implement gamepad support.
		/// </summary>
		public void Update()
		{
			// GamePadState gpState = GamePad.GetState(PlayerIndex.One);
			MouseState mouseState = Mouse.GetState();
			KeyboardState keyboardState = Keyboard.GetState();
			Keys[] keysDown = keyboardState.GetPressedKeys();

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
			mx += (keyboardState.IsKeyDown(Keys.D) ? 1 : 0);
			mx -= (keyboardState.IsKeyDown(Keys.A) ? 1 : 0);
			my += (keyboardState.IsKeyDown(Keys.W) ? 1 : 0);
			my -= (keyboardState.IsKeyDown(Keys.S) ? 1 : 0);
			movement = new Vector2(mx, my);
			if (mx != 0 && my != 0) movement.Normalize();

			ax = 0;
			ay = 0;
			ax += (keyboardState.IsKeyDown(Keys.Right)	? 1 : 0);
			ax -= (keyboardState.IsKeyDown(Keys.Left)	? 1 : 0);
			ay += (keyboardState.IsKeyDown(Keys.Up)		? 1 : 0);
			ay -= (keyboardState.IsKeyDown(Keys.Down)	? 1 : 0);
			aim = new Vector2(ax, ay);
			if (ax != 0 && ay != 0) aim.Normalize();

			mousePos = new Vector2(mouseState.Position.X, mouseState.Position.Y);

			prevState = keyboardState;
		}

		public Vector2 GetMovement()
		{
			return movement;
		}

		public Vector2 GetAim(Vector2 pos)
		{
			if (pos == null) return aim;
			else return (mousePos - pos);
		}

		public void AddListener(InputListener il)
		{
			listeners.Add(il);
		}

		public Vector2 GetMousePos()
		{
			return mousePos;
		}
	}
}
