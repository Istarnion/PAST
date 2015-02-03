using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PAST_windows.Code.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAST_windows.Code.Rooms
{
	class Room
	{

		private int width, height;

		private List<GameObject> gameObjects;

		public Room(int width, int height)
		{
			this.width = width;
			this.height = height;
			gameObjects = new List<GameObject>(50);
		}

		public void Add(GameObject obj)
		{
			gameObjects.Add(obj);
		}

		public void Update(GameTime time)
		{
			foreach (GameObject obj in gameObjects)
			{
				obj.Update(time);
			}
		}

		public void Draw(GameTime time, SpriteBatch batch, Vector2 camOffset)
		{
			foreach (GameObject obj in gameObjects)
			{
				obj.Draw(time, batch, camOffset);
			}
		}

		/// <summary>
		/// TODO:
		///		- Optimize collision checks with quadTree or something similar
		/// </summary>
		/// <param name="origin">	The origin of the ray. </param>
		/// <param name="dir">		The direction of the ray. </param>
		/// <returns> A RayHitInfo object with all the information we can find </returns>
		public RayHitInfo Raycast(Vector2 origin, Vector2 dir)
		{
			dir.Normalize();
			Vector2 endpoint = origin;
			GameObject victim = null;

			float dx = dir.X;
			float dy = dir.Y;

			float x = origin.X;
			float y = origin.Y;

			bool hitObject = false;
			int length = 0;
			while (true)
			{
				x += dx;
				y += dy;

				// Then, check if we are still inside the bounds of the room
				if (x < 0 || x >= width || y < 0 || y >= height)
				{
					endpoint = new Vector2(x, y);
					break;
				}

				// If we are, chek against all game objects. This may be optimized with something like a quad tree if needed
				foreach (GameObject gObject in gameObjects)
				{
					if (gObject.PxPerfCollision((int)x, (int)y))
					{
						victim = gObject;
						endpoint = new Vector2(x, y);
						hitObject = true;
						break;
					}
				}
				if (hitObject) break;

				length++;
			}

			RayHitInfo info = new RayHitInfo(origin, endpoint, victim, length);
			return info;
		}
	}
}
