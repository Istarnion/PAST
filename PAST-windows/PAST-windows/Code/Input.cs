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
		private GamePadState prevGPState;

		private Vector2 movement;

		private Vector2 aim;

		private Vector2 mousePos;

		private bool gamePad = false;

		public Input()
		{
			listeners = new List<InputListener>(1);
			prevState = Keyboard.GetState();
			prevGPState = GamePad.GetState(0);
		}

		/// <summary>
		/// This method should be called before the update to the game logic occurs every frame.
		/// TODO: Implement gamepad support.
		/// </summary>
		public void Update()
		{
			GamePadState gps = GamePad.GetState(0);
			KeyboardState kps = Keyboard.GetState();

			if (gps == null || kps != prevState)
			{
				gamePad = false;
			}
			else if (gps != null && gps != prevGPState)
			{
				gamePad = true;
			}

			if (gamePad) UpdateGamePad(gps);
			else UpdateKeyboard(kps);
		}

		private void UpdateGamePad(GamePadState gps)
		{
			if(gps.IsButtonDown(Buttons.Start) && !prevGPState.IsButtonDown(Buttons.Start))
			{
				foreach (InputListener l in listeners) { l.PausePress(); }
			}
			else if (gps.IsButtonDown(Buttons.B) && !prevGPState.IsButtonDown(Buttons.B))
			{
				foreach (InputListener l in listeners) { l.ChangeLaserPress(); }
			}
			else if (gps.Triggers.Left > 0.8f && gps.Triggers.Left <= 0.8f)
			{
				foreach (InputListener l in listeners) { l.FireButtonPress(); }
			}
			else if (gps.Triggers.Left <= 0.8f && gps.Triggers.Left > 0.8f)
			{
				foreach (InputListener l in listeners) { l.FireButtonRelease(); }
			}

			if(gps.ThumbSticks.Left.Length() > 0.2f)
			{
				movement = gps.ThumbSticks.Left;
			}
			else
			{
				movement = new Vector2(0, 0);
			}

			Vector2 rightStick = gps.ThumbSticks.Right;
			if (rightStick.Length() > 0.2f)
			{
				aim = rightStick;
			}
		}

		private void UpdateKeyboard(KeyboardState ks)
		{
			// GamePadState gpState = GamePad.GetState(PlayerIndex.One);
			MouseState mouseState = Mouse.GetState();
			KeyboardState keyboardState = ks;
			Keys[] keysDown = keyboardState.GetPressedKeys();

			int mx, my, ax, ay;

			foreach (Keys key in keysDown)
			{
				if (key == Keys.Enter && prevState.IsKeyUp(Keys.Enter))
				{
					foreach (InputListener l in listeners) { l.PausePress(); }
				}
				else if (key == Keys.LeftControl && prevState.IsKeyUp(Keys.LeftControl))
				{
					foreach (InputListener l in listeners) { l.ChangeLaserPress(); }
				}
				else if (key == Keys.Space && prevState.IsKeyUp(Keys.Space))
				{
					foreach (InputListener l in listeners) { l.FireButtonPress(); }
				}
				else if (key != Keys.Space && prevState.IsKeyDown(Keys.Space))
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
			ax += (keyboardState.IsKeyDown(Keys.Right) ? 1 : 0);
			ax -= (keyboardState.IsKeyDown(Keys.Left) ? 1 : 0);
			ay += (keyboardState.IsKeyDown(Keys.Up) ? 1 : 0);
			ay -= (keyboardState.IsKeyDown(Keys.Down) ? 1 : 0);
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

		public bool IsGamepad()
		{
			return gamePad;
		}
	}
}
