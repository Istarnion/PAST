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
		private MouseState prevMouseState;

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
			float leftTrigger = gps.Triggers.Left;
			if (leftTrigger > 0.7f && prevGPState.Triggers.Left <= 0.7f)
			{
				foreach (InputListener l in listeners) { l.FireButtonPress(); }
			}
			if (leftTrigger < 0.7f && prevGPState.Triggers.Left > 0.7f)
			{
				foreach (InputListener l in listeners) { l.FireButtonRelease(); };
			}

			if(gps.ThumbSticks.Left.Length() > 0.1f)
			{
				movement = gps.ThumbSticks.Left;
			}
			else
			{
				movement = new Vector2(0, 0);
			}

			Vector2 rightStick = gps.ThumbSticks.Right;
			if (rightStick.Length() > 0.3f)
			{
				aim = new Vector2(rightStick.X, -rightStick.Y);
			}
			else if(aim.LengthSquared() == 0)
			{
				aim = new Vector2(0, -1);
			}

			prevGPState = gps;
		}

		private void UpdateKeyboard(KeyboardState ks)
		{
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
			}

			if((keyboardState.IsKeyUp(Keys.Space) && prevState.IsKeyDown(Keys.Space))
				|| (mouseState.LeftButton != ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Pressed))
			{
				foreach (InputListener l in listeners) { l.FireButtonRelease(); }
			}

			if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton != ButtonState.Pressed)
			{
				foreach (InputListener l in listeners) { l.FireButtonPress(); }
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

			prevMouseState = mouseState;
			prevState = keyboardState;
		}

		public Vector2 GetMovement()
		{
			return movement;
		}

		public Vector2 GetAim(Vector2 pos)
		{
			if (gamePad) return aim;
			else
			{
				Vector2 v = mousePos - pos;
				v.Normalize();
				return v;
			}
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
